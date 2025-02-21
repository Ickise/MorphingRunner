using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource _audioSource;
    
    public static AudioManager _instance { get; private set; }

    private void Awake() 
    {
        _instance = this;
    }

    public void PlaySFX(AudioClip clipToPlay)
    {
        _audioSource.PlayOneShot(clipToPlay);
    }
}