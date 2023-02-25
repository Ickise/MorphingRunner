using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPréf : MonoBehaviour
{
    [SerializeField] private bool _useLoad = false;
    [Header("Audio Settings")]
    [SerializeField] private MenuAudio _scriptMenuAudio;

    [Header("Gameplay Settings")]
    [SerializeField] private MenuGameplay _scriptMenuGameplay;

    [Header("Graphic Settings")]
    [SerializeField] private MenuGraphic _scriptMenuGraphic;
    private void Awake()
    {
        if(_useLoad)
        {
            if(PlayerPrefs.HasKey("MasterVolume"))
            {
                float localVolume = PlayerPrefs.GetFloat("MasterVolume");
                _scriptMenuAudio.SliderVolume(localVolume);
                _scriptMenuAudio.VolumeApply();  
            }
            else
            {
                _scriptMenuAudio.DefaultVolume();
            }

            if(PlayerPrefs.HasKey("InvertY"))
            {
                int localInvertY = PlayerPrefs.GetInt("InvertY");
                if(localInvertY == 0)
                {
                    _scriptMenuGameplay._InvertY.isOn = false;
                }
                else
                {
                    _scriptMenuGameplay._InvertY.isOn = true;  
                }
               _scriptMenuGameplay.GameplayApply();
            }
            else
            {
                _scriptMenuGameplay.DefaultButton();
            }
            
            if(PlayerPrefs.HasKey("Sensitivity"))
            {
                float localSensitivity = PlayerPrefs.GetFloat("Sensitivity");

                _scriptMenuGameplay.SliderSensitivity(localSensitivity);
                _scriptMenuGameplay.GameplayApply();
            }
            else
            {
                _scriptMenuGameplay.DefaultButton();  
            }
            if(PlayerPrefs.HasKey("Brigness"))
            {
                float localBrigness = PlayerPrefs.GetFloat("Brigness");

                _scriptMenuGraphic.SliderBrigness(localBrigness);
                _scriptMenuGraphic.AppliSettings();
            }
            else
            {
                _scriptMenuGraphic.DefaultButon();
            }
            if(PlayerPrefs.HasKey("Quality"))
            {
                int localQuality = PlayerPrefs.GetInt("Quality");
                
                _scriptMenuGraphic.SetQuality(localQuality);
                _scriptMenuGraphic.AppliSettings();
            }
            else
            {
                _scriptMenuGraphic.DefaultButon();  
            }
            if(PlayerPrefs.HasKey("FullScrean"))
            {
                int localFullscrean = PlayerPrefs.GetInt("FullScrean");

                if(localFullscrean == 0)
                {
                    _scriptMenuGraphic.SetFullScrean(false);
                }  
                else
                {
                    _scriptMenuGraphic.SetFullScrean(true);
                }
                _scriptMenuGraphic.AppliSettings();
            }
            else
            {
                _scriptMenuGraphic.DefaultButon(); 
            }

            if(PlayerPrefs.HasKey("Resolution"))
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
