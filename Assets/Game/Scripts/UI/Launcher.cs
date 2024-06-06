using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    public GameObject connectionScreens;
    public GameObject connectedScreen;
    public GameObject disconnectedScreen;

    public GameObject beastScreen;
    public GameObject aboutScreen;

    private AudioManager audioManager;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();

    }
    public void OnClick_ConnectBtn()
    {

        PhotonNetwork.Disconnect();

        audioManager.PlayClickSound();
        PhotonNetwork.ConnectUsingSettings();
    }

    public void OnClick_BeastBtn()
    {
        audioManager.PlayClickSound();
        beastScreen.SetActive(true);
    }

    public void OnClick_AboutBtn()
    {
        audioManager.PlayClickSound();
        aboutScreen.SetActive(true);
    }

    public void OnClick_ReturnBtn()
    {
        audioManager.PlayClickSound();
        aboutScreen.SetActive(false);
        beastScreen.SetActive(false);
        connectionScreens.SetActive(false);
        connectedScreen.SetActive(false);
    }

    public void OnClick_ExitBtn()
    {
        audioManager.PlayClickSound();
        Application.Quit();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        disconnectedScreen.SetActive(true);
        connectionScreens.SetActive(true);
    }

    public override void OnJoinedLobby()
    {
        if (disconnectedScreen.activeSelf)
            disconnectedScreen.SetActive(false);

        connectionScreens.SetActive(true);
        connectedScreen.SetActive(true);
    }
}
