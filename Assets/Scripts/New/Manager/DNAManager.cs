using System;
using UnityEngine;

public class DNAManager : MonoBehaviour
{
    // Ce script va gèrer le stock d'ADN de chaque type d'ADN. Il va permettre d'en ajouter ou d'en enlever au stock. 
    // C'est un Singleton pour récupèrer facilement toutes les fonctions dans d'autre script.
    public static DNAManager instance;

    private int tRexDNAStock = 5;
    private int humanDNAStock = 5;

    private UIManager uiManager;

    private void Awake()
    {
        // J'initialise correctement le Singleton.
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        uiManager = UIManager.instance;
        uiManager.UpdateDNAStockUI(tRexDNAStock, humanDNAStock);
    }

    public void AddDNA(DNAType.DnaType dnaType)
    {
        // Cette méthode pourra être appelé dans PlayerCollision pour que lorsque le joueur touche de l'ADN alors il gagne celui-ci et l'ajoute au stock correspondant. 
        switch (dnaType)
        {
            case DNAType.DnaType.TRexDNA:
                tRexDNAStock++;
                break;
            case DNAType.DnaType.HumanDNA:
                humanDNAStock++;
                break;
            case DNAType.DnaType.AlienDNA:
                break;
        }

        uiManager.UpdateDNAStockUI(tRexDNAStock, humanDNAStock);
    }

    public bool ConsumeDNA(DNAType.DnaType dnaType, int amount)
    {
        // Cette méthode renvoie un bool pour savoir si le joueur a assez d'ADN pour se transformer et si oui, dans ce cas le joueur perd un nombre X pour changer
        // de forme et obtenir les caractéristiques de la transformation. 
        switch (dnaType)
        {
            case DNAType.DnaType.AlienDNA:
                return true;
            case DNAType.DnaType.TRexDNA:
                if (tRexDNAStock >= amount)
                {
                    tRexDNAStock -= amount;
                    uiManager.UpdateDNAStockUI(tRexDNAStock, humanDNAStock);
                    return true;
                }
                break;
            case DNAType.DnaType.HumanDNA:
                if (humanDNAStock >= amount)
                {
                    humanDNAStock -= amount;
                    uiManager.UpdateDNAStockUI(tRexDNAStock, humanDNAStock);
                    return true;
                }
                break;
        }

        return false;
    }

    public int GetDNAStock(DNAType.DnaType dnaType)
    {
        // Cette méthode permet de connaitre le nombre d'ADN en stock. Elle sera utile pour de l'UI ou d'autre fonction. 
        switch (dnaType)
        {
            case DNAType.DnaType.TRexDNA:
                return tRexDNAStock;
            case DNAType.DnaType.HumanDNA:
                return humanDNAStock;
            default:
                return 0;
        }
    }
}