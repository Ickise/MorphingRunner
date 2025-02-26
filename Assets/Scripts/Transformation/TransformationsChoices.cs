using System.Collections;
using UnityEngine;

public class TransformationsChoices : MonoBehaviour
{
    // Le script gère trop de choses (les transformations, le slow motion, les collisions, etc...), il aurait été plus judicieux de séparer les responsabilités.
    // La nomenclature est incorrecte, il faut utiliser [SerializeField] et mettre les variables en private pour éviter de les mettre en public.
    // Je pouvais également faire des getter/setter ou bien des méthodes qui sont publics et qui permettent de modifier les variables privées.
    [Header("Set up")] [SerializeField] GameObject transformationChoices;
    public GameObject playerMesh;
    [SerializeField] GameObject _deathEcran;

    [SerializeField] GameObject[] meshList;
    [SerializeField] GameObject[] listToDestroy;
    public bool invisibility;
    GameObject[] listToDestroyCollider;

    [SerializeField] int scoreToWin = 10;

    [SerializeField] Collider playerCollider;
    [SerializeField] CameraManager _cameraManager;

    [Header("Data")]

    // Ici j'aurai dû centraliser le tout dans une classe.
    bool slowMotionActive = false;

    public bool isTrex;
    public bool isHuman;
    public bool isMorph;

    public static TransformationsChoices transformationsChoices;

    private void Awake()
    {
        // Cette variable static est mal initialisée, il y a un risque de duplication. 
        transformationsChoices = this;
        transformationChoices.SetActive(false);

        isMorph = true;
    }

    void Update()
    {
        // L'Update s'exécute chaque frame alors que j'aurai pu utiliser un Event/InputManager.
        // J'aurai pu également faire cela : if (!Input.GetKeyDown(KeyCode.E)) return;
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!slowMotionActive)
            {
                StartCoroutine(SlowMotion());
            }
            else
            {
                StopCoroutine(SlowMotion()); // Cela ne stoppe pas correctement la coroutine. Je pouvais simplement mettre if(!slowMotionActive) yield break; dans la coroutine.
                ReturnInRealTime();
            }
        }
    }

    IEnumerator SlowMotion()
    {
        slowMotionActive = true;
        Time.timeScale = 0.5f;
        transformationChoices.SetActive(true);

        while
            (slowMotionActive) // Le while ne sert à rien, il aurait suffit de mettre un yield return new WaitForSeconds(1.5f); pour éviter un risque de boucle infinie.
        {
            // WaitForSeconds dépend du timeScale, j'aurai dû utiliser WaitForSecondsRealtime.
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
        // Il y a une redondance du code, le schéma est le même dans les autres transformations : désactiver le collider, modifier les booléens, instantier un nouvel objet,
        // Changer le mesh, activer la caméra, etc...
        // Il aurait été plus judicieux d'utiliser de l'héritage. 
        playerCollider.enabled = false;
        isTrex = true;
        isHuman = false;
        isMorph = false;

        // Enormément de valeurs hardcodées.
        GameObject Inst = Instantiate(meshList[2], new Vector3(playerMesh.transform.position.x, 0.1f, -6),
            Quaternion.identity, playerMesh.transform.parent.transform);
        Destroy(playerMesh, 0);
        playerMesh = Inst;
        _cameraManager._lerpBool = true;
        _cameraManager._lerpBoolTRex = true;
        _cameraManager._lerpBoolTRexReverse = false;
        ReturnInRealTime();
        StartCoroutine(TRexMove());
        StartCoroutine(TRexChrono());
    }

    IEnumerator TRexMove()
    {
        yield return new WaitForSeconds(1f);

        // Le TryGetCompnent aurai été plus approprié pour éviter une erreur si le component est absent.
        PointMove pointMove = playerMesh.transform.GetChild(3).GetComponent<PointMove>();
        pointMove._Jambe = true;
    }

    public void ChangeIntoHuman()
    {
        // Même chose que pour ChangeIntoTRex.
        playerCollider.enabled = false;
        isHuman = true;
        isTrex = false;
        isMorph = false;
        // Pourquoi le transform.parent.transform ?
        GameObject Inst = Instantiate(meshList[0], new Vector3(playerMesh.transform.position.x, 0, 1),
            Quaternion.identity, playerMesh.transform.parent.transform);
        Destroy(playerMesh, 0);
        playerMesh = Inst;
        _cameraManager._lerpBoolTRex = false;
        _cameraManager._lerpBoolTRexReverse = true;
        ReturnInRealTime();
        StartCoroutine(HumanChrono());
    }

    public void ChangeIntoMorph()
    {
        // Même chose que pour ChangeIntoTRex.
        playerCollider.enabled = true;
        isMorph = true;
        isHuman = false;
        isTrex = false;
        GameObject Inst = Instantiate(meshList[1], new Vector3(playerMesh.transform.position.x, 0, 0),
            Quaternion.identity, playerMesh.transform.parent.transform);
        Destroy(playerMesh, 0);
        playerMesh = Inst;
        _cameraManager._lerpBoolTRex = false;
        _cameraManager._lerpBoolTRexReverse = true;
        ReturnInRealTime();
    }

    void OnTriggerEnter(Collider other)
    {
        // Beaucoup de CompareTag alors que ce n'est pas une bonne pratique.
        if (invisibility) return;
        if (other.gameObject.CompareTag("CheckPoint") && isHuman)
        {
            Passe(other.gameObject);
        }

        if (!isTrex && other.gameObject.CompareTag("Collider"))
        {
            _deathEcran.SetActive(true);

            // Mauvaise pratique, très coûteuse en performance.
            listToDestroyCollider = GameObject.FindGameObjectsWithTag("Collider");

            // Le foreach ici pourrait poser problème si un objet est détruit pendant l’itération.
            foreach (var item in listToDestroy)
            {
                Destroy(item);
            }

            foreach (var item in listToDestroyCollider)
            {
                Destroy(item);
            }
        }

        if (isTrex && other.gameObject.CompareTag("CheckPoint"))
        {
            Destroy(other.gameObject);
        }
    }

    public void Passe(GameObject _gameobjectTrigger)
    {
        // Cela ne devrait pas être ici, il faudrait centraliser le tout dans une autre classe comme la plupart des méthodes de ce script.
        // De plus, la méthode est extrêmement bizarre, nous avons hardcodé pour récupéré l'enfant et nous avons mis les même lignes de code au lieu de refacto.
        Transform cops1 = _gameobjectTrigger.transform.parent.GetChild(4);
        Transform cops2 = _gameobjectTrigger.transform.parent.GetChild(5);
        Animator animatorCops1 = cops1.GetComponent<Animator>();
        Animator animatorCops2 = cops2.GetComponent<Animator>();
        animatorCops1.SetBool("Back", true);
        animatorCops2.SetBool("Back", true);
    }

    // Au lieu de mettre un ternaire pour les secondes, nous avions recréé une autre coroutine. L'invisibility n'était pas utile, c'est pour cela que je dis ça. 
    public IEnumerator TRexChrono()
    {
        yield return new WaitForSeconds(30f);
        invisibility = true;
        ChangeIntoMorph();
        StartCoroutine(InvisibilityChrono());
    }

    public IEnumerator HumanChrono()
    {
        yield return new WaitForSeconds(10f);
        ChangeIntoMorph();
    }

    public IEnumerator InvisibilityChrono()
    {
        yield return new WaitForSeconds(3f);
        invisibility = false;
    }
}