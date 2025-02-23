using System;
using UnityEngine;
using UnityEngine.InputSystem; 

[CreateAssetMenu(menuName = "ScriptableObjects/Input Reader")]
public class InputManager : ScriptableObject, PlayerInputAction.IPlayerActions
{
   // Tous mes événements sont déclarés ici pour être accessibles dans d'autres scripts. Cela évite l'utilisation d'Update.
   // Le fait que l'InputManager soit un ScriptableObject permet de rendre accessible ce script facilement partout dans le projet.
   public event Action LeftMoveEvent = delegate {};
   public event Action RightMoveEvent = delegate {};
   public event Action SlowMotionEvent = delegate {};
   
   private PlayerInputAction inputActions;

   void OnEnable()
   {
      // Vérifie si les actions ne sont pas déjà instanciées pour éviter de recréer des objets inutiles
      if (inputActions == null)
      {
         // Si c'est null je crée une instance de PlayerInputAction et je définis cette classe comme gestionnaire des événements.
         inputActions = new PlayerInputAction();
         inputActions.Player.SetCallbacks(this);
      }
   }

   // J'active les inputs du joueur, cette fonction est apppelée dans le script GameManager.
   public void EnablePlayerInputs()
   {
      inputActions.Enable();
   }

   // Désactive les inputs du joueur.
   public void DisablePlayerInputs()
   {
      inputActions.Disable();
   }
   
   // Détecte l'activation du slow-motion lorsque le joueur appuie sur la touche associée.
   public void OnSlowMotion(InputAction.CallbackContext context)
   {
      if (!context.performed) return; // Cette ligne permet de déclencher l'événement uniquement si l'action est "performed".
      SlowMotionEvent.Invoke(); // Cette ligne permet notifier tous les abonnés que l'événement a été déclenché.
   }

   public void OnLeftMove(InputAction.CallbackContext context)
   {
      if (!context.performed) return;
      LeftMoveEvent.Invoke();
   }

   public void OnRightMove(InputAction.CallbackContext context)
   {
      if (!context.performed) return;
      RightMoveEvent.Invoke();
   }
}
