using UnityEngine;

public class DNARotation : MonoBehaviour
{
    [SerializeField, Header("Settings")] private float speedRotation = 90f;
    [SerializeField] private float amplitude = 0.5f;
    [SerializeField] private float frequency = 1f;

    private Vector3 positionOffset;
    private Vector3 tempPosition;
    private Transform dnaTransform;

    private void Start()
    {
        dnaTransform = transform;
        positionOffset = dnaTransform.position;
    }

    private void Update()
    {
        // Rotation
        dnaTransform.Rotate(Vector3.up * speedRotation * Time.deltaTime, Space.Self);

        // Vertical movement
        tempPosition = positionOffset;
        tempPosition.y += Mathf.Sin(Time.time * Mathf.PI * frequency) * amplitude;

        dnaTransform.position = new Vector3(dnaTransform.position.x, tempPosition.y, dnaTransform.position.z);
    }
}