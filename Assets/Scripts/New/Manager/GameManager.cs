using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Ce script est un simple GameManager pour gérer l'état du jeu, comme afficher un GameOver, activer/désactiver les inputs, etc...
    // Il n'existait pas auparavant et centralise la gestion du gameplay principal, comme la gestion de l'UI, des entrées utilisateur et du son, etc...
    // Je ne sais pas si en terme d'optimisation, cela vaut le coup de faire une méthode pour chaque Singleton dans ce script et que la méthode
    // renvoie le Singleton en question pour éviter qu'au Start des autres scripts j'ai besoin de faire par exemple uiManager = UIManager.instance.
    // Je ne sais pas si cela optimise le jeu ou non, mais j'aurai pu le faire. 

    public static GameManager instance;

    [SerializeField, Header("References")] private InputManager inputManager;

    private bool gameIsOver;

    private UIManager uiManager;
    private AudioManager audioManager;

    private void Awake()
    {
        PreInitialize();
    }

    private void PreInitialize()
    {
        // Comme d'habitude, l'initialisation correcte du Singleton.
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        // Lorsque le GameManager est activé, nous activons les Inputs du joueur.
        inputManager.EnablePlayerInputs();
    }

    private void OnDisable()
    {
        // Lorsque le GameManager est désactivé, au contraire, nous désactivons les Inputs du joueur pour éviter toute interaction.
        inputManager.DisablePlayerInputs();
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        // Initialisation des références aux autres gestionnaires nécessaires pour l'UI et l'audio.
        uiManager = UIManager.instance;
        audioManager = AudioManager.instance;

        // Nous lançons la musique de fond au démarrage du jeu, avec une loop.
        audioManager.PlayMusic(0, true);
    }

    public void GameOver()
    {
        // Cette méthode est appelée lorsque le jeu est terminé (GameOver) pour empêcher le joueur de continuer d'utiliser les Inputs, le SlowMotion, etc...
        // par exemple.
        gameIsOver = true;
        Time.timeScale = 0f;
        uiManager.GameOverUI();
    }

    public bool GameIsOver()
    {
        // Cette méthode permet de savoir si le jeu est terminé en renvoyant l'état actuel de celui-ci.
        return gameIsOver;
    }
}