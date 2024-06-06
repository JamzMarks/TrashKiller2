using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : PlayerDamage, IWeapon
{
    [SerializeField]
    private float attackTime = 0.2f;

    public bool IsAttacking {get; private set;}

    private AudioManager audioManager;


    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();

        gameObject.SetActive(false);
        IsAttacking = false;
    }
    public void Attack()
    {
        if (!IsAttacking)
        {
            audioManager.PlayerAttackSound();
            gameObject.SetActive(true);
            IsAttacking = true;
            StartCoroutine(PerformAttack());
        }

    }

    private IEnumerator PerformAttack()
    {
        yield return new WaitForSeconds(attackTime);
        gameObject.SetActive(false);
        IsAttacking = false;
    }

}