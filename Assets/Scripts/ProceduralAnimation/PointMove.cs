using System.Collections;
using UnityEngine;

public class PointMove : MonoBehaviour
{
    // Problème de nomenclature et de lissibilité.
    // Je pense même que cela était possible de rendre ce script, animationQueu et ProcéduralPoint plus générique.
    // En encapsulant les comportements communs dans une classe mère et en dérivant les spécificités dans des classes filles.

    [Header("Important")] [SerializeField] private Transform _refLeg;
    public Transform _targetLeg;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _sphereRadius;
    [SerializeField] private float _maxDistanceShphereCast;
    [SerializeField] private float _flaotAnticipation;

    [Header("Donnée")] private bool _Contact = false;
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
    [SerializeField] private CameraShake _cameraShake;
    [SerializeField] private float X;

    private void Update()
    {
        if (_cameraShake == null) // A chaque frame, nous vérifions si "_cameraShake" est null. Nous aurions pu utiliser un return.
        {
            GameObject Camera = GameObject.FindGameObjectWithTag("MainCamera");
            _cameraShake = Camera.GetComponent<CameraShake>();
            // Nous aurions dû faire cette assignation une seule fois dans Start() pour éviter de chercher la caméra à chaque Update().
        }

        if (_Once == false) 
        {
            StartCoroutine(LerpLoopCoroutine());
            // Cette ligne risque de lancer plusieurs coroutines simultanément si "_Once" est faux pendant plusieurs frames.
            // Nous aurions dû passer "_Once" à true immédiatement après avoir appelé la coroutine pour éviter cela.
        }

        RaycastHit _hit;
        Vector3 RefPos = (_refLeg.position + _VectorCorection + Déplacement) * _flaotAnticipation;
        if (Physics.SphereCast(RefPos, _sphereRadius, -transform.up, out _hit, _maxDistanceShphereCast, _layerMask))
        {
            _Contact = true;
            _PointOfHit = _hit.point + _VectorUp;
        }

        if (_Contact && Vector3.Distance(_targetLeg.position, _PointOfHit) > 0.15f) 
        {
            _targetLeg.position = _PointOfHit + _PointOfHitAdd + new Vector3(X, 0, 0);
            // Nous modifions directement la position sans interpolation, ce qui peut créer un mouvement saccadé.
            // Nous aurions pu utiliser Vector3.Lerp pour rendre le déplacement de la jambe plus smooth et agréable.
        }

        if (_time > 1.5f) 
        {
            _time = 0; 
            // _time est utilisé et rénitialisé alors que nous ne l'utilisons pas ailleurs. Nous pouvons le supprimer.
        }

        Horizontal();
    }

    private void Horizontal()
    {
        // Nous appelons SideManager.instance.GetHorizontalSide() à chaque frame dans Update(), alors qu’il pourrait être appelé seulement
        // lorsqu'un changement de direction est détecté.
        switch (SideManager.instance.GetHorizontalSide())
        {
            case HorizontalSide.Left:
                X = -2.5f;
                break;
            case HorizontalSide.Right:
                X = 2.5f;
                break;
            case HorizontalSide.Mid:
                X = 0;
                break;
        }
    }

    private IEnumerator LerpLoopCoroutine()
    {
        if (_Once == false && _Jambe) 
        {
            _basePosition = transform.position + new Vector3(0, 0, -50);
            _newPosition = new Vector3(transform.position.x, transform.position.y, -40);
            _Once = true; // _Once est passé à true ici alors que nous le vérifions avant d’appeler cette coroutine.
        }

        while (_Jambe)
        {
            float elapsedTime = 0f;
            while (elapsedTime < lerpSpeed)
            {
                Déplacement = Vector3.Lerp(_basePosition, _newPosition, elapsedTime / lerpSpeed);
                _VectorUp = _animationcurveUp.Evaluate(elapsedTime / lerpSpeed) * new Vector3(0, 5, 0);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            yield return new WaitForSeconds(0.1f);
            StartCoroutine(_cameraShake.Shake(0.1f, 0.1f)); 
            // Nous lançons une nouvelle coroutine Shake() à chaque passage ici, ce qui peut causer une accumulation.
            // Il aurait fallu vérifier si une autre instance de la coroutine était déjà en cours pour éviter cela.

            _scriptPointMove._Jambe = true; // Modifier directement une variable public d’un autre script est une mauvaise chose.
            // Nous aurions dû encapsuler cela dans une méthode publique du script concerné.

            elapsedTime = 0f;
            while (elapsedTime < lerpSpeed)
            {
                _scriptPointMove._Jambe = true; // Redondance de cette ligne dans la même boucle.
                Déplacement = Vector3.Lerp(_newPosition, _basePosition, elapsedTime / lerpSpeed);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            yield return new WaitForSeconds(0.1f);
            _Jambe = false;
            _Once = false;
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
            // Le return est inutile ici.
        }
    }
}