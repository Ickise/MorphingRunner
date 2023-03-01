using UnityEngine;

public class MorphDeathCollider : MonoBehaviour
{
    GameObject deathMenu;

    void Awake()
    {
        deathMenu = GameObject.FindGameObjectWithTag("DeathCanvas");
        deathMenu.SetActive(false);
    }
    void OnTriggerEnter(Collider other)
    {
        if (TransformationsChoices.transformationsChoices.isMorph && other.gameObject.CompareTag("Player"))
        {
            deathMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
