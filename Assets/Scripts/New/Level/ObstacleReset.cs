using UnityEngine;

public class ObstacleReset : MonoBehaviour
{
    // Ce script n'existait pas auparavant, mais il permet de restaurer la position et la rotation initiales de l'objet du pool. 
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private void Awake()
    {
        // Nous enregistrons la position et la rotation initiale de l'obstacle au moment où le script est chargé.
        Initialize();
    }

    private void Initialize()
    {
        initialPosition = transform.localPosition;
        initialRotation = transform.localRotation;
    }

    private void OnEnable()
    {
        ResetObject();
    }

    private void ResetObject()
    {
        // À chaque activation de l'objet, nous réinitialisons sa position et sa rotation à leur état initial.
        transform.localPosition = initialPosition;
        transform.localRotation = initialRotation;

        // Si l'obstacle a un Rigidbody, nous le supprimons afin qu'il n'affecte plus la physique de l'objet. Cela se produit lorsque le joueur s'est 
        // transformé en Pachy. 
        if (TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            Destroy(rb); // Nous détruisons le Rigidbody pour empêcher toute interaction physique indésirable.
        }
    }
}