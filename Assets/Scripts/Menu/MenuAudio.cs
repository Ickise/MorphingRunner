using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuAudio : MonoBehaviour
{
    // Nous aurions pu mettre certaines fonctions dans un script AudioManager et d'autres dans un MenuManager.
    // Nous aurions pu supprimer les fonctions vides puisque cela consomme des performances.
    // Problème de nomenclature et de lissibilité.

    [SerializeField] private TMP_Text _textVolumeValue;

    [SerializeField] private Slider _volumeSlider;

    [SerializeField] private GameObject _PanelConfirmation;
    private float _defaultVolume = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Nous pourrions initialiser le volume ici en récupérant la valeur stockée dans PlayerPrefs. Cela éviterait d'avoir une valeur incorrecte
        // au démarrage.
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
        // Inutile de stocker le volume si sa valeur n'a pas changé, cela évite des écritures inutiles dans PlayerPrefs.
        // Nous devons appeler Save() pour s'assurer que la valeur est bien écrite immédiatement.
        PlayerPrefs.SetFloat("MasterVolume", AudioListener.volume);
        Debug.Log(AudioListener.volume);
        StartCoroutine(PanelConfirmation());
    }

    public IEnumerator PanelConfirmation()
    {
        _PanelConfirmation.SetActive(true);
        yield return new WaitForSeconds(2);
        _PanelConfirmation.SetActive(false);
    }

    public void DefaultVolume()
    {
        // Au lieu de recalculer la chaîne de caractères à chaque ligne, nous pouvons la stocker une fois.
        AudioListener.volume = _defaultVolume;
        _volumeSlider.value = _defaultVolume;
        _textVolumeValue.text = _defaultVolume.ToString("0.0");
        VolumeApply();
    }
}