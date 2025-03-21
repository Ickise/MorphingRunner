using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectCar : MonoBehaviour
{
    // Problème de nomenclature et de lissibilité.

    [SerializeField] GameObject _prefab;
    private void OnCollisionEnter(Collision other) 
    {
        // Lors d'une collision, nous faisons une instance de l'objet à la position actuelle de la voiture. Attention aux détections multiples.
        GameObject Inst = Instantiate(_prefab, transform.position, Quaternion.identity);  
        
        // Nous lançons une coroutine pour détruire l'effet après un délai, encore une fois attention aux détections multiples.
        StartCoroutine(WaitFor(Inst));       
    }

    private IEnumerator WaitFor(GameObject Inst)
    {
        yield return new WaitForSeconds(5f);
        
        Destroy(Inst, 0);
    }
}
