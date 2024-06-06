using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    [SerializeField]
    [Min(0)]
    private int damage = 1; // Quantidade de dano a ser aplicado




    
    private void OnTriggerEnter2D(Collider2D collision)
    {
            if (collision.CompareTag("Enemy") || collision.CompareTag("Boss"))
            {
                IDamageable damageable = collision.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    

                    // Chama a função TakeDamage passando somente o dano como parâmetro
                    damageable.TakeDamage(damage);

                    Debug.Log("Colidiu com algo");
                }
            }
    }
}
