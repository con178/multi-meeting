using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MouseLook : MonoBehaviourPun
{
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private Transform playerMain;

    public bool CanLook = true;
    private float xRotation = 0f;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        if(!playerMain.transform.GetComponent<PhotonView>().IsMine)
        {
            GetComponent<AudioListener>().enabled = playerMain.transform.GetComponent<PhotonView>().IsMine;
        }
    }
    void Update()
    {
        if (!playerMain.transform.GetComponent<PhotonView>().IsMine) return;

        if(CanLook)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime * 80f;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime * 80f;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -60f, 60f);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerMain.Rotate(Vector3.up * mouseX);
        }
    }
}
