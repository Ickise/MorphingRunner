using System.Collections;
using UnityEngine;

public class TransformationsChoices : MonoBehaviour
{
    [SerializeField] GameObject transformationChoices;
    [SerializeField] GameObject playerMesh;

    [SerializeField] Mesh[] meshList = new Mesh[3];

    bool slowMotionActive = false;

    void Start()
    {
        transformationChoices.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!slowMotionActive)
            {
                StartCoroutine(SlowMotion());
            }
            else
            {
                StopCoroutine(SlowMotion());
                ReturnInRealTime();
            }
        }
    }

    IEnumerator SlowMotion()
    {
        slowMotionActive = true;
        Time.timeScale = 0.5f;
        transformationChoices.SetActive(true);

        while (slowMotionActive)
        {
            yield return null;
        }
    }

    public void ChangeMesh(int meshIndex)
    {
        playerMesh.GetComponent<MeshFilter>().mesh = meshList[meshIndex];
        playerMesh.GetComponent<MeshCollider>().sharedMesh = meshList[meshIndex];
        ReturnInRealTime();
    }

    void ReturnInRealTime()
    {
        Time.timeScale = 1;
        transformationChoices.SetActive(false);
        slowMotionActive = false;
    }
}
