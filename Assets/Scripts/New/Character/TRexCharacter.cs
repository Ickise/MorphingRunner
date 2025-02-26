using UnityEngine;

public class TRexCharacter : Character
{
    public override float DodgeSpeed => 10f;

    public override void SpecialAbility(GameObject obstacle, Transform playerTransform)
    {
        Rigidbody obstacleRigidbody = obstacle.GetComponent<Rigidbody>();

        if (obstacleRigidbody == null)
        {
            obstacleRigidbody = obstacle.AddComponent<Rigidbody>();
            obstacleRigidbody.useGravity = false;
        }

        obstacleRigidbody.AddForce(playerTransform.up * 35 + playerTransform.forward * 30, ForceMode.Impulse);
        obstacleRigidbody.AddRelativeTorque(Vector3.right * 20, ForceMode.Impulse);
    }
}