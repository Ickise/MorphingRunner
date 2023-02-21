using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationQueu : MonoBehaviour
{
    [SerializeField] private  List<Transform>  _listQeu; 
    [SerializeField] private Vector3 Base;
    [SerializeField] private Vector3 NoBase;
    [SerializeField] private Vector3 Rotate;
    [SerializeField] private bool _Jambe;
    [SerializeField] private bool _Once;
    [SerializeField] private float lerpSpeed;
    [SerializeField] private float _div;
    void Update()
    {
        if(_Once) StartCoroutine(Slerp());
        for (int i = 0; i < _listQeu.Count; i++)
        {
            Debug.Log(Rotate + "" + _listQeu[i]);
            if(i == 5)
            {
                _listQeu[5].Rotate(-Rotate,Space.World);                
            }
            else
            {
                _listQeu[i].Rotate(Rotate,Space.World);
            }
        }
    }
    private IEnumerator Slerp()
    {
        while (_Jambe)
        {
            Debug.Log("Jambe");
            float elapsedTime = 0f;
            _Once = false;
            while (elapsedTime < lerpSpeed)
            {
                Rotate = Vector3.Lerp(Base,NoBase,  elapsedTime/lerpSpeed);
                elapsedTime += Time.smoothDeltaTime; 
                yield return null;
            }
            yield return new WaitForSeconds(0.1f);
            elapsedTime = 0f;
            while (elapsedTime < lerpSpeed)
            {
                Rotate = Vector3.Lerp(NoBase,Base,  elapsedTime/lerpSpeed);
                Rotate = -Rotate; 
                elapsedTime += Time.smoothDeltaTime;
                yield return null;
            }
            Rotate = new Vector3 (0,0,0);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
