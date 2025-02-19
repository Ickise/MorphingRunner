using System;
using UnityEngine;

[Serializable]
public enum HorizontalSide
{
    Left,
    Mid,
    Right
}

public class SideManager : MonoBehaviour
{
    public static SideManager instance;

    public event Action<float> OnPositionChanged = delegate { };

    [SerializeField, Header("Settings")] private float addingXValue = 3f;

    [SerializeField, Header("References")] private InputManager inputManager;
    
    [SerializeField] private PlayerMovement playerMovement;

    private HorizontalSide horizontalSide;

    private void Awake()
    {
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
        inputManager.LeftMoveEvent += () => Move(-1);
        inputManager.RightMoveEvent += () => Move(1);
    }

    private void OnDisable()
    {
        inputManager.LeftMoveEvent -= () => Move(-1);
        inputManager.RightMoveEvent -= () => Move(direction: 1);
    }

    private void Start()
    {
        horizontalSide = HorizontalSide.Mid;
    }

    private void Move(int direction)
    {
        float xPosition = 0f;
        switch (horizontalSide)
        {
            case HorizontalSide.Left:
                if (playerMovement.IsMoving()) return;
                xPosition = direction == -1 ? -addingXValue : 0;
                horizontalSide = direction == -1 ? HorizontalSide.Left : HorizontalSide.Mid;
                break;
            case HorizontalSide.Mid:
                if (playerMovement.IsMoving()) return;
                xPosition = direction == -1 ? -addingXValue : addingXValue;
                horizontalSide = direction == -1 ? HorizontalSide.Left : HorizontalSide.Right;
                break;
            case HorizontalSide.Right:
                if (playerMovement.IsMoving()) return;
                xPosition = direction == -1 ? 0f : addingXValue;
                horizontalSide = direction == -1 ? HorizontalSide.Mid : HorizontalSide.Right;
                break;
        }
        OnPositionChanged.Invoke(xPosition);
    }

    public HorizontalSide GetHorizontalSide()
    {
        return horizontalSide;
    }
}