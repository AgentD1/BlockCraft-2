using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TerrainUtility {
    static int maxHeight = 5;
    

    static int xOffset;
    static int yOffset;

    public static int GetBlockHeight(int x, int y) {
        if (xOffset == 0) {
            Random.InitState(System.DateTime.Now.Millisecond * System.DateTime.Now.Second);
            xOffset = Random.Range(-100000, 1000000);
            yOffset = Random.Range(-100000, 1000000);
        }
        return Mathf.RoundToInt(Mathf.PerlinNoise((x + xOffset) * 0.1f, (y + yOffset) * 0.1f) * maxHeight);
    }

}
