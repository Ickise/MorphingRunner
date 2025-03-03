using System;
using UnityEngine;

public class DNARotation : MonoBehaviour, IUpdate
{
    [SerializeField, Header("Settings")] private float speedRotation = 90f;
    [SerializeField] private float amplitude = 0.5f;
    [SerializeField] private float frequency = 1f;

    private Vector3 positionOffset;
    private Vector3 tempPosition;
    private Transform dnaTransform;

    private void OnEnable()
    {
        UpdateManager.RegisterUpdate(this);
    }

    private void OnDisable()
    {
        UpdateManager.UnregisterUpdate(this);
    }

    private void Start()
    {
        dnaTransform = transform;
        positionOffset = dnaTransform.position;
    }

    public void UpdateTick()
    {
        dnaTransform.Rotate(Vector3.up * speedRotation * Time.deltaTime, Space.Self);

        tempPosition = positionOffset;
        tempPosition.y += Mathf.Sin(Time.time * Mathf.PI * frequency) * amplitude;

        dnaTransform.position = new Vector3(dnaTransform.position.x, tempPosition.y, dnaTransform.position.z);
    }
}