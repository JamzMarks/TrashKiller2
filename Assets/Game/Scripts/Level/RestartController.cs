using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;


public class RestartController : MonoBehaviourPunCallbacks
{

    private AudioManager audioManager;

    private void Start()
    {
    audioManager = FindObjectOfType<AudioManager>();
    }

    

    void RestartScene()
    {

        AtivaRestart ativaRestart = new AtivaRestart();
        string nomeDaCenaAtual = ativaRestart.nomeDaCenaAtual();
        PhotonNetwork.LoadLevel(nomeDaCenaAtual);

    }

    public void OnClick_RestartBtn()
    {
            audioManager.PlayClickSound();
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        RestartScene();
    }
    

}









