using UnityEngine;

public class DNAType : MonoBehaviour
{
    // Ce script permet d'obtenir le type d'ADN grâce à un enum et une méthode qui renvoie l'enum de l'ADN.
    // Il est très court et quasiment optionnel, mais cela me permet de respecter le single responsability.
    public enum DnaType
    {
        TRexDNA,
        HumanDNA,
    }

    [SerializeField] private DnaType dnaType;

    public DnaType GetDNAType()
    {
        return dnaType;
    }
}