using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer),typeof(MeshFilter),typeof(MeshCollider))]
public class ChunkMono : MonoBehaviour {

    public Chunk chunk;
    public bool hasUpdated = true;
    public Transform parentObject;
    public GameObject emissiveLight;

	// Use this for initialization
	void Start () {
        transform.parent = parentObject;
        name = "Chunk " + chunk.position.x + " " + chunk.position.y;
        UpdateMesh();
	}
	
	// Update is called once per frame
	void Update () {
		if(hasUpdated == true) {
            UpdateMesh();
            hasUpdated = false;
        }
	}



    void UpdateMesh() {
        GetComponent<MeshFilter>().mesh = chunk.GenerateMesh();
        GetComponent<MeshCollider>().sharedMesh = GetComponent<MeshFilter>().mesh;
        if(transform.childCount > 0) {
            for (int i = 0; i < transform.childCount; i++) {
                GameObject go = transform.GetChild(i).gameObject;
                if (go.GetComponent<Light>()) {
                    GameObject.Destroy(go);
                }
            }
        }
        foreach(IBlock block in new List<IBlock>(chunk.blocksInChunk.Values)) {
            if(block is IEmissiveBlock) {
                GameObject go = Instantiate(emissiveLight, block.Position, Quaternion.identity, transform);
                go.GetComponent<Light>().color = (block as IEmissiveBlock).LightColor;
                go.GetComponent<Light>().intensity = (block as IEmissiveBlock).LightIntensity;
            }
        }
    }
}
