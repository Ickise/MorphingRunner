using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class TRexExplosion : MonoBehaviour
{
    [SerializeField] Rigidbody ObstacleRigidbody;
    [SerializeField] AnimationCurve _animationCurve;
    Collider Coco;
    [SerializeField] float _time;
    private void OnTriggerEnter(Collider other) 
    {
        Debug.Log("Tigger");
        if(!other.CompareTag("Collider"))
        {
            return;
        }
        GameObject Obstacle = other.gameObject;
        ObstacleRigidbody = Obstacle.GetComponent<Rigidbody>();
        ObstacleRigidbody.AddExplosionForce(300,transform.position + new Vector3(0,0,-5),80,100,ForceMode.Force);
        ObstacleRigidbody.useGravity = true;
        _time = 0;
        StartCoroutine(WaitFor(other));
    }
    private void Update() 
    {
        if(ObstacleRigidbody == null) return;
        _time += Time.deltaTime;
        float A = _animationCurve.Evaluate(_time);
        ObstacleRigidbody.AddForce(new Vector3(0,A,20),ForceMode.Force);
    }
    private IEnumerator WaitFor(Collider col)
    {
        yield return new WaitForSeconds(1);
        col.isTrigger = false;
        ObstacleRigidbody.useGravity = true;
        ObstacleRigidbody.mass = 1500;
    }
}
