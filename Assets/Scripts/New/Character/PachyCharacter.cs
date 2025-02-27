using UnityEngine;

public class PachyCharacter : Character
{
    public override float DodgeSpeed => 10f;

    private float forceValue = 10f;
    private float rotationValue = 20f;

    public override void SpecialAbility(GameObject obstacle, Transform playerTransform)
    {
        Rigidbody obstacleRigidbody = obstacle.GetComponent<Rigidbody>();

        if (obstacleRigidbody == null)
        {
            obstacleRigidbody = obstacle.AddComponent<Rigidbody>();
            obstacleRigidbody.useGravity = false;
        }

        obstacleRigidbody.AddForce(playerTransform.up * forceValue + playerTransform.forward * forceValue,
            ForceMode.Impulse);
        obstacleRigidbody.AddRelativeTorque(Vector3.right * rotationValue, ForceMode.Impulse);
        ScoreManager.instance.AddScore(100);
    }
}