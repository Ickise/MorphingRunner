using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotaionHelicopter : MonoBehaviour
{
    [SerializeField] float lerpSpeed;
    [SerializeField] Vector3 _vectorBase;
    [SerializeField] Vector3 _vectorBase2;
    [SerializeField] Vector3 Rotate;
    [SerializeField] bool _Jambe;
    [SerializeField] bool _Once;
    void Update()
    {
        transform.localRotation = Quaternion.Euler(0,Rotate.y,0);
        if(_Once == true)
        {
            StartCoroutine(LerpFloat());
        }
    }
    private IEnumerator LerpFloat()
    {
        while (_Jambe)
        {
            float elapsedTime = 0f;
            _Once = false;
            while (elapsedTime < lerpSpeed)
            {
                Rotate = Vector3.Lerp(_vectorBase,_vectorBase2,  elapsedTime/lerpSpeed);
                elapsedTime += Time.smoothDeltaTime; 
                yield return null;
            }
            yield return new WaitForSeconds(0.1f);
            elapsedTime = 0f;
            while (elapsedTime < lerpSpeed)
            {
                Rotate = Vector3.Lerp(_vectorBase2,_vectorBase,  elapsedTime/lerpSpeed);
                elapsedTime += Time.smoothDeltaTime;
                yield return null;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
