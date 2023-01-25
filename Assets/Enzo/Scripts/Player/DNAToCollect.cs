using UnityEngine;
using UnityEngine.UI;

public class DNAToCollect : MonoBehaviour
{
    Scoring scoring;


    [SerializeField] DNA[] listDNA = new DNA[3];

    [SerializeField] int[] listSpecialDNA = new int[3];

    [SerializeField] Button[] buttonList = new Button[3];

    void Start()
    {
        scoring = GameObject.FindWithTag("Scoring").GetComponent<Scoring>();
    }

    void Update()
    {
        for (int i = 0; i < listDNA.Length; i++)
        {
            if (listSpecialDNA[i] >= 10)
            {
                buttonList[i].interactable = true;
            }
            else
            {
                buttonList[i].interactable = false;
            }
        }
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
