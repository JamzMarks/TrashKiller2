using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class FinishGame : MonoBehaviourPunCallbacks

{
[SerializeField] private string victoryScene = "Victory";
[SerializeField] private string menuScene = "Menu";


    public void VictoryScene()
    {
        Debug.LogFormat("PhotonNetwork : Carregando Tela final");
        PhotonNetwork.LoadLevel(victoryScene);
    }




    void MenuScene()
    {
        PhotonNetwork.LoadLevel(menuScene);
        

    }

    public void OnClick_MenuBtn()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        MenuScene();
        
        
    }
    


}
