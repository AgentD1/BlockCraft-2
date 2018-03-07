using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshUtility {

    public static Mesh CreateQuad(Vector2 size, Vector3 position) {
        float l = size.x / 2;
        float w = size.y / 2;
        Mesh mesh = new Mesh();
        mesh.SetVertices(new List<Vector3>() {
            new Vector3(position.x-l,position.y,position.z+w),
            new Vector3(position.x+l,position.y,position.z+w),
            new Vector3(position.x+l,position.y,position.z-w),
            new Vector3(position.x-l,position.y,position.z-w)
        });
        mesh.SetTriangles(new List<int>() {
            0,1,3,
            1,2,3
        }, 0);
        mesh.SetUVs(0, new List<Vector2>() {
            new Vector2(0,1),
            new Vector2(1,1),
            new Vector2(1,0),
            new Vector2(0,0)
        });
        mesh.SetNormals(new List<Vector3>(){
            Vector3.up,
            Vector3.up,
            Vector3.up,
            Vector3.up
        });
        mesh.RecalculateBounds();
        mesh.RecalculateTangents();
        return mesh;
    }


    [System.Obsolete("CreateCubeLegacy uses CreateQuad and it doesn't work well with UVs")]
    public static Mesh CreateCubeLegacy(Vector3 size, Vector3 position) {
        CombineInstance[] cis = new CombineInstance[] {
            new CombineInstance(),
            new CombineInstance(),
            new CombineInstance(),
            new CombineInstance(),
            new CombineInstance(),
            new CombineInstance()
        };
        for (int i = 0; i < 6; i++) {
            cis[i].mesh = CreateQuad(size, position);
            if (i == 0) {
                cis[i].transform = Matrix4x4.TRS(Vector3.forward * 0.5f, Quaternion.Euler(90, 0, 0), Vector3.one);
            } else if(i == 1) {
                cis[i].transform = Matrix4x4.TRS(-Vector3.forward * 0.5f, Quaternion.Euler(-90, 0, 0), Vector3.one);
            } else if (i == 2) {
                cis[i].transform = Matrix4x4.TRS(Vector3.up * 0.5f, Quaternion.Euler(0, 0, 0), Vector3.one);
            } else if (i == 3) {
                cis[i].transform = Matrix4x4.TRS(Vector3.down * 0.5f, Quaternion.Euler(0, 0, 180), Vector3.one);
            } else if (i == 4) {
                cis[i].transform = Matrix4x4.TRS(Vector3.left * 0.5f, Quaternion.Euler(0, 0, 90), Vector3.one);
            } else {
                cis[i].transform = Matrix4x4.TRS(-Vector3.left * 0.5f, Quaternion.Euler(0, 0, -90), Vector3.one);
            }
        }
        Mesh mesh = new Mesh();
        mesh.CombineMeshes(cis);
        return mesh;
    }

    public static Mesh CreateCube(float size, Vector3 position) {
        Mesh mesh = new Mesh();
        float s = size / 2f;
        mesh.SetVertices(new List<Vector3>() {
            new Vector3(-s,s,-s), // Front
            new Vector3(s,s,-s),  //
            new Vector3(s,-s,-s), //
            new Vector3(-s,-s,-s),//
            new Vector3(-s,s,s),  // Back
            new Vector3(s,s,s),   //
            new Vector3(s,-s,s),  //
            new Vector3(-s,-s,s), //
            new Vector3(-s,s,s),  // Left
            new Vector3(-s,s,-s), //
            new Vector3(-s,-s,-s),//
            new Vector3(-s,-s,s), //
            new Vector3(s,s,s),   // Right
            new Vector3(s,s,-s),  //
            new Vector3(s,-s,-s), //
            new Vector3(s,-s,s),  //
            new Vector3(-s,s,s),  // Top
            new Vector3(-s,s,-s), //
            new Vector3(s,s,-s),  //
            new Vector3(s,s,s),   //
            new Vector3(-s,-s,s), // Bopttom
            new Vector3(-s,-s,-s),//
            new Vector3(s,-s,-s), //
            new Vector3(s,-s,s)   //
        });
        mesh.SetTriangles(new List<int>() {
            0,1,3,    // Front
            1,2,3,    // 
            7,5,4,    // Back
            7,6,5,    // 
            8,9,11,   // Left
            9,10,11,  // 
            15,13,12, // Right
            15,14,13, // 
            19,17,16, // Top
            19,18,17, // 
            20,21,23, // Bottom
            21,22,23  // 
        }, 0);

        mesh.SetUVs(0, new List<Vector2>() {
            new Vector2(0,1), // Front
            new Vector2(1,1), //
            new Vector2(1,0), //
            new Vector2(0,0), //
            new Vector2(1,1), // Back
            new Vector2(0,1), //
            new Vector2(0,0), //
            new Vector2(1,0), //
            new Vector2(0,1), // Left
            new Vector2(1,1), //
            new Vector2(1,0), //
            new Vector2(0,0), //
            new Vector2(1,1), // Right
            new Vector2(0,1), //
            new Vector2(0,0), //
            new Vector2(1,0), //
            new Vector2(0,1), // Top
            new Vector2(0,0), //
            new Vector2(1,0), //
            new Vector2(1,1), //
            new Vector2(0,0), // Bottom
            new Vector2(0,1), // 
            new Vector2(1,1), //
            new Vector2(1,0), //
        });

        mesh.SetNormals(new List<Vector3>() {
            Vector3.forward,
            Vector3.forward,
            Vector3.forward,
            Vector3.forward,
            Vector3.back,
            Vector3.back,
            Vector3.back,
            Vector3.back,
            Vector3.left,
            Vector3.left,
            Vector3.left,
            Vector3.left,
            Vector3.right,
            Vector3.right,
            Vector3.right,
            Vector3.right,
            Vector3.up,
            Vector3.up,
            Vector3.up,
            Vector3.up,
            Vector3.down,
            Vector3.down,
            Vector3.down,
            Vector3.down,
        });

        mesh.RecalculateNormals();
        mesh.RecalculateTangents();
        return mesh;
    }

    public static Mesh CreateCubeSidedTexture(float size, Vector3 position, Vector2 textureCoordsFront, Vector2 textureCoordsBack, Vector2 textureCoordsLeft, Vector2 textureCoordsRight, Vector2 textureCoordsTop, Vector2 textureCoordsBottom) {
        Mesh mesh = new Mesh();
        float s = size / 2f;
        List<Vector3> verts = new List<Vector3>() {
            new Vector3(-s,s,-s), // Front
            new Vector3(s,s,-s),  //
            new Vector3(s,-s,-s), //
            new Vector3(-s,-s,-s),//
            new Vector3(-s,s,s),  // Back
            new Vector3(s,s,s),   //
            new Vector3(s,-s,s),  //
            new Vector3(-s,-s,s), //
            new Vector3(-s,s,s),  // Left
            new Vector3(-s,s,-s), //
            new Vector3(-s,-s,-s),//
            new Vector3(-s,-s,s), //
            new Vector3(s,s,s),   // Right
            new Vector3(s,s,-s),  //
            new Vector3(s,-s,-s), //
            new Vector3(s,-s,s),  //
            new Vector3(-s,s,s),  // Top
            new Vector3(-s,s,-s), //
            new Vector3(s,s,-s),  //
            new Vector3(s,s,s),   //
            new Vector3(-s,-s,s), // Bopttom
            new Vector3(-s,-s,-s),//
            new Vector3(s,-s,-s), //
            new Vector3(s,-s,s)   //
        };

        for (int i = 0; i < verts.Count; i++) {
            verts[i] += position;
        }

        mesh.SetVertices(verts);
        mesh.SetTriangles(new List<int>() {
            0,1,3,    // Front
            1,2,3,    // 
            7,5,4,    // Back
            7,6,5,    // 
            8,9,11,   // Left
            9,10,11,  // 
            15,13,12, // Right
            15,14,13, // 
            19,17,16, // Top
            19,18,17, // 
            20,21,23, // Bottom
            21,22,23  // 
        }, 0);

        
        List<Vector2> UVCoordsList = new List<Vector2>();

        float uMin = TextureManager.GetUVMin(textureCoordsFront).x;
        float uMax = TextureManager.GetUVMax(textureCoordsFront).x;
        float vMin = TextureManager.GetUVMin(textureCoordsFront).y;
        float vMax = TextureManager.GetUVMax(textureCoordsFront).y;

        UVCoordsList.AddRange(new List<Vector2>(){
            new Vector2(uMin, vMax), 
            new Vector2(uMax, vMax), 
            new Vector2(uMax, vMin), 
            new Vector2(uMin, vMin) 
        });

        uMin = TextureManager.GetUVMin(textureCoordsBack).x;
        uMax = TextureManager.GetUVMax(textureCoordsBack).x;
        vMin = TextureManager.GetUVMin(textureCoordsBack).y;
        vMax = TextureManager.GetUVMax(textureCoordsBack).y;

        UVCoordsList.AddRange(new List<Vector2>(){
            new Vector2(uMax,vMax), 
            new Vector2(uMin,vMax), 
            new Vector2(uMin,vMin), 
            new Vector2(uMax,vMin)
        });

        uMin = TextureManager.GetUVMin(textureCoordsLeft).x;
        uMax = TextureManager.GetUVMax(textureCoordsLeft).x;
        vMin = TextureManager.GetUVMin(textureCoordsLeft).y;
        vMax = TextureManager.GetUVMax(textureCoordsLeft).y;

        UVCoordsList.AddRange(new List<Vector2>(){
            new Vector2(uMin,vMax),
            new Vector2(uMax,vMax),
            new Vector2(uMax,vMin),
            new Vector2(uMin,vMin)
        });

        uMin = TextureManager.GetUVMin(textureCoordsRight).x;
        uMax = TextureManager.GetUVMax(textureCoordsRight).x;
        vMin = TextureManager.GetUVMin(textureCoordsRight).y;
        vMax = TextureManager.GetUVMax(textureCoordsRight).y;

        UVCoordsList.AddRange(new List<Vector2>(){
            new Vector2(uMax,vMax), // Right
            new Vector2(uMin,vMax), //
            new Vector2(uMin,vMin), //
            new Vector2(uMax,vMin), //
        });

        uMin = TextureManager.GetUVMin(textureCoordsTop).x;
        uMax = TextureManager.GetUVMax(textureCoordsTop).x;
        vMin = TextureManager.GetUVMin(textureCoordsTop).y;
        vMax = TextureManager.GetUVMax(textureCoordsTop).y;

        UVCoordsList.AddRange(new List<Vector2>(){
            new Vector2(uMin,vMax), // Top
            new Vector2(uMin,vMin), //
            new Vector2(uMax,vMin), //
            new Vector2(uMax,vMax), //
        });

        uMin = TextureManager.GetUVMin(textureCoordsBottom).x;
        uMax = TextureManager.GetUVMax(textureCoordsBottom).x;
        vMin = TextureManager.GetUVMin(textureCoordsBottom).y;
        vMax = TextureManager.GetUVMax(textureCoordsBottom).y;

        UVCoordsList.AddRange(new List<Vector2>(){
            new Vector2(uMin,vMin), // Bottom
            new Vector2(uMin,vMax), // 
            new Vector2(uMax,vMax), //
            new Vector2(uMax,vMin), //
        });


        mesh.SetUVs(0, UVCoordsList);

        mesh.SetNormals(new List<Vector3>() {
            Vector3.forward,
            Vector3.forward,
            Vector3.forward,
            Vector3.forward,
            Vector3.back,
            Vector3.back,
            Vector3.back,
            Vector3.back,
            Vector3.left,
            Vector3.left,
            Vector3.left,
            Vector3.left,
            Vector3.right,
            Vector3.right,
            Vector3.right,
            Vector3.right,
            Vector3.up,
            Vector3.up,
            Vector3.up,
            Vector3.up,
            Vector3.down,
            Vector3.down,
            Vector3.down,
            Vector3.down,
        });

        mesh.RecalculateNormals();
        mesh.RecalculateTangents();
        return mesh;
    }

    public static Mesh CreateCubeSidedTextureOptimized(float size, Vector3 position, Vector2 textureCoordsFront, Vector2 textureCoordsBack, Vector2 textureCoordsLeft, Vector2 textureCoordsRight, Vector2 textureCoordsTop, Vector2 textureCoordsBottom, IBlock block) {
        Mesh mesh = new Mesh();
        float s = size / 2f;
        List<Vector3> verts = new List<Vector3>();
        List<int> tris = new List<int>();
        List<Vector2> uvs = new List<Vector2>();

        int triIndex = 0;
        

        float uMin, vMin, uMax, vMax;

        //Front
        if (!BlockManager.BlockExists(block.Position + Vector3.back) || (BlockManager.GetBlock(block.Position + Vector3.back) is ITransparentBlock && !(block is ITransparentBlock))){
            verts.AddRange(new Vector3[]{
                new Vector3(-s,s,-s), // Front
                new Vector3(s,s,-s),  //
                new Vector3(s,-s,-s), //
                new Vector3(-s,-s,-s),//
            });

            tris.AddRange(new int[] {
                0+triIndex,1+triIndex,3+triIndex,    // Front
                1+triIndex,2+triIndex,3+triIndex     // 
            });
            triIndex += 4;

            uMin = TextureManager.GetUVMin(textureCoordsFront).x;
            uMax = TextureManager.GetUVMax(textureCoordsFront).x;
            vMin = TextureManager.GetUVMin(textureCoordsFront).y;
            vMax = TextureManager.GetUVMax(textureCoordsFront).y;

            uvs.AddRange(new List<Vector2>(){
                new Vector2(uMin, vMax),
                new Vector2(uMax, vMax),
                new Vector2(uMax, vMin),
                new Vector2(uMin, vMin)
            });
            


        }

        //Back
        if (!BlockManager.BlockExists(block.Position + Vector3.forward) || (BlockManager.GetBlock(block.Position + Vector3.forward) is ITransparentBlock && !(block is ITransparentBlock))) {
            verts.AddRange(new Vector3[]{
                new Vector3(-s,s,s),  // Back
                new Vector3(s,s,s),   //
                new Vector3(s,-s,s),  //
                new Vector3(-s,-s,s), //
            });

            tris.AddRange(new int[] {
                3+triIndex,1+triIndex,0+triIndex,    // Back
                3+triIndex,2+triIndex,1+triIndex    // 
            });
            triIndex += 4;


            uMin = TextureManager.GetUVMin(textureCoordsBack).x;
            uMax = TextureManager.GetUVMax(textureCoordsBack).x;
            vMin = TextureManager.GetUVMin(textureCoordsBack).y;
            vMax = TextureManager.GetUVMax(textureCoordsBack).y;

            uvs.AddRange(new List<Vector2>(){
                new Vector2(uMax,vMax),
                new Vector2(uMin,vMax),
                new Vector2(uMin,vMin),
                new Vector2(uMax,vMin)
            });

        }

        //Left
        if (!BlockManager.BlockExists(block.Position + Vector3.left) || (BlockManager.GetBlock(block.Position + Vector3.left) is ITransparentBlock && !(block is ITransparentBlock))) {
            verts.AddRange(new Vector3[]{
                new Vector3(-s,s,s),  // Left
                new Vector3(-s,s,-s), //
                new Vector3(-s,-s,-s),//
                new Vector3(-s,-s,s) //
            });

            tris.AddRange(new int[] {
                0+triIndex,1+triIndex,3+triIndex,   // Left
                1+triIndex,2+triIndex,3+triIndex  // 
            });
            triIndex += 4;
            
            uMin = TextureManager.GetUVMin(textureCoordsLeft).x;
            uMax = TextureManager.GetUVMax(textureCoordsLeft).x;
            vMin = TextureManager.GetUVMin(textureCoordsLeft).y;
            vMax = TextureManager.GetUVMax(textureCoordsLeft).y;

            uvs.AddRange(new List<Vector2>(){
                new Vector2(uMin,vMax),
                new Vector2(uMax,vMax),
                new Vector2(uMax,vMin),
                new Vector2(uMin,vMin)
            });
        }

        //Right
        if (!BlockManager.BlockExists(block.Position + Vector3.right) || (BlockManager.GetBlock(block.Position + Vector3.right) is ITransparentBlock && !(block is ITransparentBlock))) {
            verts.AddRange(new Vector3[]{
                new Vector3(s,s,s),   // Right
                new Vector3(s,s,-s),  //
                new Vector3(s,-s,-s), //
                new Vector3(s,-s,s)  //
            });

            tris.AddRange(new int[] {
                3+triIndex,1+triIndex,0+triIndex, // Right
                3+triIndex,2+triIndex,1+triIndex //
            });
            triIndex += 4;

            uMin = TextureManager.GetUVMin(textureCoordsRight).x;
            uMax = TextureManager.GetUVMax(textureCoordsRight).x;
            vMin = TextureManager.GetUVMin(textureCoordsRight).y;
            vMax = TextureManager.GetUVMax(textureCoordsRight).y;

            uvs.AddRange(new List<Vector2>(){
                new Vector2(uMax,vMax), // Right
                new Vector2(uMin,vMax), //
                new Vector2(uMin,vMin), //
                new Vector2(uMax,vMin), //
            });
        }

        //Top
        if (!BlockManager.BlockExists(block.Position + Vector3.up) || (BlockManager.GetBlock(block.Position + Vector3.up) is ITransparentBlock && !(block is ITransparentBlock))) {
            verts.AddRange(new Vector3[]{
                new Vector3(-s,s,s),  // Top
                new Vector3(-s,s,-s), //
                new Vector3(s,s,-s),  //
                new Vector3(s,s,s),   //
            });

            tris.AddRange(new int[] {
                3+triIndex,1+triIndex,0+triIndex, // Top
                3+triIndex,2+triIndex,1+triIndex, // 
            });
            triIndex += 4;

            uMin = TextureManager.GetUVMin(textureCoordsTop).x;
            uMax = TextureManager.GetUVMax(textureCoordsTop).x;
            vMin = TextureManager.GetUVMin(textureCoordsTop).y;
            vMax = TextureManager.GetUVMax(textureCoordsTop).y;

            uvs.AddRange(new List<Vector2>(){
                new Vector2(uMin,vMax), // Top
                new Vector2(uMin,vMin), //
                new Vector2(uMax,vMin), //
                new Vector2(uMax,vMax), //
            });
        }


        //Bottom
        if (!BlockManager.BlockExists(block.Position + Vector3.down) || (BlockManager.GetBlock(block.Position + Vector3.down) is ITransparentBlock && !(block is ITransparentBlock))) {
            verts.AddRange(new Vector3[]{
                new Vector3(-s,-s,s), // Bottom
                new Vector3(-s,-s,-s),//
                new Vector3(s,-s,-s), //
                new Vector3(s, -s, s)   //
            });

            tris.AddRange(new int[] {
                0+triIndex,1+triIndex,3+triIndex, // Bottom
                1+triIndex,2+triIndex,3+triIndex  // 
            });
            triIndex += 4;

            uMin = TextureManager.GetUVMin(textureCoordsBottom).x;
            uMax = TextureManager.GetUVMax(textureCoordsBottom).x;
            vMin = TextureManager.GetUVMin(textureCoordsBottom).y;
            vMax = TextureManager.GetUVMax(textureCoordsBottom).y;

            uvs.AddRange(new List<Vector2>(){
                new Vector2(uMin,vMin), // Bottom
                new Vector2(uMin,vMax), // 
                new Vector2(uMax,vMax), //
                new Vector2(uMax,vMin), //
            });
        }

        
        for (int i = 0; i < verts.Count; i++) {
            verts[i] += position;
        }

        mesh.SetVertices(verts);
        
        mesh.SetTriangles(tris, 0);
        

        mesh.SetUVs(0, uvs);
        
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();
        return mesh;
    }


    public static Mesh CreateCubeSingleTexture(float size, Vector3 position, Vector2 textureCoords) {
        Mesh mesh = new Mesh();
        float s = size / 2f;
        mesh.SetVertices(new List<Vector3>() {
            new Vector3(-s,s,-s), // Front
            new Vector3(s,s,-s),  //
            new Vector3(s,-s,-s), //
            new Vector3(-s,-s,-s),//
            new Vector3(-s,s,s),  // Back
            new Vector3(s,s,s),   //
            new Vector3(s,-s,s),  //
            new Vector3(-s,-s,s), //
            new Vector3(-s,s,s),  // Left
            new Vector3(-s,s,-s), //
            new Vector3(-s,-s,-s),//
            new Vector3(-s,-s,s), //
            new Vector3(s,s,s),   // Right
            new Vector3(s,s,-s),  //
            new Vector3(s,-s,-s), //
            new Vector3(s,-s,s),  //
            new Vector3(-s,s,s),  // Top
            new Vector3(-s,s,-s), //
            new Vector3(s,s,-s),  //
            new Vector3(s,s,s),   //
            new Vector3(-s,-s,s), // Bopttom
            new Vector3(-s,-s,-s),//
            new Vector3(s,-s,-s), //
            new Vector3(s,-s,s)   //
        });
        mesh.SetTriangles(new List<int>() {
            0,1,3,    // Front
            1,2,3,    // 
            7,5,4,    // Back
            7,6,5,    // 
            8,9,11,   // Left
            9,10,11,  // 
            15,13,12, // Right
            15,14,13, // 
            19,17,16, // Top
            19,18,17, // 
            20,21,23, // Bottom
            21,22,23  // 
        }, 0);

        float uMin = TextureManager.GetUVMin(textureCoords).x;
        float uMax = TextureManager.GetUVMax(textureCoords).x;
        float vMin = TextureManager.GetUVMin(textureCoords).y;
        float vMax = TextureManager.GetUVMax(textureCoords).y;

        mesh.SetUVs(0, new List<Vector2>() {
            new Vector2(uMin,vMax), // Front
            new Vector2(uMax,vMax), //
            new Vector2(uMax,vMin), //
            new Vector2(uMin,vMin), //
            new Vector2(uMax,vMax), // Back
            new Vector2(uMin,vMax), //
            new Vector2(uMin,vMin), //
            new Vector2(uMax,vMin), //
            new Vector2(uMin,vMax), // Left
            new Vector2(uMax,vMax), //
            new Vector2(uMax,vMin), //
            new Vector2(uMin,vMin), //
            new Vector2(uMax,vMax), // Right
            new Vector2(uMin,vMax), //
            new Vector2(uMin,vMin), //
            new Vector2(uMax,vMin), //
            new Vector2(uMin,vMax), // Top
            new Vector2(uMin,vMin), //
            new Vector2(uMax,vMin), //
            new Vector2(uMax,vMax), //
            new Vector2(uMin,vMin), // Bottom
            new Vector2(uMin,vMax), // 
            new Vector2(uMax,vMax), //
            new Vector2(uMax,vMin), //
        });

        mesh.SetNormals(new List<Vector3>() {
            Vector3.forward,
            Vector3.forward,
            Vector3.forward,
            Vector3.forward,
            Vector3.back,
            Vector3.back,
            Vector3.back,
            Vector3.back,
            Vector3.left,
            Vector3.left,
            Vector3.left,
            Vector3.left,
            Vector3.right,
            Vector3.right,
            Vector3.right,
            Vector3.right,
            Vector3.up,
            Vector3.up,
            Vector3.up,
            Vector3.up,
            Vector3.down,
            Vector3.down,
            Vector3.down,
            Vector3.down,
        });

        mesh.RecalculateNormals();
        mesh.RecalculateTangents();
        return mesh;
    }

}
