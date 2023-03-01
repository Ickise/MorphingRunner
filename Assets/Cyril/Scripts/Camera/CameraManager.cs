using System.Collections;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform _PointBase;
    [SerializeField] private Transform _PointDown;
    [SerializeField] private Transform _PointTrex;
    [SerializeField] private CameraShake _cameraShake;
    [SerializeField] private float _lerpSpeed;
    public bool _lerpBoolReverse = true;
    public bool _lerpBoolUp = false;
    public bool _lerpBoolTRex = true;
    public bool _lerpBoolTRexReverse = true;
    public bool _lerpBool;
    private bool _once = true;
    private bool _onceV2 = false;
    private void Update() 
    {
        if(_lerpBoolReverse && _once && _lerpBool == true)  StartCoroutine(LerpCamera(_PointBase.position,_PointDown.position));
        if(_lerpBoolUp && _onceV2 && _lerpBool == true) StartCoroutine(LerpCam(_PointDown.position,_PointBase.position));
        if(_lerpBoolTRex && _once && _lerpBool == true)
        {
            StartCoroutine(LerpCamera(_PointBase.position,_PointTrex.position));
            StartCoroutine(WaitforSeconde());  
        } 
        if(_lerpBoolTRexReverse && _onceV2 && _lerpBool == true) StartCoroutine(LerpCam(_PointTrex.position,_PointBase.position));
    }
    public IEnumerator WaitforSeconde()
    {
        yield return new WaitForSeconds(2.5f);
        //StartCoroutine( _cameraShake.Shake(0.5f,0.1f));  
    }
    public IEnumerator LerpCamera(Vector3 Base , Vector3 Down)
    {
        float elapsedTime = 0f;
        _once = false;
        while(elapsedTime < _lerpSpeed && (_lerpBoolReverse || _lerpBoolTRex))
        {
            transform.position = Vector3.Lerp(Base,Down, elapsedTime / _lerpSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _onceV2 = true;
        _lerpBoolReverse = false;
        yield return new WaitForSeconds(1);
    }
    public IEnumerator LerpCam(Vector3 Base , Vector3 Down)
    {
        float elapsedTime = 0f;
        _onceV2 = false;
        while(elapsedTime < _lerpSpeed)
        {
            transform.position = Vector3.Lerp(Base,Down, elapsedTime / _lerpSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _once = true;
        _lerpBoolTRex = false;
        _lerpBoolTRexReverse = false;
        _lerpBool = false;
    }
}
