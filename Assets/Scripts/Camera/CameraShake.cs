using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Cette méthode effectue un "shake" sur la caméra en modifiant sa position pendant un certain temps.
    // Le problème d'optimisation ici est la gestion de la position de la caméra à chaque frame avec l'utilisation de Random.Range.
    // Chaque appel à Random.Range est relativement coûteux, surtout si cette fonction est appelée fréquemment.
    // De plus, nous modifions directement la position à chaque fois ce qui pourrait nuire aux performances en particulier dans les jeux plus complexes.

    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0.0f;
        
        // Un Lerp aurait pu être utilisé. 

        while (elapsed < duration)
        {
            // Une meilleure approche consisterait à pré-calculer les valeurs ou à en limiter le nombre. Grâce à cela, je pourais enlever le Random.Range.
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
    }
}
