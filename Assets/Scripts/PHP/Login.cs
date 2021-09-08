using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField username;
    [SerializeField] private TMP_InputField password;
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private TextMeshProUGUI loginText;
    [SerializeField] private TextMeshProUGUI connectingText;
    [SerializeField] private GameObject[] gameObjectsToHide;


    void Start()
    {
        if(!PhotonNetwork.IsConnected)
        {
            connectingText.text = "Connecting to server...";
            PhotonNetwork.ConnectUsingSettings();
            foreach (GameObject go in gameObjectsToHide)
            {
                go.SetActive(false);
            }
        }
        else
        {
            foreach (GameObject go in gameObjectsToHide)
            {
                go.SetActive(true);
            }
        }
    }


    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }


    public override void OnJoinedLobby()
    {
        connectingText.text = "";
        Debug.Log("Connected to " + PhotonNetwork.CloudRegion);

        foreach (GameObject go in gameObjectsToHide)
        {
            go.SetActive(true);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            CallLogin();
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (username.isFocused)
                password.ActivateInputField();
            else if (password.isFocused)
                username.ActivateInputField();
            else
                username.ActivateInputField();
        }
            
    }



    public void CallLogin()
    {
        if(username.text.Length > 0 && password.text.Length > 0)
        {
            foreach (GameObject go in gameObjectsToHide)
            {
                go.SetActive(false);
            }

            if (infoText.gameObject.activeSelf)
                infoText.gameObject.SetActive(false);

            loginText.gameObject.SetActive(true);
            loginText.text = "Logging in...";
            StartCoroutine(LoginPlayer());
        }
        else
        {
            infoText.gameObject.SetActive(true);
            infoText.text = "Type username and password!";
        }
        
    }

    IEnumerator LoginPlayer()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username.text);
        form.AddField("password", password.text);
        WWW www = new WWW("https://multimeeting.000webhostapp.com/login.php", form);
        yield return www;

        if(www.text[0] == '0')
        {
            DBManager.username = username.text;
            PhotonNetwork.NickName = DBManager.username;
            SceneManager.LoadScene("Menu");
            Debug.Log(DBManager.username + " logged");
        }
        else
        {
            foreach (GameObject go in gameObjectsToHide)
            {
                go.SetActive(true);
            }
            loginText.gameObject.SetActive(false);
            infoText.gameObject.SetActive(true);
            infoText.text = "User login failed - Error#" + www.text;
            Debug.Log("User login failed");
        }
    }


    public void RegisterButton()
    {
        Application.OpenURL("https://multimeeting.000webhostapp.com");
    }
}
