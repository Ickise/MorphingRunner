using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuAudio : MonoBehaviour
{
    [SerializeField] private TMP_Text _textVolumeValue;
    [SerializeField] private Slider _volumeSlider;
    // [SerializeField] private GameObject _PanelConfirmation;
    private float _defaultVolume = 1.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SliderVolume(float Volume)
    {
        AudioListener.volume = Volume;
        _textVolumeValue.text = Volume.ToString("0.0");
    }
    public void VolumeApply()
    {
        PlayerPrefs.SetFloat("MasterVolume", AudioListener.volume);
        Debug.Log(AudioListener.volume);
        // StartCoroutine(PanelConfirmation());
    }
    // public IEnumerator PanelConfirmation()
    // {
    //     // _PanelConfirmation.SetActive(true);
    //     // yield return new WaitForSeconds(2);
    //     // _PanelConfirmation.SetActive(false);
    // }
    public void DefaultVolume()
    {
        AudioListener.volume = _defaultVolume;
        _volumeSlider.value = _defaultVolume;
        _textVolumeValue.text = _defaultVolume.ToString("0.0");
        VolumeApply();
    }
}
