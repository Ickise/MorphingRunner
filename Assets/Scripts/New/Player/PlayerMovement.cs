using UnityEngine;

public class PlayerMovement : MonoBehaviour, IUpdate
{
    // Ce script remplace MoveDecor pour que, cette fois, ça soit réellement le joueur qui se déplace.
    [SerializeField, Header("Settings")] private float dodgeSpeed = 10f;
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float speedIncreaseInterval = 10f;
    [SerializeField] private float speedIncreaseAmount = 1f;

    [SerializeField, Header("References")] private Transform playerTransform;

    [SerializeField] private SideManager sideManager;

    private bool isDodging;
    
    private Vector3 moveDirection = Vector3.forward;
    
    private float time;
    private float targetXPosition;
    
    private GameManager gameManager;

    private void OnEnable()
    {
       Subscribe();
    }

    private void OnDisable()
    {
      Unsubscribe();
    }

    private void Subscribe()
    {
        // Nous nous abonnons à l'événement de déplacement du SideManager et à l'UpdateManager.
        UpdateManager.RegisterUpdate(this);
        gameManager = GameManager.instance;
        sideManager.OnHorizontalPositionChanged += HorizontalPositionDodge;
    }

    private void Unsubscribe()
    {
        // Nous nous désabonnons pour éviter les références nulles lors de la destruction de l’objet.
        UpdateManager.UnregisterUpdate(this);
        sideManager.OnHorizontalPositionChanged -= HorizontalPositionDodge;
    }

    private void HorizontalPositionDodge(float newPosition)
    {
        // Ici, nous déclenchons le déplacement vers une nouvelle position latérale.
        targetXPosition = newPosition;
        isDodging = true;
    }

    // Nous récupérons l'état de déplacement du joueur pour l'utiliser dans d'autres scripts.
    public bool IsDodging()
    {
        return isDodging;
    }

    // Cette méthode permet de modifier la vitesse de dodge selon la transformation actuelle.
    public void SetDodgeSpeed(float speed)
    {
        dodgeSpeed = speed;
    }

    private void Move()
    {
        if (gameManager.GameIsOver()) return;

        // Nous stockons la position du joueur pour éviter d’appeler transform.position inutilement.
        var position = playerTransform.position;
        // Le MoveTowards permet d'être sûr d'atteindre la position souhaitée grâce au temps voulu, tandis qu'un lerp, plus il s'approche de la position voulue
        // plus il réalise l'action lentement.
        position = Vector3.MoveTowards(position,
            new Vector3(targetXPosition, position.y, position.z), dodgeSpeed * Time.deltaTime);
        
        playerTransform.position = position;

        // Si nous sommes suffisamment proches de la position cible, nous arrêtons le mouvement.
        if (Mathf.Abs(playerTransform.position.x - targetXPosition) <= 0.01f)
        {
            isDodging = false;
        }
    }

    public void UpdateTick()
    {
        if (gameManager.GameIsOver()) return;

        time += Time.deltaTime;

        // Nous faisons avancer le joueur tout droit en continu sans utiliser Time.unscaledDeltaTime, puisque nous voulons que le scale influe
        // la vitesse de la course pendant le slow motion.
        playerTransform.position += moveDirection * (movementSpeed * Time.deltaTime); 

        // Toutes les X secondes, nous augmentons la vitesse de déplacement.
        if (time >= speedIncreaseInterval)
        {
            movementSpeed += speedIncreaseAmount;
            time = 0f;
        }

        // Cela permet de bloquer légèrement le joueur en l'empêchant d'abuser des déplacements latéraux. Il ne peut pas passer de l'extrême droite
        // à l'extrême gauche en un clin d'oeil, mais avec un très lèger temps d'attente, et inversement.
        if (!isDodging) return;
        Move();
    }
}
