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
        // Ici je détecte les objets qui ont uniquement le layer IsObstacle pour lancer le GameOver.
        // Je pense que c'est la manière la plus optimisée pour ne pas utiliser de CompareTag ou ne pas avoir juste un script Obstacle sur tous les objets.
        if (((1 << other.gameObject.layer) & isObstacleLayer) != 0)
        {
            gameManager.GameOver();
            return;
        }
        
        // Ici je détecte uniquement les ADN pour récupérer un ADN précis.
        DNAType dnaType = other.GetComponent<DNAType>();
        if (dnaType != null)
        {
            Debug.Log("DNA Collect");
            dnaManager.AddDNA(dnaType.GetDNAType());
            //Destroy(other.gameObject);
        }
    }
}