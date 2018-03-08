using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk {

    public Vector2 position;

    public Dictionary<Vector3, IBlock> blocksInChunk;

    public Chunk(Vector2 position) {
        this.position = position;
        blocksInChunk = new Dictionary<Vector3, IBlock>();
    }

    public Mesh GenerateMesh() {
        List<CombineInstance> cisOpaque = new List<CombineInstance>();
        List<CombineInstance> cisTransparent = new List<CombineInstance>();
        List<CombineInstance> cisAlwaysLit = new List<CombineInstance>();

        List<IBlock> blocks = new List<IBlock>(blocksInChunk.Values);


        for (int i = 0; i < blocks.Count; i++) {
            CombineInstance ci = new CombineInstance {
                mesh = blocks[i].GetMesh(),
                transform = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, Vector3.one)
            };
            if (blocks[i] is ITransparentBlock) {
                cisTransparent.Add(ci);
            } else if (blocks[i] is IAlwaysLitBlock) {
                cisAlwaysLit.Add(ci);
            } else {
                cisOpaque.Add(ci);
            }
        }


        /*
        Mesh meshOpaque = new Mesh();
        meshOpaque.CombineMeshes(cisOpaque.ToArray());
        Mesh meshTransparent = new Mesh();
        meshOpaque.CombineMeshes(cisTransparent.ToArray());

        Mesh finalMesh = new Mesh();
        CombineInstance[] cis = new CombineInstance[] {
            new CombineInstance() { mesh = meshOpaque },
            new CombineInstance() { mesh = meshTransparent }
        };
        */
        Mesh opaqueMesh = new Mesh();
        Mesh transparentMesh = new Mesh();
        Mesh alwaysLitMesh = new Mesh();
        opaqueMesh.CombineMeshes(cisOpaque.ToArray());
        transparentMesh.CombineMeshes(cisTransparent.ToArray());
        alwaysLitMesh.CombineMeshes(cisAlwaysLit.ToArray());

        Mesh finalMesh = new Mesh();

        if(opaqueMesh.vertices.Length == 0) {
            opaqueMesh.vertices = new Vector3[] {
                Vector3.zero
            };
            opaqueMesh.triangles = new int[] {
                0,0,0
            };
        }
        if (transparentMesh.vertices.Length == 0) {
            transparentMesh.vertices = new Vector3[] {
                Vector3.zero
            };
            transparentMesh.triangles = new int[] {
                0,0,0
            };
        }
        if (alwaysLitMesh.vertices.Length == 0) {
            alwaysLitMesh.vertices = new Vector3[] {
                Vector3.zero
            };
            alwaysLitMesh.triangles = new int[] {
                0,0,0
            };
        }


        CombineInstance[] cis = new CombineInstance[] {
            new CombineInstance() {
                mesh = opaqueMesh,
                transform = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, Vector3.one)
            },
            new CombineInstance() {
                mesh = transparentMesh,
                transform = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, Vector3.one)
            },
            new CombineInstance() {
                mesh = alwaysLitMesh,
                transform = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, Vector3.one)
            }
        };

        finalMesh.CombineMeshes(cis,false);

        if(finalMesh.subMeshCount < 3) {
            finalMesh.subMeshCount = 3;
        }

        return finalMesh;
        
    }
}
