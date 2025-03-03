using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour, IUpdate
{
    // Ce script remplace ChunGeneration. Il nous permet de gérer des chunks de manière "procédurale", en utilisant un pool d'objets.
    // Ce n'est pas une vraie génération procédurale, mais plutôt une réutilisation de chunks pré-existants. Cela permet d'optimiser les performances.
    // Il pourrait être nécessaire de revoir ce système si nous voulons implémenter une génération procédurale plus avancée à l'avenir.

    [SerializeField, Header("Settings")] private float chunkSize = 70f;
    [SerializeField] private int totalChunks = 10;
    [SerializeField] private int activeChunkCount = 3;

    [SerializeField, Header("References")] private ObjectsPool objectsPool;
    [SerializeField] private Transform playerTransform;

    private Queue<GameObject> activeChunks = new Queue<GameObject>(); // Liste des chunks actuellement actifs.
    private Queue<GameObject> inactiveChunks = new Queue<GameObject>(); // Liste des chunks inactifs prêts à être réutilisés.

    private int chunkNumber;

    private GameManager gameManager;

    private void OnEnable()
    {
        // Nous nous inscrivons pour que notre script soit mis à jour à chaque tick de l'UpdateManager.
        UpdateManager.RegisterUpdate(this);
    }

    private void OnDisable()
    {
        // Nous nous désinscrivons pour éviter les appels inutiles lors de la désactivation de l'objet.
        UpdateManager.UnregisterUpdate(this);
    }

    private void Start()
    {
       Initialize();
    }

    private void Initialize()
    {
        gameManager = GameManager.instance;
        
        // Initialisation des chunks au démarrage du jeu.
        InitializeChunks();
    }

    private void InitializeChunks()
    {
        chunkNumber = activeChunkCount - 1; // Nous ajustons le numéro du chunk pour bien placer les nouveaux chunks.

        Vector3 spawnPosition = Vector3.zero; // Nous commençons à la position initiale.

        for (int i = 0; i < totalChunks; i++)
        {
            // Nous récupérons un chunk du pool d'objets.
            GameObject chunk = objectsPool.GetObject();
            chunk.transform.position = spawnPosition;

            // Si le chunk est dans la liste des actifs, nous l'activons et nous l'ajoutons à la file des chunks actifs.
            if (i < activeChunkCount)
            {
                chunk.SetActive(true);
                activeChunks.Enqueue(chunk);
                spawnPosition += new Vector3(0f, 0f, chunkSize); // Nous déplaçons la position pour le chunk suivant.
            }
            else
            {
                // Si le chunk est inactif, nous le plaçons dans la file des chunks inactifs.
                chunk.SetActive(false);
                inactiveChunks.Enqueue(chunk);
            }
        }
    }

    private void RecycleChunk()
    {
        // Nous récupérons le premier chunk actif et le mettons inactif pour le recycler.
        GameObject oldChunk = activeChunks.Dequeue();
        oldChunk.SetActive(false);
        inactiveChunks.Enqueue(oldChunk);

        // Nous activons et positionnons un chunk inactif à la fin de la file des actifs.
        if (inactiveChunks.Count > 0)
        {
            GameObject newChunk = inactiveChunks.Dequeue();
            Vector3 lastChunkPosition = activeChunks.Peek().transform.position; // Position du dernier chunk actif.
            newChunk.transform.position = lastChunkPosition + new Vector3(0f, 0f, chunkSize * chunkNumber); // Nous le plaçons juste après le dernier chunk actif.
            newChunk.SetActive(true); // Nous activons le nouveau chunk.
            activeChunks.Enqueue(newChunk); // Nous l'ajoutons à la liste des chunks actifs.
        }
    }

    public void UpdateTick()
    {
        if (gameManager.GameIsOver()) return;
        
        // Nous vérifions si nous avons des chunks actifs et inactifs à gérer.
        if (activeChunks.Count == 0 || inactiveChunks.Count == 0) return;

        GameObject firstChunk = activeChunks.Peek(); // Nous récupérons le premier chunk actif.

        // Si le joueur a dépassé la position du premier chunk + la taille d'un chunk, nous recyclons le chunk.
        if (playerTransform.position.z > firstChunk.transform.position.z + chunkSize)
        {
            RecycleChunk();
        }
    }
}