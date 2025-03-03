using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformationManager : MonoBehaviour
{
    // Ce script remplace TransformationChoices et gère les transformations du joueur en différentes formes.
    
    [SerializeField, Header("References")] private PlayerMovement playerMovement;
    
    [SerializeField] private SlowMotion slowMotion;

    [SerializeField] private GameObject humanObject;
    [SerializeField] private GameObject alienObject;
    [SerializeField] private GameObject pachyObject;

    [SerializeField, Header("Settings")] private float transformationDuration = 10f;

    [SerializeField] private int numberOfDNAToConsume = 5;
    
    private Character currentCharacter;

    private DNAManager dnaManager;

    private List<GameObject> allCharacterObjects;

    // Dictionnaire pour stocker les types de transformations disponibles (ex: Alien, Pachy, Human) et leurs classes associées.
    private Dictionary<Type, Character> characters = new Dictionary<Type, Character>
    {
        { typeof(PachyCharacter), new PachyCharacter() }, // Transformation en Pachy
        { typeof(AlienCharacter), new AlienCharacter() }, // Transformation en Alien
        { typeof(HumanCharacter), new HumanCharacter() }  // Transformation en Humain
    };

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        // Initialisation des données.
        dnaManager = DNAManager.instance;

        // Nous attribuons le personnage correspondant à chaque prefab.
        characters[typeof(HumanCharacter)].SetCharacterObject(humanObject);
        characters[typeof(AlienCharacter)].SetCharacterObject(alienObject);
        characters[typeof(PachyCharacter)].SetCharacterObject(pachyObject);

        allCharacterObjects = new List<GameObject> { humanObject, alienObject, pachyObject };

        SetCharacter<AlienCharacter>();
        currentCharacter.ChangeSkin(allCharacterObjects);
    }

    // Méthode générique pour définir le Character actuel. Cette méthode peut facilement être modifiée pour inclure des changements de mesh,
    // de textures, etc... Elle permet de centraliser la logique du changement de personnage.
    private void SetCharacter<T>() where T : Character
    {
        currentCharacter = characters[typeof(T)];
        playerMovement.SetDodgeSpeed(currentCharacter.DodgeSpeed); // Nous assignons la vitesse de dodge selon fonction le Character.

        // Nous changeons le mesh du personnage.
        currentCharacter.ChangeSkin(allCharacterObjects);
    }

    // Ces méthodes permettent de transformer le joueur en différents personnages. J'ai assigné ces fonctions aux différents boutons.
    public void AlienTransformation() => TransformInto<AlienCharacter>(DNAType.DnaType.AlienDNA);
    public void PachyTransformation() => TransformInto<PachyCharacter>(DNAType.DnaType.PachyDNA);
    public void HumanTransformation() => TransformInto<HumanCharacter>(DNAType.DnaType.HumanDNA);

    // Cette méthode générique permet de réaliser la transformation.
    private void TransformInto<T>(DNAType.DnaType dnaType) where T : Character
    {
        // Si le joueur n'a pas suffisamment d'ADN, la transformation échoue.
        if (!dnaManager.ConsumeDNA(dnaType, numberOfDNAToConsume))
        {
            slowMotion.DeactivateSlowMotion();
            return;
        }

        // Sinon, nous effectuons la transformation en appelant SetCharacter pour changer le personnage.
        SetCharacter<T>();
        slowMotion.DeactivateSlowMotion();

        // Si la transformation n'est pas AlienCharacter, alors nous lançons une coroutine pour retourner dans la forme Alien.
        if (typeof(T) != typeof(AlienCharacter))
        {
            StartCoroutine(RevertToAlienAfterDuration());
        }
    }

    // Cela permet de savoir quel forme le joueur a actuellement. Nous l'utilisons notamment dans PlayerCollision pour utiliser les SpecificAbility lors
    // de collision avec d'autre objet.
    public Character GetCurrentCharacter()
    {
        return currentCharacter;
    }

    // La coroutine permet de revenir automatiquement à la forme Alien après un certain temps.
    private IEnumerator RevertToAlienAfterDuration()
    {
        yield return new WaitForSeconds(transformationDuration);
        
        SetCharacter<AlienCharacter>();
    }
}
