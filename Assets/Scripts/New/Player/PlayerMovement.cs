using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField, Header("Settings")] private float dodgeSpeed = 10f;

    [SerializeField, Header("References")] private Transform playerTransform;
    [SerializeField] private SideManager sideManager;

    private bool isMoving = false;

    private void OnEnable()
    {
        sideManager.OnPositionChanged += MoveToPosition;
    }

    private void OnDisable()
    {
        sideManager.OnPositionChanged -= MoveToPosition;
    }

    private void MoveToPosition(float newPosition)
    {
        if (!isMoving)
        {
            StartCoroutine(OnMove(newPosition));
        }
    }

    public bool IsMoving()
    {
        return isMoving;
    }

    private IEnumerator OnMove(float xPosition)
    {
        isMoving = true;
        Vector3 playerPosition = playerTransform.position;

        while (Mathf.Abs(playerPosition.x - xPosition) > 0.1f)
        {
            var xSmoothValue = Mathf.Lerp(playerPosition.x, xPosition, Time.deltaTime * dodgeSpeed);
            playerPosition = Vector3.right * xSmoothValue;
            playerTransform.position = playerPosition;
            yield return null;
        }

        playerTransform.position = Vector3.right * xPosition;
        isMoving = false;
    }
    
    public void SetSpeed(float speed)
    {
        dodgeSpeed = speed;
    }
}