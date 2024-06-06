using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
using UnityEngine.UI;
using Platformer2D.Character;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CharacterMovement2D))]
[RequireComponent(typeof(PlayerInput))]

public class Player : MonoBehaviourPun, IPunObservable
{

    [Header("PhotonView")]
    public PhotonView pv;

    [Header("Camera")]
    public GameObject playerCamera;
    private GameObject sceneCamera;

    [Header("NickNaneText")]
    public Text nameText;

    PlayerInput playerInput;
    CharacterMovement2D playerMovement;

    //-----------------------
    [Header("Rigidbody2D")]

    private Vector3 smoothMove;

    SpriteRenderer spriteRenderer;
    private DeathOnDamage playerOnDamage;
    //-------------------------------- 

    [SerializeField] GameObject weaponObject;
    public IWeapon Weapon { get; private set; }

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<CharacterMovement2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerOnDamage = GetComponent<DeathOnDamage>();

        if (weaponObject != null)
        {
            Weapon = weaponObject.GetComponent<IWeapon>();
        }
    }
    void Start()
    {
        if (photonView.IsMine)
        {
            nameText.text = PhotonNetwork.NickName;
            sceneCamera = GameObject.Find("Main Camera");
            sceneCamera.SetActive(false);
            playerCamera.SetActive(true);
        }
        else
        {
            nameText.text = pv.Owner.NickName;
        }
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            PerformMovement();
        }
        else
        {
            smoothMovements();
        }
    }

    private void smoothMovements()
    {
        transform.position = Vector3.Lerp(transform.position, smoothMove, Time.deltaTime * 10);
    }

    //SerializeView
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(playerMovement.IsCrouching);
            stream.SendNext(playerMovement.CurrentVelocity.x);
            stream.SendNext(playerMovement.CurrentVelocity.y);
            stream.SendNext(playerMovement.IsGrounded);
            stream.SendNext(Weapon.IsAttacking);
            stream.SendNext(spriteRenderer.flipX);
            stream.SendNext(playerOnDamage.OnDamage);
        }
        else if (stream.IsReading)
        {
            smoothMove = (Vector3)stream.ReceiveNext();
            bool isCrouching = (bool)stream.ReceiveNext();
            float horizontalSpeed = (float)stream.ReceiveNext();
            float verticalSpeed = (float)stream.ReceiveNext();
            bool isGrounded = (bool)stream.ReceiveNext();
            bool isAttacking = (bool)stream.ReceiveNext();
            bool flipX = (bool)stream.ReceiveNext();
            bool onDamage = (bool)stream.ReceiveNext();

            GetComponent<PlayerAnimationController>().UpdateAnimationStates(isCrouching, horizontalSpeed, verticalSpeed, isGrounded, isAttacking, flipX, onDamage);
        }
    }

    //Movimentos
    //-------------------------------------------------------------------------------
    public void PerformMovement()
    {
        //movimentacao
        Vector2 movementInput = playerInput.GetMovementInput();
        playerMovement.ProcessMovementInput(movementInput);

        if (movementInput.x > 0)
        {
            spriteRenderer.flipX = false;
            if (weaponObject != null)
            {
                // Ajuste a posição da arma para o lado direito
                weaponObject.transform.localPosition = new Vector3(0, weaponObject.transform.localPosition.y, weaponObject.transform.localPosition.z);
            }

        }
        else if (movementInput.x < 0)
        {
            spriteRenderer.flipX = true;
            if (weaponObject != null)
            {
                // Ajuste a posição da arma para o lado esquerdo
                weaponObject.transform.localPosition = new Vector3(-0.7f, weaponObject.transform.localPosition.y, weaponObject.transform.localPosition.z);
            }
        }

        //Pulo
        if (playerInput.IsJumpButtonDown())
        {
            playerMovement.Jump();
        }
        if (playerInput.IsJumpButtonHeld() == false)
        {
            playerMovement.UpdateJumpAbort();
        }

        //Agachar
        if (playerInput.IsCrouchButtonDown())
        {
            playerMovement.Crouch();
        }
        if (playerInput.IsCrouchButtonUp())
        {
            playerMovement.UnCrouch();
        }

        //ataque
        if (Weapon != null && playerInput.IsAttackButtonDown())
        {
            Weapon.Attack();

        }
    }
}
