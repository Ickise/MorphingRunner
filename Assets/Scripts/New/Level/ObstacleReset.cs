using UnityEngine;

public class ObstacleReset : MonoBehaviour
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private void Start()
    {
        // Je sauvegarde la position et la rotation initiale pour que lorsqu'ils réaparaissent alors ils sont à la bonne place.
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    private void OnEnable()
    {
        // Je réinitialise la position et la rotation.
        transform.position = initialPosition;
        transform.rotation = initialRotation;

        if (TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            Destroy(rb);
        }
    }
}