using System.Collections;
using UnityEngine;

public class HumanTransformation : MonoBehaviour
{
    // Problème de nomenclature et de lissibilité.
    // Je pense même que cela était possible de rendre ce script et les scripts TRex plus générique.
    // En encapsulant les comportements communs dans une classe mère et en dérivant les spécificités dans des classes filles comme je l'ai fait
    // pour les nouveaux scripts et l'optimisation.
    [SerializeField] private bool _transformationActive;
    
    // Cette méthode renvoie la transformation actuelle.
    public bool GetTransformationActive()
    {
        return _transformationActive;
    }
  
    // Celle-ci est appelée pour activer l'animation de transformation d'un personnage.
    public void Passe(GameObject _gameobjectTrigger)
    {
        Debug.Log("ActiveAnimation");

        // Il y a un problème d'optimisation et de redondance puisque nous faisons la même chose deux fois et si jamais nous changeons les enfants
        // de place dans le parent, alors cela causera un problème.
        Transform cops1 = _gameobjectTrigger.transform.parent.GetChild(4);
        Transform cops2 = _gameobjectTrigger.transform.parent.GetChild(5);

        Animator animatorCops1 = cops1.GetComponent<Animator>();
        Animator animatorCops2 = cops2.GetComponent<Animator>();

        animatorCops1.SetBool("Back", true);
        animatorCops2.SetBool("Back", true);

        // Nous aurions pu enlever le Debug ou bien ne l'activer que lorsque l'application Unity est en PlayMode.
        Debug.Log(animatorCops2.GetBool("Back"));
    }
}
