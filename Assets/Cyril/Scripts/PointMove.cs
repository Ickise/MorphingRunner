using System.Collections;
using UnityEngine;
public class PointMove : MonoBehaviour
{
    [Header("Important")]
    [SerializeField] private Transform _refLeg;
    public Transform _targetLeg;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _sphereRadius;
    [SerializeField] private float _maxDistanceShphereCast;
    [SerializeField] private float _flaotAnticipation;

    [Header("Donnée")]
    private bool _Contact = false;
    [SerializeField] private bool _Once = true;
    public bool _Jambe = false;
    private float _time;
    private Vector3 _PointOfHit = Vector3.zero;
    [SerializeField] private Vector3 _PointOfHitAdd = Vector3.zero;
    private Vector3 Déplacement = Vector3.zero;
    private Vector3 _basePosition = Vector3.zero;
    private Vector3 _newPosition = Vector3.zero;
    private Vector3 _VectorUp = Vector3.zero;
    [SerializeField] private Vector3 _VectorCorection = Vector3.zero;
    [SerializeField] private PointMove _scriptPointMove;
    [SerializeField] private float lerpSpeed;
    [SerializeField] private AnimationCurve _animationcurveUp;
    private void Update()
    {
        if (_Once == false)
        {
            StartCoroutine(LerpLoopCoroutine());
        }
        Debug.Log("Fixed");
        RaycastHit _hit;
        Vector3 RefPos = (_refLeg.position + _VectorCorection + Déplacement) * _flaotAnticipation;
        if (Physics.SphereCast(RefPos, _sphereRadius, -transform.up, out _hit, _maxDistanceShphereCast, _layerMask))
        {
            _Contact = true;
            _PointOfHit = _hit.point + _VectorUp;
        }
        if (_Contact && Vector3.Distance(_targetLeg.position, _PointOfHit) > 0.15f)
        {
            _targetLeg.position = _PointOfHit + _PointOfHitAdd;
        }
        if (_time > 1.5f)
        {
            _time = 0;
        }
    }
    private IEnumerator LerpLoopCoroutine()
    {
        if (_Once == false && _Jambe)
        {
            _basePosition = transform.position + new Vector3(0, 0, -50);
            _newPosition = new Vector3(transform.position.x, transform.position.y, -40);
            _Once = true;
        }
        while (_Jambe)
        {
            float elapsedTime = 0f;
            while (elapsedTime < lerpSpeed)
            {
                
                Déplacement = Vector3.Lerp(_basePosition , _newPosition, elapsedTime / lerpSpeed);
                _VectorUp = _animationcurveUp.Evaluate(elapsedTime / lerpSpeed) * new Vector3(0,5,0);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            yield return new WaitForSeconds(0.1f);
            _scriptPointMove._Jambe = true;
            elapsedTime = 0f;
            while (elapsedTime < lerpSpeed)
            {
                _scriptPointMove._Jambe = true;
                Déplacement = Vector3.Lerp(_newPosition, _basePosition, elapsedTime / lerpSpeed);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            yield return new WaitForSeconds(0.1f);
            _Jambe = false;
            _Once = false;
            Debug.Log(_scriptPointMove._Jambe);
        }
    }
    private void OnDrawGizmos()
    {
        if (_Contact == true)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(_PointOfHit, 0.05f);
        }
        else
        {
            return;
        }
    }
}

