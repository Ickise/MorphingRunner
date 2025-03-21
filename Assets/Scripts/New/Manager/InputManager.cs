using System;
using UnityEngine;
using UnityEngine.InputSystem; 

[CreateAssetMenu(menuName = "ScriptableObjects/Input Reader")]
public class InputManager : ScriptableObject, PlayerInputAction.IPlayerActions
{
   // Ce script n'existait pas auparavant et gère tous les événements liés aux entrées du joueur, en évitant d'utiliser l'Update.
   // Le fait que ce soit un ScriptableObject permet de rendre ce script facilement accessible depuis n'importe quel autre
   // script dans le projet et optimise puisque ce n'est que de la data.
   
   public event Action LeftMoveEvent = delegate {}; // Événement appelé lorsque le joueur se déplace vers la gauche.
   public event Action RightMoveEvent = delegate {}; // Événement appelé lorsque le joueur se déplace vers la droite.
   public event Action SlowMotionEvent = delegate {}; // Événement appelé lorsque le joueur active le slow-motion.
   
   private PlayerInputAction inputActions;

   void OnEnable()
   {
      // Je vérifie si les actions sont déjà instanciées pour éviter de recréer des objets inutiles.
      if (inputActions == null)
      {
         // Si l'instance d'inputActions est nulle, nous créeons une nouvelle instance de PlayerInputAction
         // et nous définissons ce script comme manager des callbacks pour ces actions.
         inputActions = new PlayerInputAction();
         inputActions.Player.SetCallbacks(this); 
      }
   }

   // Cela permet d'activer les Inputs du joueur dans le GameManager
   public void EnablePlayerInputs()
   {
      inputActions.Enable();
   }

   // Cela permet de désactiver les Inputs du joueur dans le GameManager
   public void DisablePlayerInputs()
   {
      inputActions.Disable();
   }
   
   // Cette méthode est appelée lorsque le slow-motion est activé (si le joueur appuie sur la touche E).
   public void OnSlowMotion(InputAction.CallbackContext context)
   {
      if (!context.performed) return;
      SlowMotionEvent.Invoke(); // Cela notifie tous les abonnés que l'événement slow-motion a été déclenché.
   }

   // Cette méthode est appelée lorsque le joueur appuie sur la touche Q.
   public void OnLeftMove(InputAction.CallbackContext context)
   {
      if (!context.performed) return;
      LeftMoveEvent.Invoke(); // Cela notifie tous les abonnés que l'événement mouvement à gauche a été déclenché.
   }

   // Cette méthode est appelée lorsque le joueur appuie sur la touche D.
   public void OnRightMove(InputAction.CallbackContext context)
   {
      if (!context.performed) return;
      RightMoveEvent.Invoke(); // Cela notifie tous les abonnés que l'événement mouvement à droite a été déclenché.
   }
}