using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TextureManager {

    public static Texture2D texture;
    public static int tileSize;

    public static void Init(Texture2D tex, int TileSize) {
        texture = tex;
        tileSize = TileSize;
    }

    public static Vector2 GetUVMin(Vector2 coords) {
        float xAmount, yAmount;
        if (coords.x == 0) {
            xAmount = 0f;
        } else {
            xAmount = 1f / (tileSize / coords.x);
        }
        if (coords.y == 0) {
            yAmount = 1 - (1f/tileSize);
        } else {
            yAmount = -(tileSize / coords.y);

        }
        
        return new Vector2(xAmount+0.0004f,yAmount+0.0004f);
    }

    public static Vector2 GetUVMax(Vector2 coords) {
        return GetUVMin(coords) + new Vector2(1f / tileSize, 1f / tileSize) - new Vector2(0.0008f, 0.0008f);
    }

}
