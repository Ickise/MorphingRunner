using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoutonLoadGame : MonoBehaviour
{
    // La méthode Start est vide et n'est pas utilisée, donc elle peut être supprimée pour éviter de laisser du code inutile.
    void Start()
    {
    }

    // Cette méthode charge une scène spécifiée par son nom. Bien que cette approche soit fonctionnelle, il y a quelques améliorations possibles.
    public void LoadScene(string _SceneLoadName)
    {
        // Afficher le nom de la scène dans le log est utile pour le Debug, mais il peut être coûteux en termes de performance lorsqu'il est activé en production.
        // Nous pourrions envisager de désactiver ces logs en mode production pour améliorer les performances.
        Debug.Log(_SceneLoadName + "Scene Load");

        // Charger la scène de manière synchrone. Cela peut être amélioré en permettant de gérer le processus de chargement de manière asynchrone
        // pour éviter des blocages pendant le changement de scène.
        SceneManager.LoadScene(_SceneLoadName);
    }
}