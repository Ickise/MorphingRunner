using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoutonLoadGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    public void LoadScene (string _SceneLoadName)
    {
        Debug.Log(_SceneLoadName + "Scene Load");
        SceneManager.LoadScene(_SceneLoadName);
    }
}
