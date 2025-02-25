using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    // Script très court mais qui permet de respecter le Single Responsability Principle.
    
    [SerializeField, Header("Settings")] private LayerMask isObstacleLayer;
    
    private DNAManager dnaManager;
    
    private GameManager gameManager;

    private void Start()
    {
        dnaManager = DNAManager.instance;
        gameManager = GameManager.instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & isObstacleLayer) != 0)
        {
            gameManager.GameOver();
            return;
        }
        
        DNAType dnaType = other.GetComponent<DNAType>();
        if (dnaType != null)
        {
            Debug.Log("DNA Collect");
            dnaManager.AddDNA(dnaType.GetDNAType());
            //Destroy(other.gameObject);
        }
    }
}