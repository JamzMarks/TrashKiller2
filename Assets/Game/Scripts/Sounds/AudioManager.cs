using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource; // Referência ao AudioSource
    public AudioClip clickSound; // Referência ao som de clique
    public AudioClip damageSound; // Referência ao som de dano
    public AudioClip attackSound; // Referência ao som de ataque

    private void Awake()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    public void PlayClickSound()
    {
        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }

    public void PlayerDamageSound()
    {
        if (audioSource != null && damageSound != null)
        {
            audioSource.PlayOneShot(damageSound);
        }
    }

        public void PlayerAttackSound()
    {
        if (audioSource != null && attackSound != null)
        {
            audioSource.PlayOneShot(attackSound);
        }
    }

}
