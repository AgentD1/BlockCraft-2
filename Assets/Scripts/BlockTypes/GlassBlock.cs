using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassBlock : ITransparentBlock {
    public Color BlockColor { get; private set; }

    public Vector3 Position { get; private set; }

    Mesh savedMesh = new Mesh();

    public string Name { get; private set; }

    public void Init() {

    }

    public void Update() {
        savedMesh = MeshUtility.CreateCubeSidedTextureOptimized(1, Position, new Vector2(7, 0), new Vector2(7, 0), new Vector2(7, 0), new Vector2(7, 0), new Vector2(7, 0), new Vector2(7, 0), this);

    }

    public GlassBlock(Vector3 pos) {
        Position = pos;
        Name = "Glass";
    }

    public Mesh GetMesh() {
        return savedMesh;
    }
}
