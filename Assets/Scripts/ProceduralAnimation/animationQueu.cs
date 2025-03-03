using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationQueu : MonoBehaviour
{
    // Problème de nomenclature et de lissibilité.
    // Je pense même que cela était possible de rendre ce script, PointMove et ProcéduralPoint plus générique.
    // En encapsulant les comportements communs dans une classe mère et en dérivant les spécificités dans des classes filles.

    [SerializeField] private  List<Transform>  _listQeu; 
    [SerializeField] private float Base = 25f;
    [SerializeField] private float NoBase = -15f;
    [SerializeField] private float Rotate;
    [SerializeField] private bool _Jambe;
    [SerializeField] private bool _Once;
    [SerializeField] private float lerpSpeed = 5f;
    [SerializeField] private Transform _trexTete;

    void Update()
    {
        if (_Once) StartCoroutine(Slerp());
        // A chaque frame, nous démarrons une coroutine si _Once est vrai.
        // Cela risque de créer plusieurs coroutines en parallèle, ce qui est une mauvaise pratique.
        // Nous aurions dû désactiver "_Once" juste après le lancement de la coroutine.

        for (int i = 0; i < _listQeu.Count; i++)
        {
            _listQeu[i].transform.localRotation = Quaternion.Euler(Rotate,-Rotate,0);
            // Nous recréeons une nouvelle instance de Quaternion.Euler à chaque itération.
            // Il aurait été plus optimal de le calculer une seule fois avant la boucle et de l'assigner directement.
        }

        _trexTete.transform.localRotation = Quaternion.Euler(-Rotate * 2,Rotate,0);
    }

    private IEnumerator Slerp()
    {
        while (_Jambe)
        {
            float elapsedTime = 0f;
            _Once = false; // Nous désactivons _Once dans la coroutine au lieu de le faire dans Update().  
            // Il aurait été plus logique de le désactiver immédiatement après le StartCoroutine dans Update() pour éviter les appels multiples.

            while (elapsedTime < lerpSpeed)
            {
                Debug.Log(elapsedTime/lerpSpeed + "v1");
                Rotate =  Mathf.Lerp(Base,NoBase,elapsedTime/lerpSpeed);
                elapsedTime += Time.smoothDeltaTime; 
                yield return null;
                // Time.smoothDeltaTime n'est pas nécessaire ici, Time.deltaTime aurait suffi.
                // smoothDeltaTime est utile uniquement dans certains cas spécifiques d'interpolation graphique.
            }
            yield return new WaitForSeconds(0.1f);

            elapsedTime = 0f;
            while (elapsedTime < lerpSpeed)
            {
                Debug.Log(elapsedTime/lerpSpeed + "v2");
                Rotate = Mathf.Lerp(NoBase,Base,elapsedTime/lerpSpeed);
                elapsedTime += Time.smoothDeltaTime;
                yield return null;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
