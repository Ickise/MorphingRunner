using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuGameplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _sensitivityTextValue;
    [SerializeField] private Slider _sliderSensitivity;
    [SerializeField] private int _sensitivityValue;
    [SerializeField] private int _defaultSensitivityValue = 4;
    public Toggle _InvertY;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SliderSensitivity(float Sensitivity)
    {
      _sensitivityValue = Mathf.RoundToInt(Sensitivity);
      _sensitivityTextValue.text = Sensitivity.ToString("0");  
    }
    public void GameplayApply()
    {
        if(_InvertY.isOn)
        {
            PlayerPrefs.SetInt("InvertY",1);
            // Coder Inverser Y
        }
        else
        {
            PlayerPrefs.SetInt("InvertY",0); 
            // Coder Ne Pas Inverser Y 
        }
        PlayerPrefs.SetInt("Sensitivity",_sensitivityValue);
        Debug.Log(_sensitivityValue);
        Debug.Log(PlayerPrefs.GetInt("InvertY"));
        // Coder Le Changement de Sensibilité 
    }
    public void DefaultButton()
    {
        _sensitivityValue = Mathf.RoundToInt(_defaultSensitivityValue);
        _sensitivityTextValue.text = _defaultSensitivityValue.ToString("0");
        _sliderSensitivity.value = _defaultSensitivityValue; 
        _InvertY.isOn = false;
        GameplayApply(); 
    }
}
