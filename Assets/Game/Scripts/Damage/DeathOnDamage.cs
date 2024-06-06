using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class DeathOnDamage : MonoBehaviourPunCallbacks, IDamageable, IPunObservable
{
    public bool IsDead { get; private set; }
    public event Action DeathEvent;
    public int MaxHP = 10; // HP do personagem
    private int atualHP;
    public int AtualHP => atualHP;

    public Image healthBar;

    public float knockbackForce = 20f; // Força de afastamento
    private Vector2 knockbackDirection;
    private bool applyKnockback = false;

    // Referência ao script AtivaRestart
    private AtivaRestart ativaRestart;

    // Referência ao script FinishGame
    private FinishGame finishGame;

    public bool OnDamage { get; private set; }

    private void Awake()
    {
        atualHP = MaxHP;
        IsDead = false;
        OnDamage = false;

        ativaRestart = FindObjectOfType<AtivaRestart>();
        if (ativaRestart == null)
        {
            Debug.Log("Script AtivaRestart não encontrado na cena!");
        }

        finishGame = FindObjectOfType<FinishGame>();

        if (healthBar == null)
        {
            Debug.LogError("Referência à barra de vida não definida!");
        }
    }

    private void FixedUpdate()
    {
        if (applyKnockback)
        {
            ApplyKnockbackForce();
            applyKnockback = false;
        }
    }

    public void TakeDamage(int damage)
    {
        photonView.RPC("ApplyDamage", RpcTarget.All, damage);
    }

    public void HealDamage(int heal)
    {
        photonView.RPC("ApplyHeal", RpcTarget.All, heal);
    }

    [PunRPC]
    private void ApplyDamage(int damage)
    {
        if (IsDead)
            return;

        atualHP -= damage;

        if (atualHP <= 0 && !IsDead)
        {
            IsDead = true;
            DeathEvent?.Invoke();
            HandleDeath();
        }
        OnDamage = true;
        UpdateHealthBar(atualHP);
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(ResetOnDamage());
            CalculateKnockbackDirection();
        }
    }

    [PunRPC]
    private void ApplyHeal(int heal)
    {
        if (IsDead)
            return;

        atualHP += heal;

        if (atualHP > MaxHP && !IsDead)
        {
            atualHP = MaxHP;
        }

        UpdateHealthBar(atualHP);
    }

    private IEnumerator ResetOnDamage()
    {
        yield return new WaitForSeconds(0.3f);
        OnDamage = false;
    }

    [PunRPC]
    private void UpdateHealthBar(int newHP)
    {
        atualHP = newHP;

        float fillAmount = (float)atualHP / MaxHP;
        healthBar.fillAmount = fillAmount;

        Debug.Log($"GameObject: {gameObject.name}, AtualHP: {atualHP}");
    }

    private void HandleDeath()
    {
        if (CompareTag("Player"))
        {
            Debug.Log("Player morreu.");
            AtivaRestart.faseAtual = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name; // Armazena o nome da fase atual
            photonView.RPC("RestartScene", RpcTarget.All);
            atualHP = MaxHP;
        }
        else if (CompareTag("Enemy"))
        {
            if (photonView.IsMine) // Verifica se este cliente é o proprietário do objeto
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
        else if (CompareTag("Boss"))
        {
            Debug.Log("o Boss morreu.");
            photonView.RPC("VictoryScene", RpcTarget.All);
        }
    }

    private void CalculateKnockbackDirection()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            knockbackDirection = (transform.position - player.transform.position).normalized;
            applyKnockback = true;
        }
        else
        {
            Debug.LogError("Player not found for knockback calculation.");
        }
    }

    private void ApplyKnockbackForce()
    {
        var movementComponent = GetComponent<Platformer2D.Character.CharacterMovement2D>();
        if (movementComponent != null)
        {
            Debug.Log($"Applying knockback to {gameObject.name} in direction {knockbackDirection}");
            movementComponent.RigidBody.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
        }
        else
        {
            Debug.LogError("CharacterMovement2D component not found on enemy.");
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(atualHP);
            stream.SendNext(knockbackDirection);
            stream.SendNext(applyKnockback);
        }
        else if (stream.IsReading)
        {
            atualHP = (int)stream.ReceiveNext();
            knockbackDirection = (Vector2)stream.ReceiveNext();
            applyKnockback = (bool)stream.ReceiveNext();
            UpdateHealthBar(atualHP);
        }
    }

    [PunRPC]
    void RestartScene()
    {
        ativaRestart.RestartScene();
    }

    [PunRPC]
    void VictoryScene()
    {
        finishGame.VictoryScene();
    }
}
