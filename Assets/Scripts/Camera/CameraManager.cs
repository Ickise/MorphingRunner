using System.Collections;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform _PointBase;
    [SerializeField] private Transform _PointDown;
    [SerializeField] private Transform _PointTrex;
    [SerializeField] private CameraShake _cameraShake;
    [SerializeField] private float _lerpSpeed;
    public bool _lerpBoolReverse = true; // Nous avions créé plusieurs bool pour suivre différents états de la caméra.
    // Trop de bool peuvent être difficiles à gérer à force. Ces bool pourraient être remplacés par un enum, rendant le code plus lisible et extensible.
    public bool _lerpBoolUp = false;
    public bool _lerpBoolTRex = true;
    public bool _lerpBoolTRexReverse = true;
    public bool _lerpBool;
    private bool _once = true;
    private bool _onceV2 = false;

    private void Update() 
    {
        // Ici nous avons une redondance : vérification de _lerpBool dans chaque condition. Il est inutile de vérifier ça à chaque fois.
        if (_lerpBool) 
        {
            if (_lerpBoolReverse && _once) 
            {
                StartCoroutine(LerpCamera(_PointBase.position, _PointDown.position));
            }
            if (_lerpBoolUp && _onceV2) 
            {
                StartCoroutine(LerpCam(_PointDown.position, _PointBase.position));
            }
            if (_lerpBoolTRex && _once) 
            {
                StartCoroutine(LerpCamera(_PointBase.position, _PointTrex.position));
                StartCoroutine(WaitforSeconde());
            }
            if (_lerpBoolTRexReverse && _onceV2) 
            {
                StartCoroutine(LerpCam(_PointTrex.position, _PointBase.position));
            }
        }
    }

    // La coroutine permet d'attendre X secondes, mais elle n'est pas utilisée correctement.
    public IEnumerator WaitforSeconde()
    {
        yield return new WaitForSeconds(2.5f);
        StartCoroutine( _cameraShake.Shake(0.5f,0.1f));  
    }

    // Cette coroutine est utilisée pour effectuer un mouvement interpolé de la caméra.
    public IEnumerator LerpCamera(Vector3 Base, Vector3 Down)
    {
        float elapsedTime = 0f;
        _once = false; // Cela permet d'empêcher de relancer la coroutine immédiatement.
        // La condition (_lerpBoolReverse || _lerpBoolTRex) n'est pas nécessaire si j'ai un seul bool pour le contrôle des différents états de la caméra.
        while (elapsedTime < _lerpSpeed && (_lerpBoolReverse || _lerpBoolTRex))
        {
            transform.position = Vector3.Lerp(Base, Down, elapsedTime / _lerpSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _onceV2 = true; // Une fois la transition terminée, l'autre condition peut se déclencher.
        _lerpBoolReverse = false; // J'aurai pu limiter le nombre de transitions possibles en réinitialisant les bools après le mouvement.
        yield return new WaitForSeconds(1);
    }

    // Coroutine similaire à la précédente, mais dans l'autre direction.
    public IEnumerator LerpCam(Vector3 Base, Vector3 Down)
    {
        float elapsedTime = 0f;
        _onceV2 = false;
        while (elapsedTime < _lerpSpeed)
        {
            transform.position = Vector3.Lerp(Base, Down, elapsedTime / _lerpSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _once = true;
        _lerpBoolTRex = false;
        _lerpBoolTRexReverse = false;
        _lerpBool = false;
    }
}
