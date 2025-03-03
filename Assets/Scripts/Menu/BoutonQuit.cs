using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoutonQuit : MonoBehaviour
{
    // Nous aurions pu concentré le tout dans un script MenuManager.
    // Nous aurions pu supprimer les fonctions vides puisque cela consomme des performances.

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Quit()
    {
        Debug.Log("Game Quit");
        Application.Quit();
    }
}