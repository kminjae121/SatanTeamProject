using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controll;

[CreateAssetMenu (menuName = "PlayerInput")]
public class InputReader : ScriptableObject, IPlayerActions
{
    public Action OnJumpHandle, OnInteractionHandle, OnEseHandle;

    public Vector3 InputVec { get; private set; }

    public bool _isJump { get; set; }
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
        if (context.performed)
            OnInteractionHandle?.Invoke();
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
        if (context.performed)
            OnEseHandle?.Invoke();
    }
}