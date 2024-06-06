using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;



public class NextLevelController : MonoBehaviourPunCallbacks
{

    [SerializeField] private string fase = "";
    [SerializeField] private string playerTag = "";
    private int playersInArea = 0;



    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            playersInArea++;
            Debug.Log("Esperando jogadores...");

            if (playersInArea >= 2 && PhotonNetwork.IsMasterClient)
            {
                LoadScene();
                //photonView.RPC("LoadScene", RpcTarget.All);
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        { 
            playersInArea--;
            Debug.Log("Jogador saiu");
        }
    }
    #region Private Methods

    void LoadScene()
    {
        Debug.LogFormat("PhotonNetwork : Carregando Fase");
        PhotonNetwork.LoadLevel(fase);
    }
    #endregion
}
