using System.Collections.Generic;
using UnityEngine;

public class ObjectsPool : MonoBehaviour
{
    // Ce script n'existait pas auparavant et nous permet de gérer un pool d'objets pour réutiliser des objets existants et éviter de recréer
    // des objets constamment.
    [SerializeField, Header("Settings")] private int poolSize = 10;

    [SerializeField, Header("References")] private List<GameObject> prefabs;

    private Queue<GameObject> poolQueue; // Queue qui contient nos objets réutilisables.

    private void Awake()
    {
        // Nous initialisons le pool dans l'Awake pour être sûrs que tout soit prêt avant d'être utilisé.
        InitializePool();
    }

    private void InitializePool()
    {
        // Ici, nous créons et désactivons tous les objets du pool pour les réutiliser.
        poolQueue = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            // Nous créons un objet à partir d'un prefab aléatoire de la liste.
            GameObject obj = Instantiate(prefabs[Random.Range(0, prefabs.Count)]);
            obj.SetActive(false); // Nous désactivons l'objet immédiatement, il sera réutilisé plus tard.
            poolQueue.Enqueue(obj); // Nous ajoutons l'objet désactivé à la queue du pool.
        }
    }

    public GameObject GetObject()
    {
        // Ici, nous vérifions s'il y a des objets disponibles dans le pool.
        if (poolQueue.Count > 0)
        {
            // Si le pool contient des objets, nous récupérons le premier objet et nous l'activons.
            GameObject obj = poolQueue.Dequeue();
            obj.SetActive(true); // Nous activons l'objet avant de le retourner.
            return obj;
        }
        else
        {
            // Si le pool est vide, nous créons un nouvel objet à partir d'un prefab aléatoire pour empêcher une erreur.
            GameObject obj = Instantiate(prefabs[Random.Range(0, prefabs.Count)]);
            return obj; // Nous retournons le nouvel objet créé.
        }
    }
}
