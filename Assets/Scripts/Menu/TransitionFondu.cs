using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionFondu : MonoBehaviour
{
    // Nous aurions pu mettre le tout dans un script TransitionManager.
    // Problème de nomenclature et de lissibilité.
    
    [Header ("Classique")]
    [SerializeField] private bool _On;
    [SerializeField] private bool _active;
    [SerializeField] private CanvasGroup _canvasgroup;
    [SerializeField] private GameObject _panel;
    [SerializeField] private float _Alpha;
    [SerializeField] private float _AlphaBis;
    [SerializeField] private AnimationCurve _animationCurve = new AnimationCurve();
    [SerializeField] private AnimationCurve _animationCurveBIS = new AnimationCurve();

    [Header ("BIS")]

    [SerializeField] private CanvasGroup _canvasgroupBIS;
    [SerializeField] private GameObject _PanelBis;

    void Update()
    {
        // Nous aurions pu mettre if (!_active) return pour éviter les exécutions inutiles.
        if (_active)
        {
            // Premier fondu similaire au second.
            if (_On)
            {
                if (_Alpha < 1)
                {
                    _panel.SetActive(true);
                    _Alpha = _Alpha +  Time.deltaTime / 2  ;
                    _canvasgroup.alpha = _animationCurve.Evaluate(_Alpha);
                }
            }
            else
            {
                if (_Alpha > 0)
                {
                    _Alpha = _Alpha - Time.deltaTime / 2;
                    _canvasgroup.alpha = _animationCurve.Evaluate(_Alpha);
                }
                else
                {
                    _panel.SetActive(false);
                }
            }

            // Second fondu redondant.
            if (_On)
            {
                if (_AlphaBis > 0)
                {
                    _AlphaBis = _AlphaBis - Time.deltaTime / 2;
                    _canvasgroupBIS.alpha = _animationCurveBIS.Evaluate(_AlphaBis);
                }
                else
                {
                    _PanelBis.SetActive(false);
                }
            }
            else
            {
                // Nous aurions du mettre if(alphaBis < 1) pour éviter une boucle infinie si jamais _AlphaBis est déjà à 1.
                if (_Alpha > 0)
                {
                    _PanelBis.SetActive(true);
                    _AlphaBis = _AlphaBis + Time.deltaTime / 2;
                    _canvasgroupBIS.alpha = _animationCurveBIS.Evaluate(_AlphaBis);
                }
            }
        }
    }
    public void transitionlocalbool(bool localON)
    {
        _On = localON;
        if (_On)
        {
            _Alpha = 0;
            _AlphaBis = 1;
        }
        else
        {
            _Alpha = 1;
            _AlphaBis = 0;
        }
    }
    public void transitionlocalCanvas(CanvasGroup _canvasgrouplocal)
    {
        _canvasgroup = _canvasgrouplocal;
    }
    public void transitionlocalGameObject(GameObject _Panellocal)
    {
        _panel = _Panellocal;
        _active = true;
    }
    public void BistransitionGameObject(GameObject _PanellocalBis)
    {
        _PanelBis = _PanellocalBis;
    }
    public void BistransitionCanvasGroup(CanvasGroup _canvasgrouplocalbis)
    {
        _canvasgroupBIS = _canvasgrouplocalbis;
    }
}
