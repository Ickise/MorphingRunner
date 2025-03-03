using System;
using UnityEngine;

// L'enum qui définit les trois positions horizontales possibles.
[Serializable]
public enum HorizontalSide
{
    Left,
    Mid,
    Right
}

public class SideManager : MonoBehaviour
{
    // Ce script ne gère que le positionnement horizontal du joueur en fonction des inputs, mais nous pourrions ajouter d'autres positions si nécessaire.
    // Auparavant, cela se trouvait dans HorizontalSide.

    public static SideManager
        instance; // Singleton de SideManager pour y accéder facilement. Nous retrouverons cela dans dans d'autre script, notamment
    // les managers.

    public event Action<float>
        OnHorizontalPositionChanged = delegate { }; // Événement déclenché lorsque la position horizontale change.

    [SerializeField, Header("Settings")] private float addingXValue = 3f;

    [SerializeField, Header("References")] private InputManager inputManager;
    [SerializeField] private PlayerMovement playerMovement;

    private HorizontalSide horizontalSide;

    private void Awake()
    {
        PreInitialize();
    }

    private void PreInitialize()
    {
        // Initialisation correcte du Singleton.
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); // Nous détruisons l'objet s'il existe déjà une instance pour éviter les doublons.
        }
    }

    private void OnEnable()
    {
        // Abonnement aux différents événements.
        inputManager.LeftMoveEvent += () => Move(-1);
        inputManager.RightMoveEvent += () => Move(1);
    }

    private void OnDisable()
    {
        // Désabonnement aux différents événements pour éviter les références nulles.
        inputManager.LeftMoveEvent -= () => Move(-1);
        inputManager.RightMoveEvent -= () => Move(1);
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        horizontalSide = HorizontalSide.Mid; // Nous définissons la position initiale du joueur.
    }

    // Cette méthode permet de gérer le déplacement du joueur sur l'axe horizontal.
    private void Move(int direction)
    {
        float xPosition = 0f;

        // Nous utilisons un switch pour chaque position (Left, Middle et Right) et selon la position, cela donne différente directive au script PlayerMovement.
        switch (horizontalSide)
        {
            case HorizontalSide.Left:
                if (playerMovement.IsDodging()) return; // Nous vérifions si le joueur dodge ou non.
                xPosition = direction == -1
                    ? -addingXValue
                    : 0; // Si le joueur est déjà à gauche, il ne peut pas aller plus loin.
                horizontalSide = direction == -1 ? HorizontalSide.Left : HorizontalSide.Mid;
                break;

            case HorizontalSide.Mid:
                if (playerMovement.IsDodging()) return;
                xPosition = direction == -1 ? -addingXValue : addingXValue;
                horizontalSide =
                    direction == -1
                        ? HorizontalSide.Left
                        : HorizontalSide.Right; // Mise à jour de la position horizontale.
                break;

            case HorizontalSide.Right:
                if (playerMovement.IsDodging()) return;
                xPosition = direction == -1
                    ? 0f
                    : addingXValue; // Si le joueur est déjà à droite, il ne peut pas aller plus loin.
                horizontalSide = direction == -1 ? HorizontalSide.Mid : HorizontalSide.Right;
                break;
        }

        OnHorizontalPositionChanged.Invoke(xPosition); // Nous notifions que la position a changé.
    }

    // Cette méthode permet de récupérer la position actuelle du joueur sur l'axe horizontal.
    public HorizontalSide GetHorizontalSide()
    {
        return horizontalSide;
    }
}