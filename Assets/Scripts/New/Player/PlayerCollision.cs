using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    // Script très court mais qui permet de respecter le Single Responsability Principle.
    private DNAManager dnaManager;

    private void Start()
    {
        dnaManager = DNAManager.instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        DNAType dnaType = other.GetComponent<DNAType>();
        if (dnaType != null)
        {
            dnaManager.AddDNA(dnaType.GetDNAType());
            //EDestroy(other.gameObject);
        }
    }
}