using UnityEngine;

public class DestroyAll : MonoBehaviour
{
    [SerializeField] GameObject[] listToDestroy;
    void Start()
    {
        // Nous aurions dû ajouter une vérification pour éviter une erreur sur un objet null et potentiellement juste SetActive(false) les objets
        // pour gagner en performance. 
        foreach (var item in listToDestroy)
        {
            Destroy(item.gameObject);
        }
    }
}
