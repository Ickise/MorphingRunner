using UnityEngine;

public class AlienCharacter : Character
{
    public override float DodgeSpeed => 15f;

    public override void SpecialAbility(GameObject obstacle, Transform playerTransform)
    {
        // L'abilité spécifique du personnage.
        // L'alien n'en a pas puisque c'est le personnage de base.
    }
}