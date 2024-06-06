using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer2D.Character;
using Photon.Pun;

public static class CharacterMovementAnimationKeys
{
    public const string IsCrouching = "IsCrouch";
    public const string HorizontalSpeed = "HorizontalSpeed";
    public const string VerticalSpeed = "VerticalSpeed";
    public const string IsGrounded = "IsGrounded";
    public const string IsAttacking = "IsAttacking";
    public const string OnDamage = "OnDamage";
}

[RequireComponent(typeof(Player))]
public class PlayerAnimationController : MonoBehaviourPun
{
    Animator animator;
    CharacterMovement2D playerMovement;
    private Player player;
    private DeathOnDamage playerOnDamage;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        player = GetComponent<Player>();
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<CharacterMovement2D>();
        //
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerOnDamage = GetComponent<DeathOnDamage>();

        if (playerOnDamage == null)
        {
            Debug.LogError("DeathOnDamage component not found on the player GameObject.");
        }
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            animator.SetBool(CharacterMovementAnimationKeys.IsCrouching, playerMovement.IsCrouching);
            animator.SetFloat(CharacterMovementAnimationKeys.HorizontalSpeed, playerMovement.CurrentVelocity.x / playerMovement.MaxGroundSpeed);
            animator.SetFloat(CharacterMovementAnimationKeys.VerticalSpeed, playerMovement.CurrentVelocity.y / playerMovement.JumpSpeed);
            animator.SetBool(CharacterMovementAnimationKeys.IsGrounded, playerMovement.IsGrounded);
            animator.SetBool(CharacterMovementAnimationKeys.IsAttacking, player.Weapon.IsAttacking);
            animator.SetBool(CharacterMovementAnimationKeys.OnDamage, playerOnDamage.OnDamage);
            photonView.RPC("UpdateAnimationStates", RpcTarget.Others, playerMovement.IsCrouching, playerMovement.CurrentVelocity.x, playerMovement.CurrentVelocity.y, playerMovement.IsGrounded, player.Weapon.IsAttacking, spriteRenderer.flipX, playerOnDamage.OnDamage);
        }
    }

    [PunRPC]
    public void UpdateAnimationStates(bool isCrouching, float horizontalSpeed, float verticalSpeed, bool isGrounded, bool isAttacking, bool flipX, bool onDamage)
    {
        animator.SetBool(CharacterMovementAnimationKeys.IsCrouching, isCrouching);
        animator.SetFloat(CharacterMovementAnimationKeys.HorizontalSpeed, horizontalSpeed / playerMovement.MaxGroundSpeed);
        animator.SetFloat(CharacterMovementAnimationKeys.VerticalSpeed, verticalSpeed / playerMovement.JumpSpeed);
        animator.SetBool(CharacterMovementAnimationKeys.IsGrounded, isGrounded);
        animator.SetBool(CharacterMovementAnimationKeys.IsAttacking, isAttacking);
        animator.SetBool(CharacterMovementAnimationKeys.OnDamage, onDamage);

        spriteRenderer.flipX = flipX;
    }
}
