using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerName : MonoBehaviour
{
    public InputField nametf;
    public Button setNameBtn;
    private AudioManager audioManager;

    private void Start()
    {
        // Obter a referência ao AudioManager na cena
        audioManager = FindObjectOfType<AudioManager>();

    }

    public void OnTFChange()
    {
        if (nametf.text.Length > 2)
        {
            setNameBtn.interactable = true;
        }
        else
        {
            setNameBtn.interactable = false;
        }
    }

    public void OnClick_SetName()
    {
        // Tocar o som de clique
        audioManager.PlayClickSound();

        PhotonNetwork.NickName = nametf.text;
    }
}
