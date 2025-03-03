using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPréf : MonoBehaviour
{
    // Problème de nomenclature et de lissibilité.

    [SerializeField] private bool _useLoad = false;

    [Header("Audio Settings")] [SerializeField]
    private MenuAudio _scriptMenuAudio;

    [Header("Gameplay Settings")] [SerializeField]
    private MenuGameplay _scriptMenuGameplay;

    [Header("Graphic Settings")] [SerializeField]
    private MenuGraphic _scriptMenuGraphic;

    private void Awake()
    {
        if (_useLoad) // Si le chargement est activé, nous chargeons les réglages.
        {
            // Chargement du volume du son
            if (PlayerPrefs.HasKey("MasterVolume"))
            {
                float localVolume = PlayerPrefs.GetFloat("MasterVolume");
                _scriptMenuAudio.SliderVolume(localVolume);
                _scriptMenuAudio.VolumeApply();
            }
            else
            {
                _scriptMenuAudio.DefaultVolume(); // Si aucune valeur n'est trouvée, nous restaurons la valeur par défaut.
            }

            // Chargement du réglage de l'inversion de l'axe Y.
            if (PlayerPrefs.HasKey("InvertY"))
            {
                int localInvertY = PlayerPrefs.GetInt("InvertY");
                _scriptMenuGameplay._InvertY.isOn = localInvertY == 1;
                _scriptMenuGameplay.GameplayApply();
            }
            else
            {
                _scriptMenuGameplay.DefaultButton();
            }

            // Chargement de la sensibilité.
            if (PlayerPrefs.HasKey("Sensitivity"))
            {
                float localSensitivity = PlayerPrefs.GetFloat("Sensitivity");
                _scriptMenuGameplay.SliderSensitivity(localSensitivity);
                _scriptMenuGameplay.GameplayApply();
            }
            else
            {
                _scriptMenuGameplay.DefaultButton();
            }

            // Chargement de la luminosité.
            if (PlayerPrefs.HasKey("Brigness"))
            {
                float localBrigness = PlayerPrefs.GetFloat("Brigness");
                _scriptMenuGraphic.SliderBrigness(localBrigness);
                _scriptMenuGraphic.AppliSettings();
            }
            else
            {
                _scriptMenuGraphic.DefaultButon();
            }

            // Chargement de la qualité graphique.
            if (PlayerPrefs.HasKey("Quality"))
            {
                int localQuality = PlayerPrefs.GetInt("Quality");
                _scriptMenuGraphic.SetQuality(localQuality);
                _scriptMenuGraphic.AppliSettings();
            }
            else
            {
                _scriptMenuGraphic.DefaultButon();
            }

            // Chargement du mode plein écran.
            if (PlayerPrefs.HasKey("FullScrean"))
            {
                int localFullscrean = PlayerPrefs.GetInt("FullScrean");
                _scriptMenuGraphic.SetFullScrean(localFullscrean == 1);
                _scriptMenuGraphic.AppliSettings();
            }
            else
            {
                _scriptMenuGraphic.DefaultButon();
            }

            // Chargement de la résolution.
            if (PlayerPrefs.HasKey("Resolution"))
            {
                int localResolution = PlayerPrefs.GetInt("Resolution");
                _scriptMenuGraphic.SetResolution(localResolution);
                _scriptMenuGraphic.AppliSettings();
            }
            else
            {
                _scriptMenuGraphic.DefaultButon();
            }
        }
    }
}