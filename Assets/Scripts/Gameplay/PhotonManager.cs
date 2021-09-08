using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PhotonManager : MonoBehaviour
{

    [SerializeField] private GameObject[] SpawnPoints;
    [SerializeField] private GameManager gameManager;

    void Start()
    {
        gameManager = (GameManager)FindObjectOfType(typeof(GameManager));
        if (PhotonNetwork.IsConnected)
        {
            SpawnPlayer();
        }
    }



    [PunRPC]
    void SpawnPlayer()
    {
        if(gameManager.genderID == 0)
        {
            GameObject Player = PhotonNetwork.Instantiate("Female", SpawnPoints[Random.Range(0, 2)].transform.position, new Quaternion(0f, 0f, 0f, 0f));
            Player.transform.localScale = new Vector3(5f, 5f, 5f);
        }
        else if(gameManager.genderID == 1)
        {
            GameObject Player = PhotonNetwork.Instantiate("Male", SpawnPoints[Random.Range(0, 2)].transform.position, new Quaternion(0f, 0f, 0f, 0f));
            Player.transform.localScale = new Vector3(5f, 5f, 5f);
        }
    }
}
