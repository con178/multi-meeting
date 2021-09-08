using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class NickNameDisplay : MonoBehaviourPun
{
    private TextMeshProUGUI nickName;
    [SerializeField] PhotonView pV;

    void Start()
    {
        nickName = GetComponent<TextMeshProUGUI>();

        if(photonView.IsMine)
        {
            nickName.text = PhotonNetwork.NickName;
        }
        else
        {
            nickName.text = pV.Owner.NickName;
        }

        
    }

    void Update()
    {
        //transform.localRotation = Quaternion.Euler(0f, -180f, 0f);
    }
}
