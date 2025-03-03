using UnityEngine;

public class MorphDeathCollider : MonoBehaviour
{
    // Ce script gère l'activation du menu de mort lorsque le joueur entre en collision avec l'objet.

    GameObject deathMenu;

    // La méthode Awake est utilisée ici pour initialiser le menu de mort en le recherchant par son tag.
    // Cependant, l'appel à `FindGameObjectWithTag` à chaque fois que ce script est chargé peut être inefficace si le deathMenu existe déjà.
    // Ce genre d'appel à `FindGameObjectWithTag` est relativement coûteux en termes de performance, en particulier si c'est appelé fréquemment ou dans un grand nombre de scènes.
    // Une meilleure solution serait de passer cette référence par l'inspecteur pour éviter la recherche dynamique chaque fois.

    void Awake()
    {
        deathMenu = GameObject.FindGameObjectWithTag("DeathCanvas");

        // Une vérification de null pour `deathMenu` pourrait être utile pour éviter des erreurs en cas d'oubli de l'objet dans la scène.
        deathMenu.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        // Vérification si la transformation est activée et si l'objet est le joueur.
        // L'appel à `CompareTag("Player")` est généralement plus performant que `other.gameObject.tag == "Player"`, ce qui est une bonne approche.
        if (TransformationsChoices.transformationsChoices.isMorph && other.gameObject.CompareTag("Player"))
        {
            // L'activation du menu de mort et la mise en pause du temps fonctionnent, mais il serait bon de se demander si nous voulons utiliser
            // Time.timeScale = 0f directement ici, ce qui peut provoquer des effets secondaires imprévus. Le changement de Time.timeScale
            // affecte toutes les autres animations et les mécaniques du jeu. L'utilisation de `Time.timeScale` pour gérer la pause du jeu pourrait
            // être centralisée dans un autre script dédié à la gestion des pauses.
            deathMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}