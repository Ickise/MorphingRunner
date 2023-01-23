using UnityEngine;

public class DrawLines : MonoBehaviour
{
    Vector3 position = new Vector3(0,0,int.MaxValue);
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(transform.position, position);
    }
}
