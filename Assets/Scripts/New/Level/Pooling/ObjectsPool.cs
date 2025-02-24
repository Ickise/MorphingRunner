using System.Collections.Generic;
using UnityEngine;

public class ObjectsPool : MonoBehaviour
{
    // Ce script permet de gérer un pool d'objets.
    [SerializeField, Header("Settings")] private int poolSize = 10;

    [SerializeField, Header("References")] private List<GameObject> prefabs;

    private Queue<GameObject> poolQueue;

    private void Awake()
    {
        // Je le fais dans l'awake pour être sûr que le pool soit initialisé avant d'être utilisé.
        InitializePool();
    }

    private void InitializePool()
    {
        // J'initialise le pool en créant des objets et en les désactivant.
        poolQueue = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefabs[Random.Range(0, prefabs.Count)]);
            obj.SetActive(false);
            poolQueue.Enqueue(obj);
        }
    }

    public GameObject GetObject()
    {
        // Ici, je écupère un objet du pool ou bien j'en crée un nouveau si nécessaire.
        if (poolQueue.Count > 0)
        {
            GameObject obj = poolQueue.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            GameObject obj = Instantiate(prefabs[Random.Range(0, prefabs.Count)]);
            return obj;
        }
    }
}