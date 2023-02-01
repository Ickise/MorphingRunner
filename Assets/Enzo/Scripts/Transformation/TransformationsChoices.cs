using System.Collections;
using UnityEngine;

public class TransformationsChoices : MonoBehaviour
{
    [SerializeField] GameObject transformationChoices;
    [SerializeField] GameObject playerMesh;
    [SerializeField] GameObject trexTransformation;

    [SerializeField] Mesh[] meshList = new Mesh[3];

    [SerializeField] private bool _transformationActive;

    bool slowMotionActive = false;
    bool isTrex;
    bool isHuman;

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

    // public void ChangeMesh(int meshIndex)
    // {
    //     playerMesh.GetComponent<MeshFilter>().mesh = meshList[meshIndex];
    //     playerMesh.GetComponent<MeshCollider>().sharedMesh = meshList[meshIndex];
    //     ReturnInRealTime();
    // }

    void ReturnInRealTime()
    {
        Time.timeScale = 1;
        transformationChoices.SetActive(false);
        slowMotionActive = false;
    }

    public void ChangeIntoTRex()
    {
        ReturnInRealTime();
        Instantiate(trexTransformation, playerMesh.transform.position, Quaternion.identity);
        Destroy(playerMesh.gameObject);
        isTrex = true;
        isHuman = false;
    }

    public void ChangeIntoHuman()
    {
        ReturnInRealTime();
        playerMesh.GetComponent<MeshFilter>().mesh = meshList[2];
        playerMesh.GetComponent<MeshCollider>().sharedMesh = meshList[2];
        isHuman = true;
        isTrex = false;
    }

    public void Passe(GameObject _gameobjectTrigger)
    {
        if (isHuman)
        {
            Transform cops1 = _gameobjectTrigger.transform.parent.GetChild(4);
            Transform cops2 = _gameobjectTrigger.transform.parent.GetChild(5);
            Animator animatorCops1 = cops1.GetComponent<Animator>();
            Animator animatorCops2 = cops2.GetComponent<Animator>();
            animatorCops1.SetBool("Back", true);
            animatorCops2.SetBool("Back", true);
        }
    }
}
