using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class UIHandler : MonoBehaviourPunCallbacks
{
    public InputField createRoomTF;
    public InputField joinRoomTF;

    private AudioManager audioManager;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void OnClick_JoinRoom()
    {
        audioManager.PlayClickSound();
        PhotonNetwork.JoinRoom(joinRoomTF.text, null);
    }

    public void OnClick_CreateRoom()
    {
        audioManager.PlayClickSound();
        PhotonNetwork.CreateRoom(createRoomTF.text, new RoomOptions { MaxPlayers = 2 }, null);
    }

    public override void OnJoinedRoom()
    {
        print("Entrou na sala");
        PhotonNetwork.LoadLevel(1);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        print("Falha ao entrar " + returnCode + "Message " + message);
    }
}
