using System.Collections.Generic;
using UnityEngine;

public abstract class Character
{
    // J'ai créé une classe abstraite Character dont héritent les classes TRexCharacter, AlienCharacter et HumanCharacter.
    // Cela permet de centraliser les caractéristiques communes à tous les personnages.
    public abstract float DodgeSpeed { get; }
    public abstract void SpecialAbility(GameObject obstacle, Transform playerTransform);

    protected GameObject characterObject;

    public void SetCharacterObject(GameObject obj)
    {
        characterObject = obj;
    }
    
    public virtual void ChangeSkin(List<GameObject> allCharacters)
    {
        foreach (var obj in allCharacters)
        {
            obj.SetActive(false);
        }

        if (characterObject != null)
        {
            characterObject.SetActive(true);
        }
    }
}