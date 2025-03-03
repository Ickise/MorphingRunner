using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IUpdate
{
    // Ce script permet de remplacer MoveDecor pour que cette fois c'est réellement le joueur qui se déplace.
    [SerializeField, Header("Settings")] private float dodgeSpeed = 10f;
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float speedIncreaseInterval = 10f;
    [SerializeField] private float speedIncreaseAmount = 1f;

    [SerializeField, Header("References")] private Transform playerTransform;

    [SerializeField] private SideManager sideManager;

    private bool isMoving;

    private Vector3 moveDirection = Vector3.forward;

    private float time;
    private float targetXPosition;

    private GameManager gameManager;

    private void OnEnable()
    {
        // Abonnement à l'événement de déplacement du SideManager.
        UpdateManager.RegisterUpdate(this);
        gameManager = GameManager.instance;
        sideManager.OnPositionChanged += DodgeToPosition;
    }

    private void OnDisable()
    {
        // Désabonnement à l'événement de déplacement du SideManager. Cela permet d'éviter les références nulles lors de la destruction de l'objet.
        UpdateManager.UnregisterUpdate(this);
        sideManager.OnPositionChanged -= DodgeToPosition;
    }

    private void DodgeToPosition(float newPosition)
    {
        // Ici, je déclenche le déplacement vers une nouvelle position si le joueur n'est pas déjà en mouvement.
        targetXPosition = newPosition;

        isMoving = true;
    }

    // Je récupère l'état de déplacement du joueur pour l'utiliser dans d'autre script
    public bool IsMoving()
    {
        return isMoving;
    }

    // Cela permet de modifier la vitesse de dodge selon la transformation.
    public void SetDodgeSpeed(float speed)
    {
        dodgeSpeed = speed;
    }

    private void Move()
    {
        // Je stocke la position du joueur pour éviter les transform.position inutiles.

        var position = playerTransform.position;
        position = Vector3.MoveTowards(position,
            new Vector3(targetXPosition, position.y, position.z), dodgeSpeed * Time.deltaTime);
        playerTransform.position = position;

        if (Mathf.Abs(playerTransform.position.x - targetXPosition) <= 0.01f)
        {
            isMoving = false;
        }
    }

    public void UpdateTick()
    {
        if (gameManager.GameIsOver()) return;

        time += Time.deltaTime;

        playerTransform.position += moveDirection * (movementSpeed * Time.deltaTime);

        if (time >= speedIncreaseInterval)
        {
            movementSpeed += speedIncreaseAmount;
            time = 0f;
        }

        if (!isMoving) return;
        Move();
    }
}