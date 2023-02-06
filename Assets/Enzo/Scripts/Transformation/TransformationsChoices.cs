using System.Collections;
using UnityEngine;

public class TransformationsChoices : MonoBehaviour
{
    [SerializeField] GameObject transformationChoices;
    [SerializeField] GameObject playerMesh;
    [SerializeField] GameObject trexTransformation;
    [SerializeField] GameObject _deathEcran;


    [SerializeField] Mesh[] meshList = new Mesh[3];

    [SerializeField] private bool _transformationActive;

    bool slowMotionActive = false;
    public bool isTrex;
    public bool isHuman;

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
            yield return new WaitForSeconds(1.5f);
            slowMotionActive = false;
            Time.timeScale = 1f;
            transformationChoices.SetActive(false);
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
        isTrex = true;
        isHuman = false;
        playerMesh.GetComponent<MeshFilter>().mesh = meshList[0];
        playerMesh.GetComponent<MeshCollider>().sharedMesh = meshList[0];
        ReturnInRealTime();
    }

    public void ChangeIntoHuman()
    {
        isHuman = true;
        isTrex = false;
        playerMesh.GetComponent<MeshFilter>().mesh = meshList[2];
        playerMesh.GetComponent<MeshCollider>().sharedMesh = meshList[2];
        ReturnInRealTime();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CheckPoint") && isHuman)
        {
            Passe(other.gameObject);
        }
        if (other.gameObject.CompareTag("Collider") && !isTrex)
        {
            _deathEcran.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void Passe(GameObject _gameobjectTrigger)
    {
        Transform cops1 = _gameobjectTrigger.transform.parent.GetChild(4);
        Transform cops2 = _gameobjectTrigger.transform.parent.GetChild(5);
        Animator animatorCops1 = cops1.GetComponent<Animator>();
        Animator animatorCops2 = cops2.GetComponent<Animator>();
        animatorCops1.SetBool("Back", true);
        animatorCops2.SetBool("Back", true);
    }
}
