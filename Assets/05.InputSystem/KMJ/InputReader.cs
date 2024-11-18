using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controll;

[CreateAssetMenu (menuName = "PlayerInput")]
public class InputReader : ScriptableObject, IPlayerActions
{
    public Action OnJumpHandle;
    public Action OnInteractionHandle;
    public Action OnThrow;
    public Action OnRunHandle;

    public Vector3 InputVec { get; private set; }

    public Vector3 RunVec { get; private set; }

    public bool _isJump { get; set; }

    public bool isRun { get; set; }

    public bool isPressRun { get; set; }
    private Controll _controll;
    

    private void OnEnable()
    {
        if(_controll == null)
        {
            _controll = new Controll();
            _controll.Player.AddCallbacks(this);
        }
        _controll.Player.Enable();
    }
    public void OnInteraction(InputAction.CallbackContext context)
    {
        Debug.Log("밖");
        if (context.phase == InputActionPhase.Started)
        {
            OnInteractionHandle?.Invoke();
            Debug.Log("인보크");
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _isJump = true;
            OnJumpHandle?.Invoke();
        }
        else
            _isJump = false;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        InputVec = context.ReadValue<Vector3>();
    }

    public void OnEsc(InputAction.CallbackContext context)
    {
        
    }

    public void OnUseItem(InputAction.CallbackContext context)
    {

    }

    public void OnThrowItem(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnThrow?.Invoke();
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isPressRun = true;
            OnRunHandle?.Invoke();
        }
        else
            isPressRun = false;
    }

    public void OnRunMode2(InputAction.CallbackContext context)
    {
        RunVec = context.ReadValue<Vector3>();
    }
}
