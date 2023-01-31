using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointMove : MonoBehaviour
{
    [Header("Important")]
    [SerializeField] private Transform _refLeg;
    [SerializeField] private Transform _targetLeg;
    [SerializeField] private Rigidbody _rigidbodyPlayer;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _sphereRadius;
    [SerializeField] private float _maxDistanceShphereCast;
    [SerializeField] private float _flaotAnticipation;

    [Header("Donnée")]
    [SerializeField] private bool _Contact = false;
    [SerializeField] private bool _Once = false;
    [SerializeField] private bool _Back = false;
    [SerializeField] private float _time;
    [SerializeField] private Vector3 _PointOfHit = Vector3.zero;
    [SerializeField] private Vector3 _Déplacement;
    [SerializeField] private float _timeCouroutine;
    // Start is called before the first frame update
    private void Update() 
    {
        Déplacement();
        Debug.Log("Fixed");
        RaycastHit _hit;
        Vector3 RefPos = _refLeg.position + _rigidbodyPlayer.velocity * _flaotAnticipation + _Déplacement;
        if(Physics.SphereCast(RefPos,_sphereRadius,-transform.up,out _hit,_maxDistanceShphereCast,_layerMask))
        {
            Debug.Log(_hit.point);
            _Contact = true;
            _PointOfHit = _hit.point;
            
        }
        if(_Contact && Vector3.Distance(_targetLeg.position,_PointOfHit) > 0.15f)
        {
            _targetLeg.position = _PointOfHit;
            // _time += Time.deltaTime;
            // Vector3.Lerp(_targetLeg.position,_PointOfHit,_time);
        }
        if(_time > 1.5f)
        {
            _time = 0;
        }
    }
    private IEnumerator WaitFor()
    {
        yield return new WaitForSeconds(_timeCouroutine);
        Debug.Log("Active");
        if(_Déplacement.z < 1 && _Once == false)
        {
            _Déplacement = _Déplacement + new Vector3(0,0,1);
            _Once = true;
            _Back = true;
        }
    }
    private void Déplacement()
    {
        if(_Déplacement.z > 0 && _Back == false)
        {
            _Déplacement.z = _Déplacement.z - Time.deltaTime/2;
            StartCoroutine(WaitFor());  
        }      
        if(_Déplacement.z < 0)
        {
            _Once = false;
        }
        if(_Back == true)
        {
            _Déplacement.z = _Déplacement.z + Time.deltaTime/2;        
        }
        if(_Back == true && _Déplacement.z > 1)
        {
            _Back = false;
        }
    }
    private void OnDrawGizmos() 
    {
        if(_Contact == true)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(_PointOfHit,0.05f);
        }
        else
        {
            return;
        }        
    }
}

