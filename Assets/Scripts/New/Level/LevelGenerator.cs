using System.Collections;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    // Ce script remplace ChunGeneration. Il permet de générer des chunks de manière "procédurale" via un système de pool de chunks. 
    // Ce n'est pas réellement de la génération procédurale, mais plutôt de la réutilisation de chunks déjà créés.
    // Cela fera l'affaire pour cet exercice d'optimisation, mais il faudra revoir le système pour de la génération procédurale réelle.
    [SerializeField, Header("Settings")] private float chunkSize = 70f;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float deactivateTime = 5f;

    [SerializeField, Header("References")] private ObjectsPool objectsPool;

    private Vector3 nextSpawnPosition;

    private void Start()
    {
        StartCoroutine(SpawnChunks());
    }

    // Ici, j'initialise un chunk en le créant, en le positionnant et en démarrant son processus de désactivation après un délai. 
    private void SpawnChunk()
    {
        GameObject chunk = objectsPool.GetObject(); // Ceci me permet de récupérer un chunk depuis le pool.
        chunk.transform.position = nextSpawnPosition; // Je le positionne à l'endroit où je veux le faire apparaître.
        nextSpawnPosition += new Vector3(0f, 0f, chunkSize); // Je mets à jour la position de spawn pour le prochain chunk.
        StartCoroutine(DeactivateChunk(chunk, deactivateTime)); // Enfin, je rends visible le chunk avant de le désactiver après un certain délai.
    }

    private IEnumerator SpawnChunks()
    {
        while (true) // Obligatoire pour que la coroutine tourne en boucle.
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