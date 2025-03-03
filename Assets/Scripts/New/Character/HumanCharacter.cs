using UnityEngine;

public class HumanCharacter : Character
{
    public override float DodgeSpeed => 20f;

    // L'abilité spécifique du personnage.
    // L'humain permet de passer par un chemin plus simple et permettant de récupérer de l'ADN de Pachy. 
    public override void SpecialAbility(GameObject obstacle, Transform playerTransform)
    {
        if (obstacle.TryGetComponent(out PoliceGroup policeGroup))
        {
            policeGroup.ActivateBack();
        }
    }
}