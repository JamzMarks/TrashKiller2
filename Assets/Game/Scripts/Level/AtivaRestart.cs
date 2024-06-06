using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class AtivaRestart : MonoBehaviour
{
    [SerializeField] private string restartScene = "Restart";

    public static string faseAtual; // Variável estática para armazenar o nome da fase atual

    public string nomeDaCenaAtual()
    {
        Debug.Log("O nome da cena atual é: " + faseAtual);
        return faseAtual;
    }

    public void RestartScene()
    {
        Debug.LogFormat("PhotonNetwork : Carregando Tela de Restart. Última fase: {0}", faseAtual);
        PhotonNetwork.LoadLevel(restartScene);
    }
}
