using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer2D.Character;


[RequireComponent(typeof(CharacterMovement2D))]
[RequireComponent(typeof(CharacterFacing2D))]



public class EnemyAiController : MonoBehaviour
{

    CharacterMovement2D enemyMovement;
    CharacterFacing2D enemyFacing;
    public Vector2 movementInput;

    void Start()
    {
        enemyMovement = GetComponent<CharacterMovement2D>();
        enemyFacing = GetComponent<CharacterFacing2D>();
        

    }

    void Update()
    {
        enemyMovement.ProcessMovementInput(movementInput);
        enemyFacing.UpdateFacing(movementInput);
        
    }


   

}
