using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "ScriptableObjects/Input Reader")]
public class InputManager : ScriptableObject, PlayerInputAction.IPlayerActions
{
   public event Action LeftMoveEvent = delegate {};
   public event Action RightMoveEvent = delegate {};
   public event Action SlowMotionEvent = delegate {};
   
   private PlayerInputAction inputActions;

   void OnEnable()
   {
      if (inputActions == null)
      {
         inputActions = new PlayerInputAction();
         inputActions.Player.SetCallbacks(this);
      }
   }

   public void EnablePlayerInputs()
   {
      inputActions.Enable();
   }

   public void DisablePlayerInputs()
   {
      inputActions.Disable();
   }
   
   public void OnSlowMotion(InputAction.CallbackContext context)
   {
      if (!context.performed) return;
      SlowMotionEvent.Invoke();
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
