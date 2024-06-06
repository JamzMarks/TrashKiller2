using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Collects : MonoBehaviourPun
{

    [SerializeField]
    [Min(0)]
    private int heal = 20;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IDamageable damageable = collision.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.HealDamage(heal);
                if (PhotonNetwork.IsMasterClient || photonView.IsMine)
                {
                    PhotonNetwork.Destroy(gameObject);
                }
                else
                {
                    // Solicita a propriedade antes de destruir o objeto
                    photonView.RequestOwnership();
                }
            }
        }
    }
}
