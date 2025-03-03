using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
   // Ce script n'existait pas auparavant, mais c'est un concentré des scripts BoutonQuit, BoutonPlay, etc...
   // C'est un simple MenuManager pour gérer le lancement du jeu et pour le quitter. 
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
