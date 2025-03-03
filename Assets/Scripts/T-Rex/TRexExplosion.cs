using UnityEngine;
using System.Collections;

public class TRexExplosion : MonoBehaviour
{
    // Ce script permettait de faire l'abilité spécial du TRex.
    // Problème de nomenclature et de lissibilité.
    [SerializeField] Rigidbody ObstacleRigidbody;
    
    // Nous avions utilisé un simple OnTriggerEnter, mais avec des CompareTag, ce qui n'est pas optimisé.
    private void OnTriggerEnter(Collider other) 
    {
        if(!other.CompareTag("Collider") && !other.CompareTag("Sign"))
        {
            return;
        }
        // Nous faisions en sorte de récupérer l'objet, son rigidbody et de lui mettre une force d'impulsion + une coroutine pour qu'il retombe. 
        // Cela donnait un effet sympa, comme ci le TRex avait mis un coup de pieds dans l'objet.
        Scoring.scoring.score += 10;
        GameObject Obstacle = other.gameObject;
        ObstacleRigidbody = Obstacle.GetComponent<Rigidbody>();
        ObstacleRigidbody.AddForce(transform.up * 35 + transform.forward * 30,ForceMode.Impulse); // Ancien commentaire : Forward inverser !!!
        ObstacleRigidbody.AddRelativeTorque(Vector3.right * 20, ForceMode.Impulse); 
        ObstacleRigidbody.useGravity = true;
        StartCoroutine(WaitFor(other));
    }
    private IEnumerator WaitFor(Collider col)
    {
        yield return new WaitForSeconds(0.2f);
        col.isTrigger = false;
        ObstacleRigidbody.useGravity = true;
        // L'hardcode était très présent...
        ObstacleRigidbody.mass = 1500;
    }
}
