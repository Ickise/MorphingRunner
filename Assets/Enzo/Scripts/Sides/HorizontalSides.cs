using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum HorizontalSide { Left, Mid, Right }

public class HorizontalSides : MonoBehaviour
{
    public HorizontalSide horizontalSide = HorizontalSide.Mid;

    public float xPosition;
    public float xValue;
    public float speedDodge;

    float smoothX;

    bool swipeLeft;
    bool swipeRight;

    Vector3 position;

    CharacterController playerCharacterController;

    void Start()
    {

        playerCharacterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        swipeLeft = Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.LeftArrow);
        swipeRight = Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow);

        if (swipeLeft)
        {
            if (horizontalSide == HorizontalSide.Mid)
            {
                xPosition = -xValue;
                horizontalSide = HorizontalSide.Left;
            }
            else if (horizontalSide == HorizontalSide.Right)
            {
                xPosition = 0;
                horizontalSide = HorizontalSide.Mid;
            }
        }

        if (swipeRight)
        {
            if (horizontalSide == HorizontalSide.Mid)
            {
                xPosition = xValue;
                horizontalSide = HorizontalSide.Right;
            }
            else if (horizontalSide == HorizontalSide.Left)
            {
                xPosition = 0;
                horizontalSide = HorizontalSide.Mid;
            }
        }

        smoothX = Mathf.Lerp(smoothX, xPosition, Time.deltaTime * speedDodge);

        playerCharacterController.Move(((smoothX - transform.position.x) * Vector3.right));
    }
}
