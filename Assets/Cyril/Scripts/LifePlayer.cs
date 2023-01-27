using UnityEngine;

public class LifePlayer : MonoBehaviour
{
    [SerializeField] private GameObject _deathEcrant;
    [SerializeField] private HumanTransformation _scriptHumanTransformation;
    private void OnCollisionEnter(Collision other)
    {
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
     if (other.gameObject.CompareTag("CheckPoint") && _scriptHumanTransformation.GetTransformationActive())
     {
        _scriptHumanTransformation.Passe(other.gameObject);    
     }        
    }
}
