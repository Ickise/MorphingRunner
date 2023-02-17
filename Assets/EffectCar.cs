using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectCar : MonoBehaviour
{
    [SerializeField] GameObject _prefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision other) 
    {
        Instantiate(_prefab,transform.position,Quaternion.identity);        
    }
}
