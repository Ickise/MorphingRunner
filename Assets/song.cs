using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class song : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private bool _once;
    private void Update()
    {
        if (_once)
        {
            _once = false;
            _audioSource.enabled = true;
            StartCoroutine(WaitforSeconde());
        }
    }
    public IEnumerator WaitforSeconde()
    {
        yield return new WaitForSeconds(1f);
        _audioSource.enabled = false;
        _once = true;
    }
}
