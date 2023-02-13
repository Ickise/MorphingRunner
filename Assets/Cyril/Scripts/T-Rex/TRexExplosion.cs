using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRexExplosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) 
    {
        Debug.Log("Tigger");
        if(!other.CompareTag("Collider"))
        {
            return;
        }
        GameObject Obstacle = other.gameObject;
        Rigidbody ObstacleRigidbody = Obstacle.GetComponent<Rigidbody>();
        ObstacleRigidbody.AddExplosionForce(20000,transform.position,5,30);
        Debug.Log(ObstacleRigidbody.name);
    }
}
