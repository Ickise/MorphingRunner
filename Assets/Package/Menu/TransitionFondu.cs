using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionFondu : MonoBehaviour
{
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
        if (_active)
        {
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
