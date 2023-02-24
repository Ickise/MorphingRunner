using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectCar : MonoBehaviour
{
    [SerializeField] GameObject _prefab;
    private void OnCollisionEnter(Collision other) 
    {
        GameObject Inst = Instantiate(_prefab,transform.position,Quaternion.identity); 
        StartCoroutine(WaitFor(Inst));       
    }
    private IEnumerator WaitFor(GameObject Inst)
    {
        yield return new WaitForSeconds(5f);
        Destroy(Inst,0);
    }
}
