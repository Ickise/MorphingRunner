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
    
    public event Action<float> OnPositionChanged = delegate {};
    
    [SerializeField, Header("Settings")] private float addingXValue = 3f;

    [SerializeField, Header("References")] private InputManager inputManager;
    
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
        inputManager.LeftMoveEvent += LeftMove;
        inputManager.RightMoveEvent += RightMove;
    }

    private void OnDisable()
    {
        inputManager.LeftMoveEvent -= LeftMove;
        inputManager.RightMoveEvent -= RightMove;
    }

    private void Start()
    {
        horizontalSide = HorizontalSide.Mid;
    }

    private void LeftMove()
    {
        float xPosition = 0f;
        switch (horizontalSide)
        {
            case HorizontalSide.Left:
                xPosition = -addingXValue;
                break;
            case HorizontalSide.Mid:
                xPosition = -addingXValue; 
                horizontalSide = HorizontalSide.Left;
                break;
            case HorizontalSide.Right:
                xPosition = 0;
                horizontalSide = HorizontalSide.Mid;
                break;
        }
        OnPositionChanged.Invoke(xPosition);
    }

    private void RightMove()
    {
        float xPosition = 0f;
        switch (horizontalSide)
        {
            case HorizontalSide.Left:
                xPosition = 0f;
                horizontalSide = HorizontalSide.Mid;
                break;
            case HorizontalSide.Right:
                xPosition = addingXValue;
                break;
            case HorizontalSide.Mid:
                xPosition = addingXValue; 
                horizontalSide = HorizontalSide.Right;
                break;
        }
        OnPositionChanged.Invoke(xPosition);
    }

    public HorizontalSide GetHorizontalSide()
    {
        return horizontalSide;
    }
}