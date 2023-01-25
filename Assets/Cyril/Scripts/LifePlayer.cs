using UnityEngine;

public class LifePlayer : MonoBehaviour
{
    [SerializeField] private GameObject _deathEcrant;
    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("Collider"))
        {
            Debug.Log("Kill");
            _deathEcrant.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
