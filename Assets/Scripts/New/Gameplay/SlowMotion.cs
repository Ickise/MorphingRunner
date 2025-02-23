using System.Collections;
using UnityEngine;

public class SlowMotion : MonoBehaviour
{
    // Séparation claire, ce script ne s'occupe que du slow motion.
    [SerializeField, Header("References")] private GameObject transformationCanvas;
    [SerializeField] private InputManager inputManager;

    [SerializeField, Header("Settings")] private float slowMotionDuration = 1.5f;

    private bool isSlowMotion;

    // L'utilisation d'événement permet d'éviter d’appeler Update() en permanence et cela améliore les performances.
    private void OnEnable()
    {
        inputManager.SlowMotionEvent += ActivateSlowMotion;
    }

    private void OnDisable()
    {
        inputManager.SlowMotionEvent -= ActivateSlowMotion;
    }

    private void ActivateSlowMotion()
    {
        if (isSlowMotion) return;
        StartCoroutine(SlowMotionCoroutine());
    }

    public void DeactivateSlowMotion()
    {
        SetSlowMotion(1f, false);
        StopAllCoroutines(); // Cela évite que plusieurs instances de la coroutine tournent en parallèle.
    }
    
    // J'ai créé une méthode pour éviter de répéter le code. Je set le slow motion et j'active/désactive le canvas.
    private void SetSlowMotion(float timeScale, bool active)
    {
        Time.timeScale = timeScale;
        transformationCanvas.SetActive(active);
        isSlowMotion = active;
    }

    private IEnumerator SlowMotionCoroutine()
    {
        SetSlowMotion(0.5f, true);
        yield return new WaitForSecondsRealtime(slowMotionDuration); // WaitForSecondsRealtime ne dépend pas du timeScale. Cela permet de ne pas ralentir le temps d'attente.
        SetSlowMotion(1f, false);
    }
}