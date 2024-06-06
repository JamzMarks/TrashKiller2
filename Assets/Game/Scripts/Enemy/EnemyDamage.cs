using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour//, IDamageable
{
    [SerializeField]
    [Min(0)]
    private int damage = 1; // Quantidade de dano a ser aplicado

    private AudioManager audioManager;

    private void Start()
    {
    audioManager = FindObjectOfType<AudioManager>();
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
            if (collision.CompareTag("Player"))
            {
                IDamageable damageable = collision.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    audioManager.PlayerDamageSound();


                    // Chama a função TakeDamage passando somente o dano como parâmetro
                    damageable.TakeDamage(damage);
                }
            }
    }
}
