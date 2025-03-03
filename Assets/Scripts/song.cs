using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class song : MonoBehaviour
{
    // Problème de nomenclature et de lissibilité.
    // Nous aurions dû faire un AudioManager puisque nous avons mis à plusieurs endroit de l'audio sans utiliser ce script.
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private bool _once;
    private void Update()
    {
        if (_once)
        {
            _once = false;
            _audioSource.enabled = true;
            StartCoroutine(WaitforSeconde());
        }
    }
    public IEnumerator WaitforSeconde()
    {
        // De plus, cela n'est pas très précis puisque nous voulons jouer une fois le son et à chaque fois au bout d'une seule seconde il se coupe.
        // Que se passe-t-il si le son dure plus ou moins d'une seconde ? Il se coupe avant la fin et trop tard.
        yield return new WaitForSeconds(1f);
        _audioSource.enabled = false;
        _once = true;
    }
}
