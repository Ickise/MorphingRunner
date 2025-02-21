using System.Collections;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private ObjectPool objectPool;
    [SerializeField] private float chunkSize = 70f;
    [SerializeField] private float spawnInterval = 2f;
    private Vector3 nextSpawnPosition;

    private void Start()
    {
        StartCoroutine(SpawnChunks());
    }
    
    private void SpawnChunk()
    {
        GameObject chunk = objectPool.GetObject();
        chunk.transform.position = nextSpawnPosition;
        nextSpawnPosition += new Vector3(0f, 0f, chunkSize);
    }
    
    private IEnumerator SpawnChunks()
    {
        while (true)
        {
            SpawnChunk();
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}