using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour
{
  public GameObject chunkType;
  public float chunkSize = 10f;
  public int chunkSteps = 10;

  // Start is called before the first frame update
  void Start()
  {
    int steps = 11;
    
    for (int z = 0; z < steps; ++z)
    {
      for (int x = 0; x < steps; ++x)
      {
        Vector3 position = (new Vector3(x - steps / 2f, 0f, z - steps / 2f)) * chunkSize;
        
        var newChunk = Instantiate(chunkType, position, new Quaternion());
        newChunk.transform.position = (new Vector3(x - steps / 2f, 0f, z - steps / 2f)) * chunkSize;
        
        var meshGenerator = newChunk.GetComponent<MeshGenerator>();
        meshGenerator.size = chunkSize;
        meshGenerator.steps = chunkSteps;
      }
    }
  }

  // Update is called once per frame
  void Update()
  {
  }
}