using UnityEngine;
using UnityEngine.UI;

public class DNAToCollect : MonoBehaviour
{
    Scoring scoring;


    [SerializeField] DNA[] listDNA = new DNA[3];

    [SerializeField] int[] listSpecialDNA = new int[3];

    [SerializeField] Button[] buttonList = new Button[3];

    [SerializeField] int numberOfDNAToTransform = 10;
    [SerializeField] string stringButton;
    
    bool canTransform;

    void Start()
    {
        scoring = GameObject.FindWithTag("Scoring").GetComponent<Scoring>();
    }

    void Update()
    {
        for (int i = 0; i < listSpecialDNA.Length; i++)
        {
            if (listSpecialDNA[i] >= numberOfDNAToTransform)
            {
                buttonList[i].interactable = true;

                if (canTransform && buttonList[i].name == stringButton)
                {
                    listSpecialDNA[i] = listSpecialDNA[i] - 10;
                    canTransform = false;
                }
            }
            else if (listSpecialDNA[i] <= numberOfDNAToTransform && !canTransform)
            {
                buttonList[i].interactable = false;
            }
        }
    }

    public void CanTransform(string _stringButton)
    {
        canTransform = true;
        stringButton = _stringButton;
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
