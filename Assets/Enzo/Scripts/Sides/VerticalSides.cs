using System.Collections;
using UnityEngine;

[System.Serializable]
public enum VerticalSide { Up, Mid, Down }

public class VerticalSides : MonoBehaviour
{
    [Header("Set up")]
    [SerializeField] float yPosition;
    [SerializeField] float yValue;
    [SerializeField] float speedToMove;

    public CameraManager cameraManager;

    [Header("Data")]
    float smoothY;

    Vector3 position;

    public static VerticalSide verticalSide = VerticalSide.Mid;

    CharacterController playerCharacterController;

    void Start()
    {
        verticalSide = VerticalSide.Mid;
        playerCharacterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        smoothY = Mathf.Lerp(smoothY, yPosition, Time.deltaTime * speedToMove);

        playerCharacterController.Move(((smoothY - transform.position.y) * Vector3.up));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Down") && verticalSide == VerticalSide.Mid && !TransformationsChoices.transformationsChoices.isTrex)
        {
            yPosition = -yValue;
            verticalSide = VerticalSide.Down;
            cameraManager._lerpBoolReverse = true;
            cameraManager._lerpBool = true;
            cameraManager._lerpBoolUp = false;
        }
        if (other.CompareTag("Up") && verticalSide == VerticalSide.Down && !TransformationsChoices.transformationsChoices.isTrex)
        {
            yPosition = yValue - 3;
            verticalSide = VerticalSide.Mid;
            cameraManager._lerpBoolUp = true;
            cameraManager._lerpBoolReverse = false;
            cameraManager._lerpBool = true;   
        }
    }

}
