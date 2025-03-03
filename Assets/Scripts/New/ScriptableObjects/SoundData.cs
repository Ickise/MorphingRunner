using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "ScriptableObjects/Sound/SoundData", order = 1)]
public class SoundData : ScriptableObject
{
    // Ce script n'existait pas auparavant et permet de stocker des datas pour les différents sons du jeu. 
    
    [SerializeField] private float volume = 1f;

    [Tooltip("À mettre faible si on veut un pitch convenable sinon, mettre 0 si on ne veut pas de pitch")]
    [SerializeField]
    private float pitchVariation = 0.2f;

    [SerializeField] private AudioMixerGroup audioMixerGroup;

    [SerializeField] private AudioClip audioToPlay;

    // Les différentes propriétés publics en public getter et private setter permettant d'accéder facilement aux valeurs depuis d'autres scripts.
    public float Volume => volume;
    public AudioMixerGroup AudioMixerGroup => audioMixerGroup;
    public AudioClip AudioToPlay => audioToPlay;

    // Cette méthode permet de renvoyer un pitch aléatoire basé sur la Range que j'ai défini.
    public float GetPitch()
    {
        return 1 + Random.Range(-pitchVariation, pitchVariation);
    }
}