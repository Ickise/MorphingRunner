using UnityEngine;

public class AlienCharacter : Character
{
    public override float DodgeSpeed => 15f;

    public override void SpecialAbility(GameObject obstacle, Transform playerTransform)
    {
        // Specific ability
    }
}