using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { private set; get; }

    [SerializeField, Header("References")] private AudioSource mainAudioSource;
    [SerializeField] private AudioSource playOnceAudioSource;
    [SerializeField] private List<SoundData> soundDataList;
    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    public void PlaySound(int index)
    {
        // Fonction à appeler dans les autres scripts AudioManager.instance.PlaySFX pour ne jouer qu'une seule fois un son
        
        if (index < 0 || index >= soundDataList.Count)
        {
            Debug.LogWarning("Invalid sound data index");
            return;
        }
       
        SoundData data = soundDataList[index]; 
        playOnceAudioSource.PlayOneShot(SetAudioParameters(data, playOnceAudioSource).AudioToPlay);
    }

    public void PlayRandomSound()
    {
        if (soundDataList.Count == 0)
        {
            Debug.LogWarning("Sound data list is empty");
            return;
        }

        int randomIndex = Random.Range(0, soundDataList.Count);
        PlaySound(randomIndex);
    }

    public void PlayMusic(int index, bool isLoop = false)
    {
        if (index < 0 || index >= soundDataList.Count)
        {
            Debug.LogWarning("Invalid sound data index");
            return;
        }
        
        SoundData data = soundDataList[index];
        if (mainAudioSource.isPlaying && mainAudioSource.clip == data.AudioToPlay)
            return;
        
        mainAudioSource.clip = SetAudioParameters(data, mainAudioSource).AudioToPlay;
        mainAudioSource.Play();

        if (isLoop)
        {
            mainAudioSource.loop = true;
        }
    }

    private SoundData SetAudioParameters(SoundData soundData, AudioSource audioSource)
    {
        audioSource.volume = soundData.Volume;
        audioSource.pitch = soundData.GetPitch();
        audioSource.outputAudioMixerGroup = soundData.AudioMixerGroup;
        return soundData;
    }
}