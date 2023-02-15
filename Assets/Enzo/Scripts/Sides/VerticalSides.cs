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
    [SerializeField] float invincibilityTime = 2f;
    [SerializeField] float timeToBecomeNormal = 0.2f;

    [SerializeField] MeshCollider playerMeshCollider;
    [SerializeField] Collider playerCollider;
    [SerializeField] MeshRenderer playerMeshRenderer;


    public CameraManager cameraManager;

    [Header("Data")]
    float smoothY;
    bool swipeUp;
    bool swipeDown;

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
        // swipeUp = Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.UpArrow);
        // swipeDown = Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);

        // if (swipeUp)
        // {
        //     if (verticalSide == VerticalSide.Mid)
        //     {
        //         yPosition = yValue;
        //         verticalSide = VerticalSide.Up;
        //     }
        //     if (verticalSide == VerticalSide.Down)
        //     {
        //         yPosition = 0;
        //         verticalSide = VerticalSide.Mid;
        //     }
        // }

        // if (swipeDown)
        // {
        //     if (verticalSide == VerticalSide.Mid)
        //     {
        //         yPosition = -yValue;
        //         verticalSide = VerticalSide.Down;
        //     }
        //     else if (verticalSide == VerticalSide.Up)
        //     {
        //         yPosition = 0;
        //         verticalSide = VerticalSide.Mid;
        //     }
        // }

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
            // StartCoroutine(Invincible());
        }
        if (other.CompareTag("Up") && verticalSide == VerticalSide.Down && !TransformationsChoices.transformationsChoices.isTrex)
        {
            yPosition = yValue - 3;
            verticalSide = VerticalSide.Mid;
            cameraManager._lerpBoolReverse = false;
            cameraManager._lerpBool = true;
            // StartCoroutine(Invincible());
        }
    }

    // IEnumerator Invincible()
    // {
    //     playerMeshCollider.enabled = false;
    //     playerCharacterController.enabled = false;
    //     playerCollider.enabled = false;

    //     for (float i = 0; i < invincibilityTime; i += timeToBecomeNormal)
    //     {
    //         playerMeshRenderer.enabled = !playerMeshRenderer.enabled;
    //         yield return new WaitForSeconds(timeToBecomeNormal);
    //     }

    //     playerMeshRenderer.enabled = true;
    //     playerMeshCollider.enabled = true;
    //     playerCharacterController.enabled = true;
    //     playerCollider.enabled = true;
    // }
}
