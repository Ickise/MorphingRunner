using UnityEngine;

public class ObstacleReset : MonoBehaviour
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private void Awake()
    {
        // Je sauvegarde la position et la rotation initiale pour que lorsqu'ils réaparaissent alors ils sont à la bonne place.
        initialPosition = transform.localPosition;
        initialRotation = transform.localRotation;
    }

    private void OnEnable()
    {
        // Je réinitialise la position et la rotation.
        transform.localPosition = initialPosition;
        transform.localRotation = initialRotation;

        if (TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            Destroy(rb);
        }
    }
}