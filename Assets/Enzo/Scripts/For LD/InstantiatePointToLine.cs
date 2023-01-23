using UnityEngine;

public class InstantiatePointToLine : MonoBehaviour
{
    [SerializeField] GameObject startPoint1;
    [SerializeField] GameObject startPoint2;
    [SerializeField] GameObject startPoint3;
    [SerializeField] GameObject startPoint4;

    private void Start()
    {
        Instantiate(startPoint1, startPoint1.transform.position, Quaternion.identity);
        Instantiate(startPoint2, startPoint2.transform.position, Quaternion.identity);
        Instantiate(startPoint3, startPoint3.transform.position, Quaternion.identity);
        Instantiate(startPoint4, startPoint4.transform.position, Quaternion.identity);
    }
}
