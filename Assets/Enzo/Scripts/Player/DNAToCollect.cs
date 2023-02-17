using UnityEngine;
using UnityEngine.UI;

public class DNAToCollect : MonoBehaviour
{
    [Header("Set up")]
    [SerializeField] DNA[] listDNA = new DNA[3];

    [SerializeField] int[] listSpecialDNA = new int[3];
    [SerializeField] int numberOfDNAToTransform = 10;

    [SerializeField] Button[] buttonList = new Button[3];

    [SerializeField] Text[] numberOfSpecialDNA = new Text[2];

    [SerializeField] int scoreToGain = 10;
    
    [Header("Data")]
    [SerializeField] string stringButton;

    bool canTransform;

    void Update()
    {
        numberOfSpecialDNA[0].text = "" + listSpecialDNA[0];
        numberOfSpecialDNA[1].text = "" + listSpecialDNA[1];

        for (int i = 0; i < listSpecialDNA.Length; i++)
        {
            if (listSpecialDNA[i] >= numberOfDNAToTransform)
            {
                buttonList[i].interactable = true;

                if (canTransform && buttonList[i].name == stringButton)
                {
                    listSpecialDNA[i] = listSpecialDNA[i] - numberOfDNAToTransform;
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
                if (other.name == listDNA[i].dnaName)
                {
                    listSpecialDNA[i]++;
                    Scoring.scoring.score += scoreToGain;
                    Destroy(other.gameObject);
                }
            }
        }
    }
}