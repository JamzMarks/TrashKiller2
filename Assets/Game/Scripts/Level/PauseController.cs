using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class PauseController : MonoBehaviourPunCallbacks

{

[SerializeField] private string menuScene = "Menu";

    public GameObject pauseScreen;
    public GameObject pauseButton;


    void MenuScene()
    {
        PhotonNetwork.LoadLevel(menuScene);  

    }

    public void OnClick_PauseBtn()
    {
        
       pauseScreen.SetActive(true);
       pauseButton.SetActive(false);
    }

    public void OnClick_ReturnBtn()
    {
        
        pauseScreen.SetActive(false);
        pauseButton.SetActive(true);
    }

    public void OnClick_MenuBtn()
    {
        MenuScene();
        
        
    }
    


}
