//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Core/Input/CannonInputAction.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @CannonInputAction : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @CannonInputAction()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""CannonInputAction"",
    ""maps"": [
        {
            ""name"": ""Cannon"",
            ""id"": ""d7129ffd-5648-4bc9-93c6-9fe775c00927"",
            ""actions"": [
                {
                    ""name"": ""xAxis"",
                    ""type"": ""PassThrough"",
                    ""id"": ""504d0aa2-c437-4943-a220-50c0457a4e18"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""yAxis"",
                    ""type"": ""PassThrough"",
                    ""id"": ""2ba42745-57a9-4a0d-aec5-86905ea06814"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""AllowRotation"",
                    ""type"": ""Button"",
                    ""id"": ""d9ddbf80-7f04-40bd-b6da-f4c3849b9443"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""713a9fc9-5864-445c-8cc0-c0c753d6caf0"",
                    ""path"": ""<Mouse>/delta/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""xAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6423bbe7-e0bf-4533-aad0-c146410c2efc"",
                    ""path"": ""<Touchscreen>/delta/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""xAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6ed6f9e7-422a-41a1-8f1d-59f76ac0e31c"",
                    ""path"": ""<Mouse>/delta/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""yAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1427df62-3a0a-45e1-94a2-214b90490f99"",
                    ""path"": ""<Touchscreen>/delta/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""yAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9e4ae1e1-a4ca-4ce6-aee5-d10c0b90d64b"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AllowRotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4d314536-e246-49bc-9d26-1ac3603d8ac0"",
                    ""path"": ""<Touchscreen>/Press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AllowRotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Cannon
        m_Cannon = asset.FindActionMap("Cannon", throwIfNotFound: true);
        m_Cannon_xAxis = m_Cannon.FindAction("xAxis", throwIfNotFound: true);
        m_Cannon_yAxis = m_Cannon.FindAction("yAxis", throwIfNotFound: true);
        m_Cannon_AllowRotation = m_Cannon.FindAction("AllowRotation", throwIfNotFound: true);
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
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Cannon
    private readonly InputActionMap m_Cannon;
    private ICannonActions m_CannonActionsCallbackInterface;
    private readonly InputAction m_Cannon_xAxis;
    private readonly InputAction m_Cannon_yAxis;
    private readonly InputAction m_Cannon_AllowRotation;
    public struct CannonActions
    {
        private @CannonInputAction m_Wrapper;
        public CannonActions(@CannonInputAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @xAxis => m_Wrapper.m_Cannon_xAxis;
        public InputAction @yAxis => m_Wrapper.m_Cannon_yAxis;
        public InputAction @AllowRotation => m_Wrapper.m_Cannon_AllowRotation;
        public InputActionMap Get() { return m_Wrapper.m_Cannon; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CannonActions set) { return set.Get(); }
        public void SetCallbacks(ICannonActions instance)
        {
            if (m_Wrapper.m_CannonActionsCallbackInterface != null)
            {
                @xAxis.started -= m_Wrapper.m_CannonActionsCallbackInterface.OnXAxis;
                @xAxis.performed -= m_Wrapper.m_CannonActionsCallbackInterface.OnXAxis;
                @xAxis.canceled -= m_Wrapper.m_CannonActionsCallbackInterface.OnXAxis;
                @yAxis.started -= m_Wrapper.m_CannonActionsCallbackInterface.OnYAxis;
                @yAxis.performed -= m_Wrapper.m_CannonActionsCallbackInterface.OnYAxis;
                @yAxis.canceled -= m_Wrapper.m_CannonActionsCallbackInterface.OnYAxis;
                @AllowRotation.started -= m_Wrapper.m_CannonActionsCallbackInterface.OnAllowRotation;
                @AllowRotation.performed -= m_Wrapper.m_CannonActionsCallbackInterface.OnAllowRotation;
                @AllowRotation.canceled -= m_Wrapper.m_CannonActionsCallbackInterface.OnAllowRotation;
            }
            m_Wrapper.m_CannonActionsCallbackInterface = instance;
            if (instance != null)
            {
                @xAxis.started += instance.OnXAxis;
                @xAxis.performed += instance.OnXAxis;
                @xAxis.canceled += instance.OnXAxis;
                @yAxis.started += instance.OnYAxis;
                @yAxis.performed += instance.OnYAxis;
                @yAxis.canceled += instance.OnYAxis;
                @AllowRotation.started += instance.OnAllowRotation;
                @AllowRotation.performed += instance.OnAllowRotation;
                @AllowRotation.canceled += instance.OnAllowRotation;
            }
        }
    }
    public CannonActions @Cannon => new CannonActions(this);
    public interface ICannonActions
    {
        void OnXAxis(InputAction.CallbackContext context);
        void OnYAxis(InputAction.CallbackContext context);
        void OnAllowRotation(InputAction.CallbackContext context);
    }
}
