using UnityEngine;

public class HumanCharacter : Character
{
    public override float DodgeSpeed => 20f;

    public override void SpecialAbility(GameObject obstacle, Transform playerTransform)
    {
        Animator policeWomen = obstacle.GetComponent<Animator>();

        if (policeWomen == null)
        {
            return;
        }

        policeWomen.SetBool("Back", true);
    }
}