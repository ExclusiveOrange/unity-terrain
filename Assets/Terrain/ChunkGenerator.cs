using System;
using UnityEngine;

namespace Terrain
{
  public class ChunkGenerator : MonoBehaviour
  {
    public float size = 10f;
    public int steps = 10;
    public float perlinXOffset = 0.1f;
    public float perlinZOffset = 0.2f;
    public float perlinScale = 1f;
    public float verticalSize = 1f;
    public float textureSize = 1f;
  
    // gets the UV pointed in the same direction as a standard Plane's UV; not sure why it would be different
    private const float uvMultiplier = -1f;

    // Start is called before the first frame update
    void Start()
    {
      var position = transform.position;
      float chunkCenterX = position.x;
      float chunkCenterZ = position.z;
      
      float chunkSize = size;
      int chunkSteps = steps;

      float LocalFromGrid(int x) => chunkSize * ((float) x / chunkSteps - 0.5f);

      Mesh mesh = GetComponent<MeshFilter>().mesh;
      mesh.Clear();
    
      // allocate arrays for vertices
      int numVertices = (chunkSteps + 1) * (chunkSteps + 1);
      var newVertices = new Vector3[numVertices];
      var newNormals = new Vector3[numVertices];
      var newTangents = new Vector4[numVertices];
      var newUV = new Vector2[numVertices];

      // allocate array for triangles
      int numTriangles = chunkSteps * chunkSteps * 2;
      var newTriangles = new int[numTriangles * 3];
    
      // the position of the center of this chunk for the purpose of texture UV and landscape 
      float uvScale = uvMultiplier / textureSize;

      // assign vertices
      for (int zi = 0; zi <= chunkSteps; ++zi)
      {
        float localZ = LocalFromGrid(zi);
        float globalZ = chunkCenterZ + localZ;

        for (int xi = 0; xi <= chunkSteps; ++xi)
        {
          float localX = LocalFromGrid(xi);
          float globalX = chunkCenterX + localX;
          
          float y = verticalSize * (Mathf.PerlinNoise(perlinXOffset + globalX * perlinScale, perlinZOffset + globalZ * perlinScale) - 0.5f);
        
          int index = xi + zi * (chunkSteps + 1);
        
          newVertices[index] = new Vector3(localX, y, localZ);
          newNormals[index] = new Vector3(0f, 1f, 0f);
          newTangents[index] = new Vector4(1f, 0f, 0f, 1f);
          newUV[index] = uvScale * new Vector2(globalX, globalZ);
        }
      }
    
      // assign triangles
      int triIndex = 0;
      for (int z = 0; z < chunkSteps; ++z)
      {
        for (int x = 0; x < chunkSteps; ++x)
        {
          int vertIndex = x + z * (chunkSteps + 1);
          newTriangles[triIndex++] = vertIndex;
          newTriangles[triIndex++] = vertIndex + chunkSteps + 1;
          newTriangles[triIndex++] = vertIndex + chunkSteps + 2;
          newTriangles[triIndex++] = vertIndex;
          newTriangles[triIndex++] = vertIndex + chunkSteps + 2;
          newTriangles[triIndex++] = vertIndex + 1;
        }
      }
    
      // set values to mesh
      mesh.vertices = newVertices;
      mesh.normals = newNormals;
      mesh.tangents = newTangents;
      mesh.uv = newUV;
      mesh.triangles = newTriangles;
    
      GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    // Update is called once per frame
    void Update()
    {
      // TODO: update mesh if needed
    }
  }
}