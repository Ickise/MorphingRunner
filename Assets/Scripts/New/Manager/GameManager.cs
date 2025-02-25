using UnityEngine;

public class GameManager : MonoBehaviour
{
   [SerializeField, Header("References")] private InputManager inputManager;

   private void OnEnable()
   {
      inputManager.EnablePlayerInputs();
   }
   
   
   private void OnDisable()
   {
      inputManager.DisablePlayerInputs();
   }

   public void GameOver()
   {
      Debug.Log("GameOver");
      Time.timeScale = 0f;
   }
}
