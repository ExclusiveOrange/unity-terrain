using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
  public float size = 10f;
  public int steps = 10;
  
  // gets the UV pointed in the same direction as a standard Plane's UV; not sure why it would be different
  private const float uvMultiplier = -1f;

  // Start is called before the first frame update
  void Start()
  {
    Mesh mesh = GetComponent<MeshFilter>().mesh;
    mesh.Clear();
    
    // allocate arrays for vertices
    int numVertices = (steps + 1) * (steps + 1);
    var newVertices = new Vector3[numVertices];
    var newNormals = new Vector3[numVertices];
    var newTangents = new Vector4[numVertices];
    var newUV = new Vector2[numVertices];

    // allocate array for triangles
    int numTriangles = steps * steps * 2;
    var newTriangles = new int[numTriangles * 3];
    
    // the position of this object for the purpose of global UV
    var position = transform.position;
    Vector2 uvOffset = new Vector2(position.x, position.z);
    
    // assign vertices
    for (int z = 0; z <= steps; ++z)
    {
      for (int x = 0; x <= steps; ++x)
      {
        int index = x + z * (steps + 1);
        newVertices[index] = new Vector3((size * -0.5f) + (size * x) / steps, 0f, (size * -0.5f) + (size * z) / steps);
        newNormals[index] = new Vector3(0f, 1f, 0f);
        newTangents[index] = new Vector4(1f, 0f, 0f, 1f);
        newUV[index] = (uvMultiplier / steps) * (new Vector2(x, z) + uvOffset);
      }
    }
    
    // assign triangles
    int triIndex = 0;
    for (int z = 0; z < steps; ++z)
    {
      for (int x = 0; x < steps; ++x)
      {
        int vertIndex = x + z * (steps + 1);
        newTriangles[triIndex++] = vertIndex;
        newTriangles[triIndex++] = vertIndex + steps + 1;
        newTriangles[triIndex++] = vertIndex + steps + 2;
        newTriangles[triIndex++] = vertIndex;
        newTriangles[triIndex++] = vertIndex + steps + 2;
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