using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoutonPlay : MonoBehaviour
{
    // Même commentaire que pour le script BoutonOption. De plus, nous aurions pu concentré le tout dans un script MenuManager.
    [SerializeField] private CanvasGroup _canvasGroupMenuBasique;
    [SerializeField] private CanvasGroup _canvasGroupMenuPlay;
    [SerializeField] private GameObject _canvasMenuPlay;
    [SerializeField] private GameObject _canvasMenuBasique;
    [SerializeField] private float _Alpha = 1;
    [SerializeField] private bool _MenuPress;
    [SerializeField] private bool _playPress;
    void Start()
    {

    }

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
