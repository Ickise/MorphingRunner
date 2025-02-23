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
        SetSlowMotion(1f, false);
        StopAllCoroutines();
    }

    private void SetSlowMotion(float timeScale, bool active)
    {
        Time.timeScale = timeScale;
        transformationCanvas.SetActive(active);
        isSlowMotion = active;
    }

    private IEnumerator SlowMotionCoroutine()
    {
        SetSlowMotion(0.5f, true);
        yield return new WaitForSecondsRealtime(slowMotionDuration);
        SetSlowMotion(1f, false);
    }
}