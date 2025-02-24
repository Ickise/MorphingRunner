using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    // Ce script remplace ChunGeneration. Il permet de générer des chunks de manière "procédurale" via un système de pool de chunks. 
    // Ce n'est pas réellement de la génération procédurale, mais plutôt de la réutilisation de chunks déjà créés.
    // Cela fera l'affaire pour cet exercice d'optimisation, mais il faudra revoir le système pour de la génération procédurale réelle.
    [SerializeField, Header("Settings")] private float chunkSize = 70f;
    [SerializeField] private int initialChunkCount = 3;

    [SerializeField, Header("References")] private ObjectsPool objectsPool;
    [SerializeField] private Transform playerTransform;

    private Queue<GameObject> activeChunks = new Queue<GameObject>();
    private Vector3 nextSpawnPosition;

    private void Start()
    {
        for (int i = 0; i < initialChunkCount; i++)
        {
            SpawnChunk();
        }
        // StartCoroutine(SpawnChunks());
    }

    // Ici, j'initialise un chunk en le créant, en le positionnant et en démarrant son processus de désactivation après un délai. 
    private void SpawnChunk()
    {
        // Ceci me permet de récupérer un chunk depuis le pool.
        GameObject chunk = objectsPool.GetObject(); 
        // Je le positionne à l'endroit où je veux le faire apparaître.
        chunk.transform.position = nextSpawnPosition; 
        // Je mets à jour la position de spawn pour le prochain chunk.
        nextSpawnPosition += new Vector3(0f, 0f, chunkSize); 
        activeChunks.Enqueue(chunk);
    }

    // Je suis un peu obligé d'utiliser une Update, j'avoue que je n'ai pas d'autre idée.
    private void Update()
    {
        if (activeChunks.Count <= 0) return;
        GameObject firstChunk = activeChunks.Peek();

        // Si le joueur a dépassé le premier chunk + chunkSize alors je recycle le chunk.
        if (playerTransform.position.z > firstChunk.transform.position.z + chunkSize)
        {
            RecycleChunk();
        }
    }

    private void RecycleChunk()
    {
        // Je retire l'ancien chunk.
        GameObject chunk = activeChunks.Dequeue(); 
        // Je le déplace à la nouvelle position.
        chunk.transform.position = nextSpawnPosition; 
        nextSpawnPosition += new Vector3(0f, 0f, chunkSize);
        // Enfin, je le remets dans la file.
        activeChunks.Enqueue(chunk);
    }
}