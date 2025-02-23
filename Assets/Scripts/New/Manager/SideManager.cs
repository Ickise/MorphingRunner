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
    // Ce script ne gère que le positionnement horizontal du joueur en fonction des inputs, mais je pourrais ajouter d'autres positions si nécessaire.

    public static SideManager instance; // Singleton de SideManager pour y accéder facilement.

    public event Action<float> OnPositionChanged = delegate { }; // Événement déclenché lorsque la position change.

    [SerializeField, Header("Settings")] private float addingXValue = 3f;

    [SerializeField, Header("References")] private InputManager inputManager;
    [SerializeField] private PlayerMovement playerMovement;

    private HorizontalSide horizontalSide;

    private void Awake()
    {
        // Initialisation correcte du Singleton.
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    private void OnEnable()
    {
        // Abonnement aux événements.
        inputManager.LeftMoveEvent += () => Move(-1);
        inputManager.RightMoveEvent += () => Move(1);
    }

    private void OnDisable()
    {
        // Désabonnement des événements.
        inputManager.LeftMoveEvent -= () => Move(-1);
        inputManager.RightMoveEvent -= () => Move(1);
    }

    private void Start()
    {
        horizontalSide = HorizontalSide.Mid; // Initialise de la position de départ du joueur. 
    }

    // Cette méthode permet d'indiquer au joueur sa position et ce qu'il doit faire par conséquence.
    private void Move(int direction)
    {
        // Je vérifie si le joueur est en mouvement.
        if (playerMovement.IsMoving()) return;

        float xPosition = 0f;

        switch (horizontalSide)
        {
            case HorizontalSide.Left:
                if (direction == -1) return; // Cas où le joueur ne peut pas aller plus à gauche.
                xPosition = 0;
                horizontalSide = HorizontalSide.Mid;
                break;

            case HorizontalSide.Mid:
                horizontalSide = direction == -1 ? HorizontalSide.Left : HorizontalSide.Right; // Ternaire pour déterminer la direction.
                break;

            case HorizontalSide.Right:
                if (direction == 1) return; // Cas où le joueur ne peut pas aller plus à droite.
                xPosition = 0;
                horizontalSide = HorizontalSide.Mid;
                break;
        }

        OnPositionChanged.Invoke(xPosition); // Enfin, je notifie que la position a changé.
    }

    // Cette méthode permet de récupérer la position du joueur.
    public HorizontalSide GetHorizontalSide()
    {
        return horizontalSide;
    }
}
