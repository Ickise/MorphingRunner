using System;
using System.Collections.Generic;
using UnityEngine;

public class TransformationManager : MonoBehaviour
{
    // Séparation claire, ce script ne s'occupe que de la gestion des transformations.
    [SerializeField, Header("References")] private PlayerMovement playerMovement;
    [SerializeField] private SlowMotion slowMotion;

    private Character currentCharacter;

    private DNAManager dnaManager;

    // Stocke toutes les transformations en mémoire pour éviter de recréer les objets à chaque fois. Je pourrais tout simplement ajouter d'autres transformations si besoin.
    private Dictionary<Type, Character> characters = new Dictionary<Type, Character>
    {
        { typeof(TRexCharacter), new TRexCharacter() },
        { typeof(AlienCharacter), new AlienCharacter() },
        { typeof(HumanCharacter), new HumanCharacter() }
    };

    // J'utilise une méthode pour définir le personnage actuel. Je pourrais la modifier pour rajouter le changement d'autres paramètres tel que le mesh, etc...
    // Définit le personnage actuel en utilisant le dictionnaire
    private void SetCharacter<T>() where T : Character
    {
        currentCharacter = characters[typeof(T)];
        playerMovement.SetDodgeSpeed(currentCharacter.Speed);
    }

    private void Start()
    {
        dnaManager = DNAManager.instance;
    }

    // J'ai centralisé la gestion des transformations via une classe Character dont hérité les classes TRexCharacter, AlienCharacter et HumanCharacter.
    // Cela permettra de rajouter facilement d'autres transformations.

    //Cette méthode permet d'évite de recréer les objets inutilement à chaque transformation pour économiser de la mémoire.

    public void AlienTransformation() => TransformInto<AlienCharacter>(DNAType.DnaType.AlienDNA);
    public void TRexTransformation() => TransformInto<TRexCharacter>(DNAType.DnaType.TRexDNA);
    public void HumanTransformation() => TransformInto<HumanCharacter>(DNAType.DnaType.HumanDNA);

    // J'ai utilisé une generic pour optimiser. 
    private void TransformInto<T>(DNAType.DnaType dnaType) where T : Character
    {
        if (!dnaManager.ConsumeDNA(dnaType, 5))
        {
            slowMotion.DeactivateSlowMotion();
            return;
        }

        SetCharacter<T>();
        slowMotion.DeactivateSlowMotion();
    }
}