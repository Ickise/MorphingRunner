using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    // Les fonctionnalités de ce script se trouvaient auparavant dans TransformationChoices. Il est très court mais il permet de respecter
    // le Single Responsibility Principle.

    [SerializeField, Header("Settings")] private LayerMask isObstacleLayer;

    [SerializeField] private TransformationManager transformationManager;

    private DNAManager dnaManager;

    private GameManager gameManager;

    private ScoreManager scoreManager;

    private AudioManager audioManager;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        // Initialisations de toutes les instances.
        dnaManager = DNAManager.instance;
        gameManager = GameManager.instance;
        scoreManager = ScoreManager.instance;
        audioManager = AudioManager.instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        PachySpecificAbilityOnCollsion(other);
        HumanSpecificAbilityOnCollision(other);
        GetDNAOnCollision(other);
    }

    private void PachySpecificAbilityOnCollsion(Collider other)
    {
        // Nous vérifions si le collider possède le layer que nous souhaitons.

        if (((1 << other.gameObject.layer) & isObstacleLayer) != 0)
        {
            // Si actuellement le joueur a la forme du Pachy, nous lançons sa capacité spéciale sur les obstacles.
            if (transformationManager.GetCurrentCharacter() is PachyCharacter pachyCharacter)
            {
                pachyCharacter.SpecialAbility(other.gameObject, transform);
            }
            else
            {
                // Si le joueur n'est pas un Pachy, s'il n'est pas invincible, alors le joueur perd la partie.
                gameManager.GameOver();
            }
        }
    }

    private void HumanSpecificAbilityOnCollision(Collider other)
    {
        // Si actuellement le joueur est sous la forme humaine, nous exécutons sa capacité spéciale.

        if (transformationManager.GetCurrentCharacter() is HumanCharacter humanCharacter)
        {
            humanCharacter.SpecialAbility(other.gameObject, transform);
        }
    }

    private void GetDNAOnCollision(Collider other)
    {
        // Nous vérifions si l'objet a le component DNAType pour récupérer un ADN spécifique.
        
        DNAType dnaType = other.GetComponent<DNAType>();
        if (dnaType != null)
        {
            // Si l'objet possède le component, cela joue un son et ajoute l'ADN au stock correspondant et du score.
            audioManager.PlaySound(1);
            dnaManager.AddDNA(dnaType.GetDNAType());
            scoreManager.AddScore(100);
        }
    }
}