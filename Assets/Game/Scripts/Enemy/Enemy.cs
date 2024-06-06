using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector]
    public float health;
    [SerializeField] public float maxHealth;
    [SerializeField] float shield;
    [SerializeField] float enemyDamage;

    public enum EnemyDamageType
    {
        AreaDamage,
        TriggerDamage
    }

    public EnemyDamageType enemyType;


    void Start()
    {
        health = maxHealth;

        switch (enemyType)
        {
            case EnemyDamageType.AreaDamage:
                break;
            case EnemyDamageType.TriggerDamage:
                break;
        }
    }

    // Update is called once per frame
    public void takeDamage(float damageAmout)
    {
        float damageReduction = damageAmout * (shield / 100f);
        float finalDamage = damageAmout - damageReduction;

        health -= finalDamage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnValidate()
    {
        if (maxHealth < 0)
        {
            maxHealth = 10; // Corrige maxHealth para 0 se for negativo
        }
        if(enemyDamage <= -1)
        {
            enemyDamage = 0; // Corrige maxHealth para 0 se for negativo
        }
    }

}
