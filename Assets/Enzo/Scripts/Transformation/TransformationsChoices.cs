using System.Collections;
using UnityEngine;

public class TransformationsChoices : MonoBehaviour
{
    [Header("Set up")]
    [SerializeField] GameObject transformationChoices;
    public GameObject playerMesh;
    [SerializeField] GameObject _deathEcran;

    [SerializeField] int scoreToWin = 10;

    [SerializeField] GameObject[] meshList;

    [SerializeField] Collider playerCollider;
    [SerializeField] CameraManager _cameraManager;

    [Header("Data")]

    bool slowMotionActive = false;
    public bool isTrex;
    public bool isHuman;
    public bool isMorph;

    public static TransformationsChoices transformationsChoices;

    private void Awake()
    {
        transformationsChoices = this;
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

    void ReturnInRealTime()
    {
        Time.timeScale = 1;
        transformationChoices.SetActive(false);
        slowMotionActive = false;
    }

    public void ChangeIntoTRex()
    {
        playerCollider.enabled = false;
        isTrex = true;
        isHuman = false;
        isMorph = false;

        GameObject Inst = Instantiate(meshList[2], new Vector3(playerMesh.transform.position.x, 0.1f, -6), Quaternion.identity, playerMesh.transform.parent.transform);
        Destroy(playerMesh, 0);
        playerMesh = Inst;
        _cameraManager._lerpBool = true;
        _cameraManager._lerpBoolTRex = true;
        _cameraManager._lerpBoolTRexReverse = false;
        ReturnInRealTime();
        StartCoroutine(TRexMove());
    }
    IEnumerator TRexMove()
    {
        yield return new WaitForSeconds(1f);
        PointMove pointMove = playerMesh.transform.GetChild(3).GetComponent<PointMove>();
        pointMove._Jambe = true;
    }

    public void ChangeIntoHuman()
    {
        playerCollider.enabled = false;
        isHuman = true;
        isTrex = false;
        isMorph = false;
        GameObject Inst = Instantiate(meshList[0], new Vector3(playerMesh.transform.position.x, 0, 1), Quaternion.identity, playerMesh.transform.parent.transform);
        Destroy(playerMesh, 0);
        playerMesh = Inst;
        _cameraManager._lerpBoolTRex = false;
        _cameraManager._lerpBoolTRexReverse = true;
        ReturnInRealTime();
    }

    public void ChangeIntoMorph()
    {
        playerCollider.enabled = true;
        isMorph = true;
        isHuman = false;
        isTrex = false;
        GameObject Inst = Instantiate(meshList[1], new Vector3(playerMesh.transform.position.x, 0, 0), Quaternion.identity, playerMesh.transform.parent.transform);
        Destroy(playerMesh, 0);
        playerMesh = Inst;
        _cameraManager._lerpBoolTRex = false;
        _cameraManager._lerpBoolTRexReverse = true;
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
        if (isTrex && other.gameObject.CompareTag("CheckPoint") || other.gameObject.CompareTag("Sign"))
        {
            Destroy(other.gameObject);
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