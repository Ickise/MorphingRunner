// GENERATED AUTOMATICALLY FROM 'Assets/InputAction/PlayerInputAction.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInputAction : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputAction()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputAction"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""c5a25db4-b488-43c8-962a-daece85eb120"",
            ""actions"": [
                {
                    ""name"": ""SlowMotion"",
                    ""type"": ""Button"",
                    ""id"": ""4e674415-320e-4ea8-8847-da4de55ac26b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LeftMove"",
                    ""type"": ""Button"",
                    ""id"": ""1e16a114-6457-420b-ba06-9aef46a1ba79"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RightMove"",
                    ""type"": ""Button"",
                    ""id"": ""f4b25c6d-5a10-4116-9c2c-58389a7f9939"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""2d649f35-2708-4edf-aeec-4f1b93556755"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SlowMotion"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f0165982-4e6b-4bb0-8e71-fd0992340782"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a47cfa34-ce29-4259-8417-7e96e953848b"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_SlowMotion = m_Player.FindAction("SlowMotion", throwIfNotFound: true);
        m_Player_LeftMove = m_Player.FindAction("LeftMove", throwIfNotFound: true);
        m_Player_RightMove = m_Player.FindAction("RightMove", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_SlowMotion;
    private readonly InputAction m_Player_LeftMove;
    private readonly InputAction m_Player_RightMove;
    public struct PlayerActions
    {
        private @PlayerInputAction m_Wrapper;
        public PlayerActions(@PlayerInputAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @SlowMotion => m_Wrapper.m_Player_SlowMotion;
        public InputAction @LeftMove => m_Wrapper.m_Player_LeftMove;
        public InputAction @RightMove => m_Wrapper.m_Player_RightMove;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @SlowMotion.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSlowMotion;
                @SlowMotion.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSlowMotion;
                @SlowMotion.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSlowMotion;
                @LeftMove.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLeftMove;
                @LeftMove.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLeftMove;
                @LeftMove.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLeftMove;
                @RightMove.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRightMove;
                @RightMove.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRightMove;
                @RightMove.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRightMove;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @SlowMotion.started += instance.OnSlowMotion;
                @SlowMotion.performed += instance.OnSlowMotion;
                @SlowMotion.canceled += instance.OnSlowMotion;
                @LeftMove.started += instance.OnLeftMove;
                @LeftMove.performed += instance.OnLeftMove;
                @LeftMove.canceled += instance.OnLeftMove;
                @RightMove.started += instance.OnRightMove;
                @RightMove.performed += instance.OnRightMove;
                @RightMove.canceled += instance.OnRightMove;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnSlowMotion(InputAction.CallbackContext context);
        void OnLeftMove(InputAction.CallbackContext context);
        void OnRightMove(InputAction.CallbackContext context);
    }
}
