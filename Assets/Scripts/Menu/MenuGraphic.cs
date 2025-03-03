using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuGraphic : MonoBehaviour
{
    // Nous aurions pu mettre le tout dans un script SettingManager.
    // Nous aurions pu supprimer les fonctions vides puisque cela consomme des performances.
    // Problème de nomenclature et de lissibilité.
    
    // Start is called before the first frame update
    [Header("Brigness Settings")]
    [SerializeField] private Slider _brignessSlider;
    [SerializeField] private TMP_Text _brignessValue;
    [SerializeField] private float _defaultBrigness;
    private float _brigness;

    [Header("FullScrean Settings")]
    private bool _fullScrean;

    [Header("Quality Settings")]
    private int _quality;
    [Header("Resolution Settings")]
    [SerializeField] private Dropdown _dropdownResolution;
    private Resolution[] resolutions;
    private int _resolution;

    [Header("Default Buton")]
    [SerializeField] private Dropdown _dropdownQuality;
    [SerializeField] private Toggle _toggleFullScrean;
    

    void Start()
    {
        // Nous aurions dû réellement tout initialiser ici et non parfois dans les méthodes du script.
        resolutions = Screen.resolutions;
        _dropdownResolution.ClearOptions();

        List<string> _listResolution = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            string optionResolution = resolutions[i].width + " x " + resolutions[i].height;
            _listResolution.Add(optionResolution);

            if(resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                _resolution = i;  
            }
        }
        _dropdownResolution.AddOptions(_listResolution);
        _dropdownResolution.value = _resolution;
        _dropdownResolution.RefreshShownValue();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void DefaultButon()
    {
        _brignessSlider.value = _defaultBrigness;
        _brignessValue.text = _defaultBrigness.ToString("0.0");
        _brigness = _defaultBrigness;
        // Régler la luminositer sur notre support

        _dropdownQuality.value = 1;
        _quality = 1;

        _fullScrean = false;
        _toggleFullScrean.isOn = false;

        // Screen.currentResolution renvoie la résolution actuelle, mais il faut l'appliquer correctement.
        Resolution resolutions = Screen.currentResolution;
        Screen.SetResolution(resolutions.width,resolutions.height,Screen.fullScreen);
        _dropdownResolution.value = resolutions.height;

        AppliSettings();
    }
    public void SliderBrigness(float Sensitivity)
    {
        _brigness = Sensitivity;
        _brignessValue.text = Sensitivity.ToString("0.0");
    }
    public void SetFullScrean(bool isFullScrean)
    {
        _fullScrean = isFullScrean;
    }
    public void SetQuality(int QualityIndex)
    {
        _quality = QualityIndex;
    }
    public void SetResolution(int ResolutionIndex)
    {
        Resolution resolution = resolutions[_resolution];
        Screen.SetResolution(resolution.width,resolution.height,Screen.fullScreen);
    }
    public void AppliSettings()
    {
        PlayerPrefs.SetFloat("Brigness", _brigness);
        Debug.Log("Brig" + _brigness);
        // Ancien commentaire : Faire un post porésing ou un filtre et le régler avec ce paramètre
        PlayerPrefs.SetInt("Quality", _quality);
        Debug.Log("Quality" + _quality);
        QualitySettings.SetQualityLevel(_quality);
        PlayerPrefs.SetInt("FullScrean", _fullScrean ? 1 : 0);
        Debug.Log("FullScrean" + _fullScrean);
        Screen.fullScreen = _fullScrean;
        PlayerPrefs.SetInt("Resolution",_resolution);
    }

}
