using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshTest : MonoBehaviour {

    public Texture2D texMap;
    public int tileSize = 16;
    public GameObject chunkPrefab;

	// Use this for initialization
	void Start () {
        GetComponent<MeshFilter>().mesh = MeshUtility.CreateCube(1f, Vector3.zero);
        BlockManager.Init(chunkPrefab);
        TextureManager.Init(texMap, tileSize);
        
        for (int x = 0; x < 100; x++) {
            for (int y = 0; y < 100; y++) {
                IBlock block = new GrassBlock(Vector3.left * x + (Vector3.up * TerrainUtility.GetBlockHeight(x, y)) + Vector3.forward * y);
                for (int i = 0; i < block.Position.y; i++) {
                    IBlock block1 = new DirtBlock(new Vector3(block.Position.x, i, block.Position.z));
                    BlockManager.AddBlock(block1);
                }
                BlockManager.AddBlock(block);
            }
        }
        

        //Block block = new Block(Vector3.one);
        //BlockManager.AddBlock(block);
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public void UpdateMesh() {
        CombineInstance[] cis = new CombineInstance[BlockManager.chunks.Count];

        List<Chunk> chunks = new List<Chunk>(BlockManager.chunks.Values);
        for (int i = 0; i < chunks.Count; i++) {
            cis[i].mesh = chunks[i].GenerateMesh();
            cis[i].transform = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, Vector3.one);
        }

        GetComponent<MeshFilter>().mesh.CombineMeshes(cis);

        GetComponent<MeshCollider>().sharedMesh = GetComponent<MeshFilter>().mesh;
    }
}
