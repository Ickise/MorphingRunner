using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    // Script très court mais qui permet de respecter le Single Responsability Principle.

    [SerializeField, Header("Settings")] private LayerMask isObstacleLayer;
    [SerializeField] private TransformationManager transformationManager;

    private DNAManager dnaManager;

    private GameManager gameManager;

    private ScoreManager scoreManager;

    private UIManager uiManager;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        dnaManager = DNAManager.instance;
        gameManager = GameManager.instance;
        scoreManager = ScoreManager.instance;
        uiManager = UIManager.instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Ici je détecte les objets qui ont uniquement le layer IsObstacle pour lancer le GameOver.
        // Je pense que c'est la manière la plus optimisée pour ne pas utiliser de CompareTag ou ne pas avoir juste un script Obstacle sur tous les objets.
        if (((1 << other.gameObject.layer) & isObstacleLayer) != 0)
        {
            if (transformationManager.GetCurrentCharacter() is TRexCharacter tRexCharacter)
            {
                tRexCharacter.SpecialAbility(other.gameObject, transform);
            }
            else
            {
                gameManager.GameOver();
            }
        }
        
        if (transformationManager.GetCurrentCharacter() is HumanCharacter humanCharacter)
        {
            humanCharacter.SpecialAbility(other.gameObject, transform);
        }

        // Ici je détecte uniquement les ADN pour récupérer un ADN précis.
        DNAType dnaType = other.GetComponent<DNAType>();
        if (dnaType != null)
        {
            dnaManager.AddDNA(dnaType.GetDNAType());
            scoreManager.AddScore(100);
        }
    }
}