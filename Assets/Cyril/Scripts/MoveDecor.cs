using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDecor : MonoBehaviour
{
   [SerializeField] private float _speed = 0;
    
    void Update()
    {
        transform.position = transform.position + (new Vector3(0,0,-1) * Time.deltaTime * _speed);
        if(transform.position.z < -40)
        {
            //Destroy(this.gameObject,0);
        }     
    }
    public void SetSpeedAdd(float _speedAdd)
    {
        _speed =_speed + _speedAdd;
        Debug.Log(_speed + "Chunk");
    }
    public void SetSpeed(float speed)
    {
        _speed = speed;
        Debug.Log(speed + "Point");
    }
}
