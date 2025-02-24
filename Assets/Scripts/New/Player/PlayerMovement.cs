using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Ce script permet de remplacer MoveDecor pour que cette fois c'est réellement le joueur qui se déplace.
    [SerializeField, Header("Settings")] private float dodgeSpeed = 10f;
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float speedIncreaseInterval = 10f;
    [SerializeField] private float speedIncreaseAmount = 1f;
    
    [SerializeField, Header("References")] private Transform playerTransform;
    [SerializeField] private SideManager sideManager;

    private bool isMoving = false;
    private Vector3 moveDirection = Vector3.forward;

    private void OnEnable()
    {
        // Abonnement à l'événement de déplacement du SideManager.
        sideManager.OnPositionChanged += MoveToPosition;
        StartCoroutine(IncreaseSpeedOverTime());
    }

    private void OnDisable()
    {
        // Désabonnement à l'événement de déplacement du SideManager. Cela permet d'éviter les références nulles lors de la destruction de l'objet.
        sideManager.OnPositionChanged -= MoveToPosition;
        StopAllCoroutines();
    }
    
    // Malheureusement, pour que mon personnage se déplace en permanence, il faut que j'utilise une Update.
    private void Update()
    {
        if (!isMoving)
        {
            playerTransform.position += moveDirection * movementSpeed * Time.deltaTime;
        }
    }

    private void MoveToPosition(float newPosition)
    {
        // Ici, je déclenche le déplacement vers une nouvelle position si le joueur n'est pas déjà en mouvement.
        if (!isMoving)
        {
            StartCoroutine(OnMove(newPosition));
        }
    }

    // Je récupère l'état de déplacement du joueur pour l'utiliser dans d'autre script
    public bool IsMoving()
    {
        return isMoving;
    }

    // La coroutine gère le déplacement fluide du joueur.
    private IEnumerator OnMove(float xPosition)
    {
        isMoving = true;
        Vector3 startPosition = playerTransform.position; // Je stocke la position de départ du joueur pour éviter les transform.position inutiles.

        // Je peux améliorer cette partie en enlevant le while.
        while (Mathf.Abs(playerTransform.position.x - xPosition) > 0.01f)
        {
            // Le fait de mettre un MoveTowards permet de déplacer le joueur de manière fluide et constante peu importe les FPS. 
            playerTransform.position = Vector3.MoveTowards(
                playerTransform.position,
                new Vector3(xPosition, startPosition.y, startPosition.z),
                dodgeSpeed * Time.deltaTime
            );
            yield return null;
        }

        // Cette ligne me permet de m'assurer que la position finale est exacte pour éviter les erreurs de précision futures.
        playerTransform.position = new Vector3(xPosition, startPosition.y, startPosition.z);
        isMoving = false;
    }
    
    // Cela permet de modifier la vitesse de dodge selon la transformation.
    public void SetDodgeSpeed(float speed)
    {
        dodgeSpeed = speed;
    }
    
    // La coroutine permet d'augmenter la speed toutes les X secondes. 
    private IEnumerator IncreaseSpeedOverTime()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(speedIncreaseInterval);
            movementSpeed += speedIncreaseAmount;
        }
    }
}