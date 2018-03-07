using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBlock {
    Color BlockColor { get; }

    string Name { get; }

    Vector3 Position { get; }

    void Update();

    void Init();

    Mesh GetMesh();
    

}
