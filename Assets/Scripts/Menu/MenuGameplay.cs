using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuGameplay : MonoBehaviour
{
    // Nous aurions pu mettre le tout dans un script SettingManager.
    // Nous aurions pu supprimer les fonctions vides puisque cela consomme des performances.
    // Problème de nomenclature et de lissibilité.

    [SerializeField] private TMP_Text _sensitivityTextValue;
    [SerializeField] private Slider _sliderSensitivity;
    [SerializeField] private int _sensitivityValue;
    [SerializeField] private int _defaultSensitivityValue = 4;
    public Toggle _InvertY;
    // Start is called before the first frame update
    void Start()
    {
        // Nous devrions initialiser la sensibilité et l'option d'inversion de l'axe Y ici. Cela permettrait de charger les préférences
        // de l'utilisateur dès le début.
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
        // Nous pourrions éviter d'écrire inutilement dans PlayerPrefs si la valeur n'a pas changé.
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
        // PlayerPrefs.Save(); nécessaire pour sauvegarder immédiatement les préférences.
        PlayerPrefs.SetInt("Sensitivity",_sensitivityValue);
        Debug.Log(_sensitivityValue);
        Debug.Log(PlayerPrefs.GetInt("InvertY"));
        // Il faudrait ici appeler une méthode pour appliquer réellement la sensibilité.
    }
    public void DefaultButton()
    {
        // Il faudrait éviter de recalculer plusieurs fois la conversion de _defaultSensitivityValue en chaîne de caractères.
        _sensitivityValue = Mathf.RoundToInt(_defaultSensitivityValue);
        _sensitivityTextValue.text = _defaultSensitivityValue.ToString("0");
        _sliderSensitivity.value = _defaultSensitivityValue; 
        _InvertY.isOn = false;
        GameplayApply(); 
    }
}
