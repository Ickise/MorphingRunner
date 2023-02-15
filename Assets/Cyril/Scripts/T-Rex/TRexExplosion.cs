using UnityEngine;

public class TRexExplosion : MonoBehaviour
{
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
