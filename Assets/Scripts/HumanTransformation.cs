using System.Collections;
using UnityEngine;

public class HumanTransformation : MonoBehaviour
{
    [SerializeField] private bool _transformationActive;
    public bool GetTransformationActive()
    {
        return _transformationActive;
    }
  
    public void Passe(GameObject _gameobjectTrigger)
    {
        Debug.Log("ActiveAnimation");
        Transform cops1 = _gameobjectTrigger.transform.parent.GetChild(4);
        Transform cops2 = _gameobjectTrigger.transform.parent.GetChild(5);
        Animator animatorCops1 = cops1.GetComponent<Animator>();
        Animator animatorCops2 = cops2.GetComponent<Animator>();
        animatorCops1.SetBool("Back",true);
        animatorCops2.SetBool("Back",true);
        Debug.Log(animatorCops2.GetBool("Back"));
    }
}
