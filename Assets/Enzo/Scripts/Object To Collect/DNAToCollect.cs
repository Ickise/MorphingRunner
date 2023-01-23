using UnityEngine;

public class DNAToCollect : MonoBehaviour
{
    Scoring scoring;

    void Start()
    {
        scoring = GameObject.FindWithTag("Scoring").GetComponent<Scoring>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            scoring.score++;
            Destroy(gameObject);
        }
    }
}
