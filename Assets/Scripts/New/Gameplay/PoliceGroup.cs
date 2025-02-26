using System.Collections.Generic;
using UnityEngine;

public class PoliceGroup : MonoBehaviour
{
    [SerializeField] private List<Animator> policeAnimators;

    public void ActivateBack()
    {
        foreach (Animator animator in policeAnimators)
        {
            animator.SetBool("Back", true);
        }
    }
}