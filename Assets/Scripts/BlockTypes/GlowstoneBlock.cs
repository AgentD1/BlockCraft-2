using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowstoneBlock : IEmissiveBlock , IAlwaysLitBlock {
    public Color BlockColor { get; private set; }

    public Color LightColor { get; private set; }
    public float LightIntensity { get; private set; }

    public Vector3 Position { get; private set; }

    Mesh savedMesh = new Mesh();

    public string Name { get; private set; }

    public void Init() {

    }

    public void Update() {
        savedMesh = MeshUtility.CreateCubeSidedTextureOptimized(1, Position, new Vector2(8, 0), new Vector2(8, 0), new Vector2(8, 0), new Vector2(8, 0), new Vector2(8, 0), new Vector2(8, 0), this);

    }

    public GlowstoneBlock(Vector3 pos) {
        Position = pos;
        Name = "Glowstone";
        LightColor = new Color(1f, 0.717f, 0f);
        LightIntensity = 1.5f;
    }

    public Mesh GetMesh() {
        return savedMesh;
    }
}
