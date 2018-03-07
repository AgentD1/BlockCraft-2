using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEmissiveBlock : IBlock {
    float LightIntensity { get; }
    Color LightColor { get; }
}
