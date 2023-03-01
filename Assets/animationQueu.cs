using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationQueu : MonoBehaviour
{
    [SerializeField] private  List<Transform>  _listQeu; 
    [SerializeField] private float Base = 25f;
    [SerializeField] private float NoBase = -15f;
    [SerializeField] private float Rotate;
    [SerializeField] private bool _Jambe;
    [SerializeField] private bool _Once;
    [SerializeField] private float lerpSpeed = 5f;
    [SerializeField] private Transform _trexTete;

    void Update()
    {
        if(_Once) StartCoroutine(Slerp());
        for (int i = 0; i < _listQeu.Count; i++)
        {
            _listQeu[i].transform.localRotation = Quaternion.Euler(Rotate,-Rotate,0);
        }
        _trexTete.transform.localRotation = Quaternion.Euler(-Rotate * 2,Rotate,0);

    }
    private IEnumerator Slerp()
    {
        while (_Jambe)
        {
            float elapsedTime = 0f;
            _Once = false;
            while (elapsedTime < lerpSpeed)
            {
                Debug.Log(elapsedTime/lerpSpeed + "v1");
                Rotate =  Mathf.Lerp(Base,NoBase,elapsedTime/lerpSpeed);
                elapsedTime += Time.smoothDeltaTime; 
                yield return null;
            }
            yield return new WaitForSeconds(0.1f);
            elapsedTime = 0f;
            while (elapsedTime < lerpSpeed)
            {
                Debug.Log(elapsedTime/lerpSpeed + "v2");
                Rotate = Mathf.Lerp(NoBase,Base,elapsedTime/lerpSpeed);
                elapsedTime += Time.smoothDeltaTime;
                yield return null;
            }
            //Rotate = 0.2f;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
