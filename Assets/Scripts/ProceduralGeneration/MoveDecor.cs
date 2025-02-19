using UnityEngine;

public class MoveDecor : MonoBehaviour
{
    [SerializeField] private float _speed = 0;
    private void Start()
    {
        GameObject _player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        transform.position = transform.position + (new Vector3(0, 0, -1) * Time.deltaTime * _speed);
        if (transform.position.z < -40 && !TransformationsChoices.transformationsChoices.isTrex)
        {
            Destroy(this.gameObject, 0);
        }
    }
    public void SetSpeedAdd(float _speedAdd)
    {
        _speed = _speed + _speedAdd;
    }
    public void SetSpeed(float speed)
    {
        _speed = speed;
    }
}
