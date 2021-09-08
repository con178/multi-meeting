using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    private GameManager gameManager;
    [SerializeField] private GameObject confirmationWindow;
    [SerializeField] private GameObject[] gameObjectsConfirmWindow;

    void Start()
    {
        gameManager = (GameManager)FindObjectOfType(typeof(GameManager));
        gameManager.Save();
        gameManager.Load();
        gameManager.LoadOptionsSettings();
    }
    public void StartButton()
    {
        SceneManager.LoadScene("Lobby");
    }
    public void CustomizationButton()
    {
        SceneManager.LoadScene("Customization");
    }
    public void OptionsButton()
    {
        SceneManager.LoadScene("Options");
    }
    public void OpenConfirmationWindow()
    {
        confirmationWindow.SetActive(true);
        foreach(GameObject go in gameObjectsConfirmWindow)
        {
            go.SetActive(false);
        }
    }
    public void ConfirmationWindow_NO()
    {
        confirmationWindow.SetActive(false);
        foreach (GameObject go in gameObjectsConfirmWindow)
        {
            go.SetActive(true);
        }
    }
    public void ConfirmationWindow_YES()
    {
        gameManager.Save();
        Application.Quit();
    }
    public void Disconnect()
    {
        SceneManager.LoadScene("LoginScreen");
    }
}
