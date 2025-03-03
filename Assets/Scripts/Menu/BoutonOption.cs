using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoutonOption : MonoBehaviour
{
    // Nous aurions pu concentré le tout dans un script MenuManager.

    [SerializeField] private CanvasGroup _canvasGroupMenuBasique;
    [SerializeField] private CanvasGroup _canvasGroupMenuOption;
    [SerializeField] private GameObject _canvasMenuOption;
    [SerializeField] private GameObject _canvasMenuBasique;
    [SerializeField] private float _Alpha = 1;
    [SerializeField] private bool _MenuPress;
    [SerializeField] private bool _playPress;

    void Start()
    {
        // La méthode Start est vide et inutile ici.
    }

    void Update()
    {
        // Ce bloc réduit progressivement l'alpha de _canvasGroupMenuBasique lorsque _playPress est vrai et _MenuPress est faux.
        if (_playPress && _MenuPress != true)
        {
            _Alpha = _Alpha - Time.deltaTime; 
            _canvasGroupMenuBasique.alpha = _Alpha;
        }

        // Quand l'alpha atteint 0, nous désactivons le canvas.
        if (_Alpha <= 0)
        {
            _canvasMenuBasique.SetActive(false);
            _MenuPress = true;
        }

        // Si _MenuPress est true, nous augmentons l'alpha du menu option.
        if (_MenuPress)
        {
            _Alpha = _Alpha + Time.deltaTime;
            _canvasGroupMenuOption.alpha = _Alpha;
        }
    }

    // Fonction appelée lorsque le bouton option est pressé.
    public void Option()
    {
        _playPress = true;
        _canvasMenuOption.SetActive(true);
    }
}
