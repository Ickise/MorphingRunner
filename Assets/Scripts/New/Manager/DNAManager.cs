using UnityEngine;

public class DNAManager : MonoBehaviour
{
    // Ce script n'existait pas auparavant et gère le stock d'ADN pour différents types (comme Pachy, Humain, etc.).
    // Il permet d'ajouter ou d'enlever de l'ADN à chaque type, et de mettre à jour l'UI en conséquence.
    public static DNAManager instance;

    private int pachyDNAStock = 5;
    private int humanDNAStock = 5;

    private UIManager uiManager;

    private void Awake()
    {
        PreInitialize();
    }

    private void PreInitialize()
    {
        // Initialisation du Singleton, si une instance existe déjà, nous la détruisons.
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
        Initialize();
    }

    private void Initialize()
    {
        uiManager = UIManager.instance; // Nous récupèrons l'instance du UIManager pour éviter les appels inutile de UIManager.instance
        uiManager.UpdateDNAStockUI(pachyDNAStock, humanDNAStock); // Nous mettons à jour l'UI avec les stocks actuels.
    }

    public void AddDNA(DNAType.DnaType dnaType)
    {
        // Cette méthode permet d'ajouter de l'ADN au stock en fonction du type d'ADN récupéré.
        // Elle est appelée dans PlayerCollision pour ajouter de l'ADN lors de la collision.
        // Un switch est adéquat pour ce genre de fonction.
        switch (dnaType)
        {
            case DNAType.DnaType.PachyDNA:
                pachyDNAStock++;
                break;
            case DNAType.DnaType.HumanDNA:
                humanDNAStock++;
                break;
            case DNAType.DnaType.AlienDNA:
                break; // Pas de gestion pour l'ADN Alien pour l'instant.
        }

        // Après chaque ajout, nous mettons à jour l'UI avec les nouveaux stocks.
        uiManager.UpdateDNAStockUI(pachyDNAStock, humanDNAStock);
    }

    public bool ConsumeDNA(DNAType.DnaType dnaType, int amount)
    {
        // Cette méthode permet de consommer un certain nombre d'ADN du stock, en vérifiant si le joueur a assez d'ADN.
        // Si oui, l'ADN est retiré et la fonction renvoie true.
        // Si non, elle renvoie false pour indiquer que l'action est impossible.
        switch (dnaType)
        {
            case DNAType.DnaType.AlienDNA:
                return true;
            case DNAType.DnaType.PachyDNA:
                if (pachyDNAStock >= amount)
                {
                    pachyDNAStock -= amount;
                    uiManager.UpdateDNAStockUI(pachyDNAStock, humanDNAStock);
                    return true;
                }

                break;
            case DNAType.DnaType.HumanDNA:
                if (humanDNAStock >= amount)
                {
                    humanDNAStock -= amount;
                    uiManager.UpdateDNAStockUI(pachyDNAStock, humanDNAStock);
                    return true;
                }

                break;
        }

        return false; // Si le stock est insuffisant, nous renvoyons false.
    }

    public int GetDNAStock(DNAType.DnaType dnaType)
    {
        // Cette méthode retourne la quantité d'ADN d'un type spécifique.
        // Elle a été créée en prévention. 
        switch (dnaType)
        {
            case DNAType.DnaType.PachyDNA:
                return pachyDNAStock;
            case DNAType.DnaType.HumanDNA:
                return humanDNAStock;
            default:
                return 0;
        }
    }
}