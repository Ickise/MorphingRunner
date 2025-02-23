using System;
using UnityEngine;

public class TransformationManager : MonoBehaviour
{
    [SerializeField, Header("References")]  private PlayerMovement playerMovement;
    [SerializeField] private SlowMotion slowMotion;

    private Character currentCharacter;

    private void SetCharacter(Character character)
    {
        currentCharacter = character;
        playerMovement.SetSpeed(currentCharacter.Speed);
    }

    public void TRexTransformation()
    {
        Debug.Log("Trex");
        SetCharacter(new TRexCharacter());
        slowMotion.DeactivateSlowMotion();
    }

    public void AlienTransformation()
    {
        Debug.Log("alien");
        SetCharacter(new AlienCharacter());
        slowMotion.DeactivateSlowMotion();
    }

    public void HumanTransformation()
    {
        Debug.Log("human");
        SetCharacter(new HumanCharacter());
        slowMotion.DeactivateSlowMotion();
    }
}