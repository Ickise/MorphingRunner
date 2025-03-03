using System.Collections;
using UnityEngine;

public class SlowMotion : MonoBehaviour, IUpdate
{
    // Séparation claire, ce script ne s'occupe que du slow motion.
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

    public void UpdateTick()
    {
        if (gameManager.GameIsOver())
        {
            DeactivateSlowMotion();
            return;
        }

        if (!isSlowMotion) return;

        slowMotionTimer -= Time.unscaledDeltaTime;
        
        if (slowMotionTimer <= 0f)
        {
            DeactivateSlowMotion();
        }
    }
}