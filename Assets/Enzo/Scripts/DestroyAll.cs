using UnityEngine;

public class DestroyAll : MonoBehaviour
{
    [SerializeField] GameObject[] listToDestroy;
    void Start()
    {
        foreach (var item in listToDestroy)
        {
            Destroy(item.gameObject);
        }
    }
}
