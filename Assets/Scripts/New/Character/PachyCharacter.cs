using UnityEngine;

public class PachyCharacter : Character
{
    public override float DodgeSpeed => 10f;

    private float forceValue = 10f; 
    private float rotationValue = 20f; 

    // L'abilité spéciale du Pachy.
    // En gros, il devient invincible pendant un certain temps et tout ce qu'il touche est détruit et projeté en l'air.
    public override void SpecialAbility(GameObject obstacle, Transform playerTransform)
    {
        // Cela permet de récupérer le Rigidbody de l'objet touché. 
        Rigidbody obstacleRigidbody = obstacle.GetComponent<Rigidbody>();

        // Si l'obstacle n'a pas de Rigidbody, nous lui en donnons un pour pouvoir l'envoyer en l'air.
        if (obstacleRigidbody == null)
        {
            obstacleRigidbody = obstacle.AddComponent<Rigidbody>();
            obstacleRigidbody.useGravity = false; // On désactive la gravité pour qu'il ne puisse pas retomber directement.
        }

        // Nous appliquons une force pour le projeter vers l'avant et vers le haut.
        obstacleRigidbody.AddForce(playerTransform.up * forceValue + playerTransform.forward * forceValue,
            ForceMode.Impulse);

        // Nous appliquons une force de rotation pour avoir un effet réaliste.
        obstacleRigidbody.AddRelativeTorque(Vector3.right * rotationValue, ForceMode.Impulse);

        // Enfin, nous récompensons le joueur.
        ScoreManager.instance.AddScore(100);
    }
}