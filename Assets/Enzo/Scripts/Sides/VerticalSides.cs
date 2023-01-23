using UnityEngine;

[System.Serializable]
public enum VerticalSide { Up, Mid, Down }

public class VerticalSides : MonoBehaviour
{
    HorizontalSide horizontalSide;
    public static VerticalSide verticalSide = VerticalSide.Mid;

    public float yPosition;
    public float yValue;
    public float speedToMove;

    float smoothY;

    bool swipeUp;
    bool swipeDown;

    Vector3 position;

    CharacterController playerCharacterController;

    void Start()
    {
        playerCharacterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        swipeUp = Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.UpArrow);
        swipeDown = Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);

        if (swipeUp)
        {
            if (verticalSide == VerticalSide.Mid)
            {
                yPosition = yValue;
                verticalSide = VerticalSide.Up;
            }
            else if (verticalSide == VerticalSide.Down)
            {
                yPosition = 0;
                verticalSide = VerticalSide.Mid;
            }
        }

        if (swipeDown)
        {
            if (verticalSide == VerticalSide.Mid)
            {
                yPosition = -yValue;
                verticalSide = VerticalSide.Down;
            }
            else if (verticalSide == VerticalSide.Up)
            {
                yPosition = 0;
                verticalSide = VerticalSide.Mid;
            }
        }

        smoothY = Mathf.Lerp(smoothY, yPosition, Time.deltaTime * speedToMove);

        playerCharacterController.Move(((smoothY - transform.position.y) * Vector3.up));
    }
}
