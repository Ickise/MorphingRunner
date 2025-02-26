using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
   // Ce script est un simple MenuManager pour gérer le lancement du jeu et pour le quitter. 
   public void Menu()
   {
      Time.timeScale = 1f;
      SceneManager.LoadScene("Menu");
   }

   public void QuitGame()
   {
      Application.Quit();
   }

   public void TryAgain()
   {
      Time.timeScale = 1f;
      SceneManager.LoadScene("Game");
   }
}
