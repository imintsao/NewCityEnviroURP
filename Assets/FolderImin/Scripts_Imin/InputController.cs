using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[Serializable]

public class MoveInputEvent : UnityEvent<float, float> { }


public class InputController : MonoBehaviour
{

    NewControls newControls;

    private void Awake()
    {
        newControls = new NewControls();
    }

    private void OnEnable()
    {
        newControls.Gameplay.Enable();
        newControls.Gameplay.Look.performed += OnLookPerformed;
    }

    private void OnLookPerformed(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }
}
