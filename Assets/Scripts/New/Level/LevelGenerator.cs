using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelGenerator : MonoBehaviour
{
    // Ce script remplace ChunGeneration. Il permet de générer des chunks de manière "procédurale" via un système de pool de chunks. 
    // Ce n'est pas réellement de la génération procédurale, mais plutôt de la réutilisation de chunks déjà créés.
    // Cela fera l'affaire pour cet exercice d'optimisation, mais il faudra peut être revoir le système pour de la génération procédurale réelle.
    [SerializeField, Header("Settings")] private float chunkSize = 70f;
    [SerializeField] private int totalChunks = 10;
    [SerializeField] private int activeChunkCount = 3;

    [SerializeField, Header("References")] private ObjectsPool objectsPool;
    [SerializeField] private Transform playerTransform;

    // J'ai besoin de deux Queue pour avoir les actifs et inactifs. Cela va me permettre de fair apparaitre et disparaitre les chunks.
    private Queue<GameObject> activeChunks = new Queue<GameObject>();
    private Queue<GameObject> inactiveChunks = new Queue<GameObject>();

    private int chunkNumber;

    private void Start()
    {
        InitializeChunks();
    }

    private void InitializeChunks()
    {
        chunkNumber = activeChunkCount - 1;
        
        Vector3 spawnPosition = Vector3.zero;
        
        for (int i = 0; i < totalChunks; i++)
        {
            GameObject chunk = objectsPool.GetObject();
            chunk.transform.position = spawnPosition;
            if (i < activeChunkCount)
            {
                chunk.SetActive(true);
                activeChunks.Enqueue(chunk);
                spawnPosition += new Vector3(0f, 0f, chunkSize);
            }
            else
            {
                chunk.SetActive(false);
                inactiveChunks.Enqueue(chunk);
            }
        }
    }

    // Je suis un peu obligé d'utiliser une Update, je pense que le mieux serait de faire une UpdateManager et une interface IUpdate.
    // En faisant cela, j'aurai une seule Update et tous les scripts ayant l'interface se lancerait dans l'Update.
    private void Update()
    {
        if (activeChunks.Count == 0 || inactiveChunks.Count == 0) return;
        GameObject firstChunk = activeChunks.Peek();

        // Si le joueur a dépassé le premier chunk + chunkSize alors je recycle le chunk.
        if (playerTransform.position.z > firstChunk.transform.position.z + chunkSize)
        {
            RecycleChunk();
        }
    }

    private void RecycleChunk()
    {
        GameObject oldChunk = activeChunks.Dequeue();
        oldChunk.SetActive(false);
        inactiveChunks.Enqueue(oldChunk);

        // J'active et place le chunk inactif au bout de la file.
        if (inactiveChunks.Count > 0)
        {
            GameObject newChunk = inactiveChunks.Dequeue();
            Vector3 lastChunkPosition = activeChunks.Peek().transform.position;
            newChunk.transform.position = lastChunkPosition + new Vector3(0f, 0f, chunkSize * chunkNumber);
            newChunk.SetActive(true);
            activeChunks.Enqueue(newChunk);
        }
    }
}