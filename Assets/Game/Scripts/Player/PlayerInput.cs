using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerInput : MonoBehaviour
{
    Rigidbody2D body;
    private struct PlayerInputConstants
    {
        public const string Horizontal = "Horizontal";
        public const string Vertical = "Vertical";
        public const string Jump = "Jump";
        public const string Attack = "Attack";
    }

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }
    public Vector2 GetMovementInput()
    {
        //Teclado
        float horizontalInput = Input.GetAxisRaw(PlayerInputConstants.Horizontal);

        if (Mathf.Approximately(horizontalInput, 0.0f))
        {
            horizontalInput = CrossPlatformInputManager.GetAxisRaw(PlayerInputConstants.Horizontal);
        }

        return new Vector2(horizontalInput, body.velocity.y);
    }

    public bool IsJumpButtonDown()
    {
        bool isKeyboardButtonDown = Input.GetKeyDown(KeyCode.Space);
        bool isMobileButtonDown = CrossPlatformInputManager.GetButtonDown(PlayerInputConstants.Jump);

        return isKeyboardButtonDown || isMobileButtonDown;
    }

    public bool IsJumpButtonHeld()
    {
        bool isKeyboardButtonHeld = Input.GetKey(KeyCode.Space);
        bool isMobileButtonHeld = CrossPlatformInputManager.GetButton(PlayerInputConstants.Jump);
        return isKeyboardButtonHeld || isMobileButtonHeld;
    }

    public bool IsCrouchButtonDown()
    {
        bool isKeyboardButtonDown = Input.GetKeyDown(KeyCode.S);
        bool isMobileButtonDown = CrossPlatformInputManager.GetAxisRaw(PlayerInputConstants.Vertical) < 0;
        return isKeyboardButtonDown || isMobileButtonDown;
    }

    public bool IsCrouchButtonUp()
    {
        bool isKeyboardButtonUp = Input.GetKey(KeyCode.S) == false;
        bool isMobileButtonUp = CrossPlatformInputManager.GetAxis(PlayerInputConstants.Vertical) >= 0;
        return isKeyboardButtonUp && isMobileButtonUp;
    }

    public bool IsAttackButtonDown()
    {
        bool isKeyboardButtonDown = Input.GetKeyDown(KeyCode.K);
        bool isMobileButtonDown = CrossPlatformInputManager.GetButton(PlayerInputConstants.Attack);
        return isKeyboardButtonDown || isMobileButtonDown;
    }
}
