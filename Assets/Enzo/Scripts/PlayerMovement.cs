using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum Sides { Left, Mid, Right }

public class PlayerMovement : MonoBehaviour
{
    public Sides side = Sides.Mid;

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
            if (side == Sides.Mid)
            {
                xPosition = -xValue;
                side = Sides.Left;
            }
            else if (side == Sides.Right)
            {
                xPosition = 0;
                side = Sides.Mid;
            }
        }

        if (swipeRight)
        {
            if (side == Sides.Mid)
            {
                xPosition = xValue;
                side = Sides.Right;
            }
            else if (side == Sides.Left)
            {
                xPosition = 0;
                side = Sides.Mid;
            }
        }

        smoothX = Mathf.Lerp(smoothX, xPosition, Time.deltaTime * speedDodge);

        playerCharacterController.Move(((smoothX - transform.position.x) * Vector3.right));
    }
}
