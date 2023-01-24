using System.Collections.Generic;
using UnityEngine;

public class DNAToCollect : MonoBehaviour
{
    Scoring scoring;

    [SerializeField] DNA[] listDNA = new DNA[3];
    [SerializeField] int[] listSpecialDNA = new int[3];

    void Start()
    {
        scoring = GameObject.FindWithTag("Scoring").GetComponent<Scoring>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DNA"))
        {
            for (int i = 0; i < listDNA.Length; i++)
            {
                if (other.name == listDNA[i].dnaName + "(Clone)")
                {
                    listSpecialDNA[i]++;
                    scoring.score++;
                    Destroy(other.gameObject);
                }
            }
        }
    }
}
