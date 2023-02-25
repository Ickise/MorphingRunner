using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoutonQuit : MonoBehaviour
{
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