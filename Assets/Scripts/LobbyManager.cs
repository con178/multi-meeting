using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject[] GOToLoadAfterConnect;
    [SerializeField] private TMP_InputField PINinput;
    [SerializeField] private TextMeshProUGUI LoadingRoomText;
    [SerializeField] private TextMeshProUGUI connectingText;

    void Start()
    {
        foreach(GameObject go in GOToLoadAfterConnect)
        {
            go.SetActive(true);
        }
    }



    public void CreateRoom()
    {
        int randomPIN = Random.Range(1000, 10000);
        PhotonNetwork.CreateRoom(randomPIN.ToString(), new RoomOptions
        {
            MaxPlayers = 15,
            IsVisible = false,
            IsOpen = true
        });
    }

    public override void OnCreatedRoom()
    {
        foreach (GameObject go in GOToLoadAfterConnect)
        {
            go.SetActive(false);
        }
        LoadingRoomText.text = "";
        connectingText.text = "Loading room...";
        PhotonNetwork.LoadLevel("Gameplay");
    }







    public void JoinRoom()
    {
        if (PINinput.text.Length == 4)
        {
            //PhotonNetwork.NickName = nickNameInput.text;
            PhotonNetwork.JoinRoom(PINinput.text);
            LoadingRoomText.text = "Looking for a room...";
        }
        else
        {
            LoadingRoomText.text = "Invalid PIN!";
        }
    }

    public override void OnJoinedRoom()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            foreach (GameObject go in GOToLoadAfterConnect)
            {
                go.SetActive(false);
            }
            LoadingRoomText.text = "";
            connectingText.text = "Loading room...";
            PhotonNetwork.LoadLevel("Gameplay");
        }
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        LoadingRoomText.text = message + " (code: " + returnCode + ")";
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        LoadingRoomText.text = message + " (code: " + returnCode + ")";
    }


    public void GoBack()
    {
        SceneManager.LoadScene("Menu");
    }

    
}
