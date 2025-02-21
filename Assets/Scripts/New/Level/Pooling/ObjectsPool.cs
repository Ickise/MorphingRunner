using System.Collections.Generic;
using UnityEngine;

public class ObjectsPool : MonoBehaviour
{
    [SerializeField, Header("Settings")] private int poolSize = 10;

    [SerializeField, Header("References")] private List<GameObject> prefabs;

    private Queue<GameObject> poolQueue;

    private void Awake()
    {
        InitializePool();
    }

    private void InitializePool()
    {
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

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        poolQueue.Enqueue(obj);
    }
}