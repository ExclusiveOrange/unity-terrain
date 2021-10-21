using UnityEngine;

namespace Terrain
{
  public class ChunkPlacer : MonoBehaviour
  {
    public GameObject chunkType;
    public float chunkSize = 10f;
    public int chunkSteps = 10;
    public int terrainChunksPerSide = 11;

    void Start()
    {
      int steps = terrainChunksPerSide;
    
      for (int z = 0; z < steps; ++z)
      {
        float chunkZ = chunkSize * (z - (steps - 1) * 0.5f);
      
        for (int x = 0; x < steps; ++x)
        {
          float chunkX = chunkSize * (x - (steps - 1) * 0.5f);
          var newChunk = Instantiate(chunkType, new Vector3(chunkX, 0f, chunkZ), new Quaternion());
          newChunk.isStatic = true;
          newChunk.hideFlags = HideFlags.HideInHierarchy;
          newChunk.name = $"Terrain Chunk ({chunkX}, {chunkZ})";
          var meshGenerator = newChunk.GetComponent<ChunkGenerator>();
          meshGenerator.size = chunkSize;
          meshGenerator.steps = chunkSteps;
        }
      }
    }

    void Update()
    {
    }
  }
}