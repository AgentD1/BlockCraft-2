using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BlockManager {


    public static GameObject chunkPrefab;
    public static int CHUNKSIZE = 16;
    
    public static Dictionary<Vector2, Chunk> chunks;

    /// <summary>
    /// Initializes the Block Manager
    /// </summary>
    public static void Init(GameObject chunksPrefabb) {
        chunkPrefab = chunksPrefabb;
        chunks = new Dictionary<Vector2, Chunk>();
    }
    
    public static void RemoveBlock(Vector3 position) {
        Vector2 pos = new Vector2(Mathf.FloorToInt(position.x / CHUNKSIZE), Mathf.FloorToInt(position.z / CHUNKSIZE));

        Chunk chunk;
        if (!chunks.TryGetValue(pos, out chunk)) {
            return;
        }
        
        chunk.blocksInChunk.Remove(position);
        UpdateNeighbors(position);
    }

    public static GameObject GetChunkMonoGameObject(Vector2 position) {
        if(GameObject.Find("Chunk " + position.x + " " + position.y)){
            return GameObject.Find("Chunk " + position.x + " " + position.y);
        } else {
            return null;
        }
    }

    public static Vector2 GetChunkPositionFromBlockPosition(Vector3 position) {
        return new Vector2(Mathf.FloorToInt(position.x / CHUNKSIZE), Mathf.FloorToInt(position.z / CHUNKSIZE));
    }

    public static void AddBlock(IBlock block) {
        Vector2 pos = new Vector2(Mathf.FloorToInt(block.Position.x / CHUNKSIZE), Mathf.FloorToInt(block.Position.z / CHUNKSIZE));
        
        Chunk chunk;
        if(!chunks.TryGetValue(pos, out chunk)) {
            chunk = new Chunk(pos);
            chunks.Add(pos, chunk);
            GameObject go = MonoBehaviour.Instantiate(chunkPrefab);
            go.GetComponent<ChunkMono>().chunk = chunk;
        }

        chunk.blocksInChunk.Add(block.Position, block);
        chunk.blocksInChunk[block.Position].Update();
        UpdateNeighbors(block.Position);
    }

    public static Chunk GetChunkFromBlockPosition(Vector3 position) {
        return chunks[new Vector2(Mathf.FloorToInt(position.x / CHUNKSIZE), Mathf.FloorToInt(position.z / CHUNKSIZE))];
    }

    public static IBlock GetBlock(Vector3 position) {
        if (chunks.ContainsKey(GetChunkPositionFromBlockPosition(position))) {
            if (chunks[new Vector2(Mathf.FloorToInt(position.x / CHUNKSIZE), Mathf.FloorToInt(position.z / CHUNKSIZE))].blocksInChunk.ContainsKey(position)) {
                return chunks[new Vector2(Mathf.FloorToInt(position.x / CHUNKSIZE), Mathf.FloorToInt(position.z / CHUNKSIZE))].blocksInChunk[position];
            } else {
                return new DirtBlock(position) as IBlock;
            }
        } else {
            return new DirtBlock(position) as IBlock;
        }
    }

    public static bool BlockExists(Vector3 position) {
        if (!chunks.ContainsKey(new Vector2(Mathf.FloorToInt(position.x / CHUNKSIZE), Mathf.FloorToInt(position.z / CHUNKSIZE)))) {
            return false;
        }
        return chunks[new Vector2(Mathf.FloorToInt(position.x / CHUNKSIZE), Mathf.FloorToInt(position.z / CHUNKSIZE))].blocksInChunk.ContainsKey(position);
    }

    public static void UpdateNeighbors(Vector3 position) {
        IBlock block = GetBlock(position + Vector3.forward);
        block.Update();
        GameObject chunk = GetChunkMonoGameObject(GetChunkPositionFromBlockPosition(block.Position));
        if(chunk != null) {
            chunk.GetComponent<ChunkMono>().hasUpdated = true;
        }
        block = GetBlock(position + Vector3.back);
        chunk = GetChunkMonoGameObject(GetChunkPositionFromBlockPosition(block.Position));
        if (chunk != null) {
            chunk.GetComponent<ChunkMono>().hasUpdated = true;
        }
        block.Update();
        block = GetBlock(position + Vector3.up);
        chunk = GetChunkMonoGameObject(GetChunkPositionFromBlockPosition(block.Position));
        if (chunk != null) {
            chunk.GetComponent<ChunkMono>().hasUpdated = true;
        }
        block.Update();
        block = GetBlock(position + Vector3.down);
        chunk = GetChunkMonoGameObject(GetChunkPositionFromBlockPosition(block.Position));
        if (chunk != null) {
            chunk.GetComponent<ChunkMono>().hasUpdated = true;
        }
        block.Update();
        block = GetBlock(position + Vector3.left);
        chunk = GetChunkMonoGameObject(GetChunkPositionFromBlockPosition(block.Position));
        if (chunk != null) {
            chunk.GetComponent<ChunkMono>().hasUpdated = true;
        }
        block.Update();
        block = GetBlock(position + Vector3.right);
        chunk = GetChunkMonoGameObject(GetChunkPositionFromBlockPosition(block.Position));
        if (chunk != null) {
            chunk.GetComponent<ChunkMono>().hasUpdated = true;
        }
        block.Update();
    }
}
