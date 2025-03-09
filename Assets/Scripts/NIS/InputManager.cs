using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Collections.Generic;

public class InputManager : MonoBehaviour
{
    [Serializable]
    public class InputActionBinding
    {
        public enum InputType
        {
            Vector2,
            Boolean,
            Float,
            Vector3
        }

        [Tooltip("Name of the input action as defined in the Input Action Asset")]
        public string actionName;

        [Tooltip("Type of input value to read")]
        public InputType inputType = InputType.Vector2;

        [HideInInspector]
        public InputAction inputAction;

        // Runtime values
        [HideInInspector]
        public Vector2 vector2Value;
        [HideInInspector]
        public bool boolValue;
        [HideInInspector]
        public float floatValue;
        [HideInInspector]
        public Vector3 vector3Value;

        // Triggered state
        [HideInInspector]
        public bool wasTriggered;
    }

    [Tooltip("List of input actions to bind and track")]
    public List<InputActionBinding> inputBindings = new List<InputActionBinding>();

    private void Awake()
    {

        // Enable all action maps


        // Bind input actions during Awake
        BindInputActions();
    }

    private void BindInputActions()
    {
        foreach (var binding in inputBindings)
        {
            try
            {
                // Find the input action by name
                binding.inputAction = InputSystem.actions.FindAction(binding.actionName);

                if (binding.inputAction == null)
                {
                    Debug.LogError($"Input action '{binding.actionName}' not found in the Input Action Asset.");
                    continue;
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Error binding input action '{binding.actionName}': {e.Message}");
            }
        }
    }

    private void Update()
    {
        UpdateInputValues();
    }

    private void UpdateInputValues()
    {
        foreach (var binding in inputBindings)
        {
            if (binding.inputAction == null) continue;

            // Reset triggered state
            binding.wasTriggered = false;

            // Read input based on type
            switch (binding.inputType)
            {
                case InputActionBinding.InputType.Vector2:
                    binding.vector2Value = binding.inputAction.ReadValue<Vector2>();
                    break;
                case InputActionBinding.InputType.Boolean:
                    binding.boolValue = binding.inputAction.ReadValue<float>() > 0;
                    binding.wasTriggered = binding.inputAction.triggered;
                    break;
                case InputActionBinding.InputType.Float:
                    binding.floatValue = binding.inputAction.ReadValue<float>();
                    break;
                case InputActionBinding.InputType.Vector3:
                    binding.vector3Value = binding.inputAction.ReadValue<Vector3>();
                    break;
            }
        }
    }

    /// <summary>
    /// Get the Vector2 value for a specific input action
    /// </summary>
    public Vector2 GetVector2(string actionName)
    {
        var binding = FindBinding(actionName);
        return binding != null ? binding.vector2Value : Vector2.zero;
    }

    /// <summary>
    /// Get the Boolean value for a specific input action
    /// </summary>
    public bool GetBoolean(string actionName)
    {
        var binding = FindBinding(actionName);
        return binding != null && binding.boolValue;
    }

    /// <summary>
    /// Check if a boolean input was triggered this frame
    /// </summary>
    public bool GetBooleanTriggered(string actionName)
    {
        var binding = FindBinding(actionName);
        return binding != null && binding.wasTriggered;
    }

    /// <summary>
    /// Get the Float value for a specific input action
    /// </summary>
    public float GetFloat(string actionName)
    {
        var binding = FindBinding(actionName);
        return binding != null ? binding.floatValue : 0f;
    }

    /// <summary>
    /// Get the Vector3 value for a specific input action
    /// </summary>
    public Vector3 GetVector3(string actionName)
    {
        var binding = FindBinding(actionName);
        return binding != null ? binding.vector3Value : Vector3.zero;
    }

    /// <summary>
    /// Find an input binding by action name
    /// </summary>
    private InputActionBinding FindBinding(string actionName)
    {
        return inputBindings.Find(b => b.actionName == actionName);
    }

    public void EnableActionMap(string actionMapName)
    {
        InputSystem.EnableDevice(InputSystem.GetDevice<Keyboard>());
        InputSystem.EnableDevice(InputSystem.GetDevice<Gamepad>());
        InputSystem.EnableDevice(InputSystem.GetDevice<Mouse>());

        InputActionMap actionMap = InputSystem.actions.FindActionMap(actionMapName);
        if (actionMap != null)
        {
            actionMap.Enable();
        }
        else
        {
            Debug.LogError($"Action map '{actionMapName}' not found in the Input Action Asset.");
        }
    }
}