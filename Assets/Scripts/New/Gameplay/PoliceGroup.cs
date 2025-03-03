using System.Collections.Generic;
using UnityEngine;

public class PoliceGroup : MonoBehaviour
{
    // Ce script se trouve sur les policières pour qu'elles puissent jouer leur animation lorsque le joueur est en humain. 
    // Cette fonctionnalité se retrouvait dans TransformationChoices, cela permet de bien le séparer et de faire du Single Responsability Principle.
    [SerializeField] private List<Animator> policeAnimators;

    public void ActivateBack()
    {
        foreach (Animator animator in policeAnimators)
        {
            animator.SetBool("Back", true);
        }
    }
}