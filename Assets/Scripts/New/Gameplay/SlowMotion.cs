using UnityEngine;

public class SlowMotion : MonoBehaviour, IUpdate
{
    // Séparation claire, ce script ne s'occupe que du slow motion. Auparavant, le slow motion se faisait dans TransformationChoices.
    [SerializeField, Header("References")] private GameObject transformationCanvas;
    [SerializeField] private InputManager inputManager;

    [SerializeField, Header("Settings")] private float slowMotionDuration = 2f;

    private bool isSlowMotion;

    private float slowMotionTimer;

    private GameManager gameManager;

    // Nous souscrivons à l'événement lorsque le script est activé pour utiliser les Inputs et s'abonner à l'UpdateManager.
    private void OnEnable()
    {
        Subscribe();
    }

    // Nous nous désinscrivons lorsque le script est désactivé pour éviter les erreurs et les null références.
    private void OnDisable()
    {
        Unsubscribe();
    }

    private void Subscribe()
    {
        UpdateManager.RegisterUpdate(this);
        inputManager.SlowMotionEvent += ActivateSlowMotion;
    }

    private void Unsubscribe()
    {
        UpdateManager.UnregisterUpdate(this);
        inputManager.SlowMotionEvent -= ActivateSlowMotion;
    }

    private void Start()
    {
        gameManager =
            GameManager.instance; // Nous récupérons l'instance du GameManager pour éviter les GameManager.instance et optimiser.
    }

    private void ActivateSlowMotion()
    {
        if (isSlowMotion) return; // Si le slow motion est déjà activé, nous faisons un return.
        SetSlowMotion(0.5f, true); // Au contraire, s'il n'est pas activé alors nous lançons le slow motion.
        slowMotionTimer = slowMotionDuration; // Ici, je set la valeur de slowMotionTimer
    }

    public void DeactivateSlowMotion()
    {
        SetSlowMotion(1f, false); // Nous remettons le temps à la normale et nous désactivons l'effet.
    }

    // Méthode pour éviter la répétition du code : elle change le timeScale et active/désactive le canvas.
    private void SetSlowMotion(float timeScale, bool active)
    {
        Time.timeScale = timeScale;
        transformationCanvas.SetActive(active);
        isSlowMotion = active;
    }

    // Cette méthode provient de l'interface pour avoir accès à l'Update de l'UpdateManager.
    public void UpdateTick()
    {
        // Si la partie est perdue, alors nous arrêtons l'Update.
        if (gameManager.GameIsOver())
        {
            // Nous désactivons le slow motion pour éviter un bug, si le joueur meurt et qu'il a activé le slow motion, alors à la fin cela remet le timeScale à 1.
            DeactivateSlowMotion();
            return;
        }

        // Cela permet d'activer le slow motion tant que le bool est true, lorsque le temps atteint 0 ou moins, alors on désactive le slow motion.
        if (!isSlowMotion) return;

        slowMotionTimer -= Time.unscaledDeltaTime;

        if (slowMotionTimer <= 0f)
        {
            DeactivateSlowMotion();
        }
    }
}