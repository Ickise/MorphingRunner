using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class TRexExplosion : MonoBehaviour
{
    [SerializeField] Rigidbody ObstacleRigidbody;
    private void OnTriggerEnter(Collider other) 
    {
        if(!other.CompareTag("Collider") && !other.CompareTag("Sign"))
        {
            return;
        }
        Scoring.scoring.score += 10;
        GameObject Obstacle = other.gameObject;
        ObstacleRigidbody = Obstacle.GetComponent<Rigidbody>();
        ObstacleRigidbody.AddForce(transform.up * 35 + transform.forward * 30,ForceMode.Impulse); // Forward inverser !!!
        ObstacleRigidbody.AddRelativeTorque(Vector3.right * 20, ForceMode.Impulse); 
        ObstacleRigidbody.useGravity = true;
        StartCoroutine(WaitFor(other));
    }
    private IEnumerator WaitFor(Collider col)
    {
        yield return new WaitForSeconds(0.2f);
        col.isTrigger = false;
        ObstacleRigidbody.useGravity = true;
        ObstacleRigidbody.mass = 1500;
    }
}
