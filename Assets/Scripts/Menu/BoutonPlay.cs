using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoutonPlay : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroupMenuBasique;
    [SerializeField] private CanvasGroup _canvasGroupMenuPlay;
    [SerializeField] private GameObject _canvasMenuPlay;
    [SerializeField] private GameObject _canvasMenuBasique;
    [SerializeField] private float _Alpha = 1;
    [SerializeField] private bool _MenuPress;
    [SerializeField] private bool _playPress;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_playPress && _MenuPress != true)
        {
            _Alpha = _Alpha - Time.deltaTime;
            _canvasGroupMenuBasique.alpha = _Alpha;
        }
        if (_Alpha <= 0)
        {
            _canvasMenuBasique.SetActive(false);
            _MenuPress = true;
        }
        if (_MenuPress)
        {
            _Alpha = _Alpha + Time.deltaTime;
            _canvasGroupMenuPlay.alpha = _Alpha;
        }
    }
    public void Play()
    {
        _playPress = true;
        _canvasMenuPlay.SetActive(true);
    }
}
