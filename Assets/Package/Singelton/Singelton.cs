using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singelton : MonoBehaviour
{
    private static Singelton _instance;
    // Start is called before the first frame update
    void Start()
    {
        
    } 
    private void Awake() 
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
    }
}
