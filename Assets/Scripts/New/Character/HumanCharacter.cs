using UnityEngine;

public class HumanCharacter : Character
{
    public override float DodgeSpeed => 20f;

    public override void SpecialAbility(GameObject obstacle, Transform playerTransform)
    {
        if (obstacle.TryGetComponent(out PoliceGroup policeGroup))
        {
            policeGroup.ActivateBack();
        }
    }
}