using UnityEngine;

public class MoveDecor : MonoBehaviour
{
    // Ce script est une honte...
    // Il se trouve sur chacun des éléments du décor qui se déplacent.  
    [SerializeField] private float _speed = 0;
    private void Start()
    {
        // Chaque éléments du décor fait un FindGameObjectWithTag("Player") pour trouver le joueur. En terme de performance, c'est très mauvais.
        GameObject _player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        // Chaque éléments du décor se déplace en fonction de sa vitesse. En terme de performance, c'est enocre une fois très mauvais puisque nous faisons transform.position
        // et le tout dans une Update.  
        // Nous faisons également un Destroy pour détruire l'objet si il est trop loin. Les performances nous demandent de l'aide je pense...
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
