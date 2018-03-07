using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtBlock : IBlock {

    public Color BlockColor { get; private set; }
    
    public Vector3 Position { get; private set; }

    Mesh savedMesh = new Mesh();

    public string Name { get; private set; }

    public void Init () {
        BlockColor = new Color(0.666f, 0.294f, 0);
	}

    public void Update() {
        savedMesh = MeshUtility.CreateCubeSidedTextureOptimized(1, Position, new Vector2(2, 0), new Vector2(2, 0), new Vector2(2, 0), new Vector2(2, 0), new Vector2(2, 0), new Vector2(2, 0), this);

    }

    public DirtBlock(Vector3 pos) {
        Position = pos;
        Name = "Dirt";
    }

    public Mesh GetMesh() {
        return savedMesh;
    }
}
