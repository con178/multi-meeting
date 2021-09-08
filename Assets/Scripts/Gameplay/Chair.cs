using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Chair : MonoBehaviourPun
{
    private CapsuleCollider triggerCollider;
    private Vector3 prevPos;
    private bool isLocked = false;


    void Start()
    {
        triggerCollider = GetComponent<CapsuleCollider>();
    }

    private void OnTriggerStay(Collider other)
    {
        PlayerMovement playerMovement = other.gameObject.GetComponent<PlayerMovement>();
        Debug.Log("Trigger chair");
        if(!playerMovement.isSitting)
        {
            playerMovement.leftDownScreenText.text = "Press [E] to sit down";
        }
        if (other.CompareTag("Player") && Input.GetKey(KeyCode.E) && !other.gameObject.GetComponent<PlayerMovement>().isSitting && !isLocked && !playerMovement.chatManager.input.isFocused)
        {
            isLocked = true;
            prevPos = other.gameObject.transform.localPosition;
            Debug.Log("PREVPOS: " + prevPos);
            playerMovement.ChangeSit();
            playerMovement.leftDownScreenText.text = "Press [Left Shift] to stand up";
            playerMovement.CanMove = false;
            playerMovement.playerCamera.transform.localPosition = new Vector3(playerMovement.playerCamera.transform.localPosition.x, playerMovement.playerCamera.transform.localPosition.y - 0.3f, playerMovement.playerCamera.transform.localPosition.z);
            GetComponent<BoxCollider>().enabled = false;
            other.gameObject.transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + 1f, transform.localPosition.z);
        }
        if(Input.GetKey(KeyCode.LeftShift) && other.gameObject.GetComponent<PlayerMovement>().isSitting && isLocked)
        {
            playerMovement.ChangeSit();
            playerMovement.CanMove = true;
            playerMovement.leftDownScreenText.text = "";
            playerMovement.playerCamera.transform.localPosition = new Vector3(playerMovement.playerCamera.transform.localPosition.x, playerMovement.playerCamera.transform.localPosition.y + 0.3f, playerMovement.playerCamera.transform.localPosition.z);
            other.gameObject.transform.localPosition = prevPos;
            GetComponent<BoxCollider>().enabled = true;
            isLocked = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerMovement playerMovement = other.gameObject.GetComponent<PlayerMovement>();
        playerMovement.leftDownScreenText.text = "";
    }
}
