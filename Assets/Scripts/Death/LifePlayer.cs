using UnityEngine;

public class LifePlayer : MonoBehaviour
{
    // Problème de nomenclature et de lissibilité.
    // De plus, il existe un script MorphDeathCollider qui fait la même chose.
    // Nous aurions pu regrouper le tout dans un UIManager.
    [SerializeField] private GameObject _deathEcrant;
    [SerializeField] private HumanTransformation _scriptHumanTransformation;

    private void OnCollisionEnter(Collision other)
    {
        // Problème de redondance puisque nous faisons la même chose deux fois avec simplement une condition différente.
        // Nous utilisons également les CompareTag alors que cela pourrait causer un problème si nous changeons le tag et cela cause un problème
        // d'optimisation.
        if (other.gameObject.CompareTag("Collider"))
        {
            Debug.Log("Kill");
            _deathEcrant.SetActive(true);
            Time.timeScale = 0f;
        }

        if (other.gameObject.CompareTag("CheckPoint") && !_scriptHumanTransformation.GetTransformationActive())
        {
            Debug.Log("Kill");
            _deathEcrant.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Encore une fois, les ComapreTag...
        if (other.gameObject.CompareTag("CheckPoint") && _scriptHumanTransformation.GetTransformationActive())
        {
            _scriptHumanTransformation.Passe(other.gameObject);
        }
    }
}