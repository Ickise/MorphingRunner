using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotaionHelicopter : MonoBehaviour
{
    // Problème de nomenclature et de lissibilité.
    [SerializeField] float lerpSpeed;
    [SerializeField] Vector3 _vectorBase;
    [SerializeField] Vector3 _vectorBase2;
    [SerializeField] Vector3 Rotate;
    [SerializeField] bool _Jambe;
    [SerializeField] bool _Once;

    void Update()
    {
        // Ce code applique directement une rotation autour de l'axe Y pendant l'Update() ce qui est assez coûteux juste pour du décor.
        transform.localRotation = Quaternion.Euler(0, Rotate.y, 0);

        // Si _Once est vrai, nous commençons la coroutine LerpFloat. Nous n'avons pas besoin d'écrire == true, nous avons juste besoin de mettre
        // if(_Once), je suppose que cela fait un appel inutile et nous fait perdre des performances.
        if (_Once == true)
        {
            StartCoroutine(LerpFloat());
        }
    }

    // La coroutine est utilisée pour animer l'interpolation entre deux vecteurs. Elle déplace l'hélicoptère entre deux points en permanence.
    private IEnumerator LerpFloat()
    {
        while (_Jambe) // La boucle continue tant que _Jambe est vrai. Nous avons deux boucles while dans une boucle while, cela augmente énormément
        // les risques de boucle infinie et les problèmes. Je suppose que cela est peu optimisé.
        {
            float elapsedTime = 0f;
            _Once = false;

            // Il y a de la redondance, nous aurions pu utiliser un ternaire et optimiser cela pour éviter les deux boucles while dans une boucle while.
            while (elapsedTime < lerpSpeed)
            {
                Rotate = Vector3.Lerp(_vectorBase, _vectorBase2, elapsedTime / lerpSpeed);
                elapsedTime += Time.smoothDeltaTime;
                yield return null;
            }

            yield return new WaitForSeconds(0.1f); // Nous pouvons tout simplement faire yield return null pour laisser une frame, mais peut être que
            // cela est fait exprès ou avait déjà était testé. 

            elapsedTime = 0f;

            while (elapsedTime < lerpSpeed)
            {
                Rotate = Vector3.Lerp(_vectorBase2, _vectorBase, elapsedTime / lerpSpeed);
                elapsedTime += Time.smoothDeltaTime;
                yield return null;
            }

            yield return new WaitForSeconds(0.1f); 
        }
    }
}