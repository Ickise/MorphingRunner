using System.Collections;
using UnityEngine;

public class SlowMotion : MonoBehaviour
{
    [SerializeField, Header("References")] private GameObject transformationCanvas;
    [SerializeField] private InputManager inputManager;
    
    [SerializeField, Header("Settings")] private float slowMotionDuration = 1.5f;

    private bool isSlowMotion;

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
        isSlowMotion = false;
        StopCoroutine(SlowMotionCoroutine());
        Time.timeScale = 1f;
        transformationCanvas.SetActive(false);
    }

    private IEnumerator SlowMotionCoroutine()
    {
        isSlowMotion = true;
        Time.timeScale = 0.5f;
        transformationCanvas.SetActive(true);

        yield return new WaitForSeconds(slowMotionDuration); // on ne peut pas appuyer sur les boutons
        Time.timeScale = 1f;
        transformationCanvas.SetActive(false);
        isSlowMotion = false;
    }
}