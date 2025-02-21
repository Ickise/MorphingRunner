using System.Collections;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField, Header("Settings")] private float chunkSize = 70f;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float deactivateTime = 5f;

    [SerializeField, Header("References")] private ObjectsPool objectsPool;

    private Vector3 nextSpawnPosition;

    private void Start()
    {
        StartCoroutine(SpawnChunks());
    }

    private void SpawnChunk()
    {
        GameObject chunk = objectsPool.GetObject();
        chunk.transform.position = nextSpawnPosition;
        nextSpawnPosition += new Vector3(0f, 0f, chunkSize);
        StartCoroutine(DeactivateChunk(chunk, deactivateTime));
    }

    private IEnumerator SpawnChunks()
    {
        while (true)
        {
            SpawnChunk();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private IEnumerator DeactivateChunk(GameObject chunk, float delay)
    {
        yield return new WaitForSeconds(delay);
        objectsPool.ReturnObject(chunk);
    }
}