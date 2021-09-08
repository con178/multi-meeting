using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviourPun
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private MouseLook mouseLook;


    [SerializeField] private GameObject pauseMain;
    [SerializeField] private GameObject pauseOptions;
    [SerializeField] private GameObject pauseWindow;

    public bool isMenuShown = false;
    public bool CanShowMenu = true;




    public void ShowMenu()
    {
        Debug.Log("Trying to show menu");
        if (CanShowMenu)
        {
            Debug.Log("Showing menu...");
            pauseWindow.gameObject.SetActive(true);
            pauseOptions.SetActive(false);
            pauseMain.SetActive(true);
            playerMovement.CanMove = false;
            playerMovement.chatManager.gameObject.SetActive(false);
            playerMovement.leftDownScreenText.gameObject.SetActive(false);
            playerMovement.nextToChatText.gameObject.SetActive(false);
            mouseLook.CanLook = false;
            isMenuShown = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void ResumeButton()
    {
        pauseMain.SetActive(false);
        pauseOptions.SetActive(false);
        pauseWindow.gameObject.SetActive(false);
        if(!playerMovement.isSitting)
        {
            playerMovement.CanMove = true;
        }
        playerMovement.chatManager.gameObject.SetActive(true);
        playerMovement.leftDownScreenText.gameObject.SetActive(true);
        playerMovement.nextToChatText.gameObject.SetActive(true);
        mouseLook.CanLook = true;
        isMenuShown = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ShowOptions()
    {
        pauseOptions.SetActive(true);
        pauseMain.SetActive(false);
    }

    public void HideOptions()
    {
        pauseMain.SetActive(true);
        pauseOptions.SetActive(false);
    }

    public void Quit()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("Lobby");
    }
}
