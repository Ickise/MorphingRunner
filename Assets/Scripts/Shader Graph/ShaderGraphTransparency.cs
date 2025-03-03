using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderGraphTransparency : MonoBehaviour
{
    // Problème de nomenclature et de lissibilité.
    [SerializeField] 
    private Transform _player;
    [SerializeField]
    private LayerMask wallMask;
    private Camera mainCamera;
    private void Awake() {
        mainCamera = GetComponent<Camera>();
    }
    void Update()
    {
        Vector2 cutoutPos = mainCamera.WorldToViewportPoint(_player.position); // Conversion de la position du joueur en coordonnées de la caméra.

        cutoutPos.y /= (Screen.width / Screen.height); 

        Vector3 offset = _player.position - transform.position;

        // Physics.RaycastAll renvoie tous les objets dans le rayon, ce qui peut être coûteux.
        // Nous aurions pu utiliser Physics.Raycast() si un seul obstacle est nécessaire ou Physics.RaycastNonAlloc() pour réduire l’allocation mémoire.
        RaycastHit[] hitObjects = Physics.RaycastAll(transform.position, offset, offset.magnitude, wallMask);
        Debug.DrawRay(transform.position, offset, Color.blue, 10f); // Ligne de debug pour voir le rayon.

        for (int i = 0; i < hitObjects.Length; i++)
        {
            // GetComponent<Renderer>() peut être coûteux s'il est appelé souvent.
            // Nous aurions pu tocker une référence au Renderer via un Dictionary<Transform, Renderer> pour éviter de l’appeler en boucle.
            Material[] materials = hitObjects[i].transform.GetComponent<Renderer>().materials;

            for (int m = 0; m < materials.Length; m++)
            {
                // Nous aurions dû mettre une vérification pour éviter d’appeler SetVector() et SetFloat() inutilement.
                if (materials[m].HasProperty("_cutoutPosition"))
                {
                    materials[m].SetVector("_cutoutPosition", cutoutPos);
                }
                if (materials[m].HasProperty("_cutoutSize"))
                {
                    materials[m].SetFloat("_cutoutSize", 0.15f);
                }
                if (materials[m].HasProperty("_falloffSize"))
                {
                    materials[m].SetFloat("_falloffSize", 0.01f);
                }
            }
        }
    }
}
