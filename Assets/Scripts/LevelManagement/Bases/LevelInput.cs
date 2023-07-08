using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelInput : LevelComponent
{
    private NewInputControls inputControls;

    protected override void Awake()
    {
        base.Awake();

        inputControls = new NewInputControls();
    }

    private void OnEnable()
    {
        inputControls.Enable();
        inputControls.DefaultMap.Enable();

        inputControls.DefaultMap.PauseGame.performed += Escape_performed;
        inputControls.DefaultMap.PauseGame.canceled += Escape_canceled;
    }

    private void OnDisable()
    {
        inputControls.Disable();
        inputControls.DefaultMap.Disable();

        inputControls.DefaultMap.PauseGame.performed -= Escape_performed;
        inputControls.DefaultMap.PauseGame.canceled -= Escape_canceled;
    }

    private void Escape_performed(InputAction.CallbackContext obj) { level.Settings.Events.OnEscapePressed?.Invoke(); }
    private void Escape_canceled(InputAction.CallbackContext obj) => level.Settings.Events.OnEscapeReleased?.Invoke();
}
