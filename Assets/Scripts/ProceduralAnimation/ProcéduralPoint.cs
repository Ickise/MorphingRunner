using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcéduralPoint : MonoBehaviour
{
    [SerializeField] private Vector3 _shift = Vector3.up;
    [SerializeField] private Vector3 _sensorDirection = Vector3.down;
    [SerializeField] private float _maxDetectionDistance = 2;

    [SerializeField, Range(0.1f, 1f)] private float _radius = 0.3f;

    [SerializeField] private LayerMask _layerMaskDetection;

    private float _floorDistance = 0;
    private bool _floorDetected = false;

    private void FixedUpdate()
    {
        RaycastHit hit;
        _floorDetected = false;

        if(Physics.SphereCast(transform.position + _shift,_radius,_sensorDirection, out hit, _maxDetectionDistance,_layerMaskDetection))
        {
            _floorDistance = hit.distance - Vector3.Project(_shift,_sensorDirection).magnitude;
            _floorDetected = true;

            Debug.DrawLine(transform.position + _shift, hit.point, Color.green, Time.fixedDeltaTime);    
            return;
        }

        Debug.DrawLine(transform.position + _shift, transform.position + _sensorDirection.normalized * _maxDetectionDistance, Color.red, Time.fixedDeltaTime);    
    }
    public float GetDistance()
    {
        return _floorDistance;
    }
}
