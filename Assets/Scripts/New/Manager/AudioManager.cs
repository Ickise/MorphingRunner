using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Ce script n'existait pas auparavant, il permet d'appeler l'AudioManager n'importe où et de jouer une musique ou un son précis.
    public static AudioManager instance { private set; get; }

    [SerializeField, Header("References")] private AudioSource mainAudioSource;
    [SerializeField] private AudioSource playOnceAudioSource;
    [SerializeField] private List<SoundData> soundDataList; // Liste des données sonores pour gérer les différents sons et musiques.

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        // Nous vérifions si l'instance existe déjà, pour éviter la duplication de l'AudioManager.
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // Si l'instance existe déjà, nous détruisons cet objet.
            return;
        }

        instance = this; // Sinon, nous affectons cette instance au Singleton.
    }

    // Cette fonction permet de jouer un son à partir de l'index dans la liste des sons.
    public void PlaySound(int index)
    {
        // Nous vérifions si l'index est correct pour pour éviter les erreurs.
        if (index < 0 || index >= soundDataList.Count)
        {
            Debug.LogWarning("Invalid SoundData index");
            return;
        }

        // Nous récupèrons les données sonores et nous jouons le son une seule fois avec l'AudioSource dédié.
        SoundData data = soundDataList[index];
        playOnceAudioSource.PlayOneShot(SetAudioParameters(data, playOnceAudioSource).AudioToPlay);
    }

    // Cette fonction permet de jouer un son aléatoire parmi ceux disponibles dans la liste. 
    public void PlayRandomSound()
    {
        if (soundDataList.Count == 0)
        {
            Debug.LogWarning("SoundData list is empty");
            return;
        }

        // Nous choisissons un index aléatoire et nous jouons le son.
        int randomIndex = Random.Range(0, soundDataList.Count);
        PlaySound(randomIndex);
    }

    // Cette fonction permet de jouer une musique en utilisant un index spécifique, avec la possibilité de boucler.
    public void PlayMusic(int index, bool isLoop = false)
    {
        // Nous vérifions si l'index est correct pour pour éviter les erreurs.
        if (index < 0 || index >= soundDataList.Count)
        {
            Debug.LogWarning("Invalid SoundData index");
            return;
        }

        // Nous récupèrons les données de la musique et nous vérifions si elle est déjà en train de jouer.
        SoundData data = soundDataList[index];
        if (mainAudioSource.isPlaying && mainAudioSource.clip == data.AudioToPlay)
            return; // Si la musique est déjà jouée, nous ne la relançons pas.

        // Nous configurons les paramètres de l'audio et nous jouons la musique.
        mainAudioSource.clip = SetAudioParameters(data, mainAudioSource).AudioToPlay;
        mainAudioSource.Play();

        // Si nous voulons que la musique soit en boucle, dans ce cas, nous activons le loop.
        if (isLoop)
        {
            mainAudioSource.loop = true;
        }
    }

    // Cette fonction permet de configurer les paramètres de l'AudioSource (volume, pitch, etc.) en fonction des données du son.
    private SoundData SetAudioParameters(SoundData soundData, AudioSource audioSource)
    {
        audioSource.volume = soundData.Volume; // Nous règlons le volume.
        audioSource.pitch = soundData.GetPitch(); // Nous règlons le pitch avec la méthode GetPitch.
        audioSource.outputAudioMixerGroup = soundData.AudioMixerGroup; // Nous assignons l'AudioMixerGroup à l'AudioSource.
        return soundData; // Nous renvoyons les données du son modifiées.
    }
}
