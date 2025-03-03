using UnityEngine;
using UnityEngine.UI;

public class DNAToCollect : MonoBehaviour
{
    // Ce script gérer le stock de l'ADN sans aucune distinction. Il met également à jour l'UI.  
    // Lorsque le joueur récupérer de l'ADN, il pouvait l'utiliser pour devenir un TRex ou bien un humain.
    [Header("Set up")] // Il y a un effort de nomenclature, mais cela reste parfois abstrait. 
    [SerializeField]
    DNA[] listDNA = new DNA[3];

    [SerializeField]
    int[]
        listSpecialDNA =
            new int[3]; // Cette variable est inutile, nous pouvons tout simplement utiliser un int que nous incrémentons ou décrémentons.
                        // // et un enum pour connaître le type d'ADN que le joueur possède. 
    [SerializeField] int numberOfDNAToTransform = 10;

    [SerializeField] Button[] buttonList = new Button[3];

    [SerializeField] Text[] numberOfSpecialDNA = new Text[2];

    [SerializeField] int scoreToGain = 10;

    [SerializeField] AudioClip collectDNA;

    [Header("Data")] [SerializeField] string stringButton;

    bool canTransform;

    void Update()
    {
        // Nous utilisons une Update pour mettre à jour en permanence de l'UI au lieu d'utiliser un event... cela serait beaucoup plus optimisé.
        // De plus, nous mélangeons l'UI avec les collisions et la logique de gestion du stock. 
        numberOfSpecialDNA[0].text = "" + listSpecialDNA[0];
        numberOfSpecialDNA[1].text = "" + listSpecialDNA[1];

        // Pourquoi utiliser une boucle for ici ? Surtout dans une Update. Nous n'avons pas besoin d'appeler en permanence cette boucle et
        // nous n'avons même pas besoin d'avoir une boucle. En utilisant un event et si nous faisons Invoke() ou même une fonction,
        // nous pouvons très bien actualiser tout ce dont nous avons besoin. 
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
        // Nous utilisons un CompareTag, ce que nous pouvons optimiser via une autre façon de faire. 
        if (other.CompareTag("DNA"))
        {
            // L'utilisation d'une boucle for et beaucoup trop gourmande, cela n'apporte rien. 
            // Nous pouvons juste utiliser un event pour optimiser tout ça et l'invoke lors de la collision ou même une fonction. 
            for (int i = 0; i < listDNA.Length; i++)
            {
                if (other.name == listDNA[i].dnaName)
                {
                    // AudioManager._instance.PlaySFX(collectDNA);
                    listSpecialDNA[i]++;
                    Scoring.scoring.score += scoreToGain;
                    Destroy(other.gameObject);
                }
            }
        }
    }
}