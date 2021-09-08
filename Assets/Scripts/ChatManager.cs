using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Chat;
using Photon.Pun;
using ExitGames.Client.Photon;
using UnityEngine.UI;
using TMPro;

public class ChatManager : MonoBehaviourPun, IChatClientListener
{
    public ChatClient chatClient;
    public TMP_InputField input;
    public TextMeshProUGUI chatMessages;
    [SerializeField] private string roomName;
    public GameObject chatActiveColor;

    void Start()
    {
        Application.runInBackground = true;
        roomName = PhotonNetwork.CurrentRoom.Name;
        this.chatClient = new ChatClient(this);
        ConnectToChat();
        chatActiveColor.SetActive(false);
    }
    void Update()
    {
        if(this.chatClient != null)
            this.chatClient.Service();
    }
    void ConnectToChat()
    {
        this.chatClient.AuthValues = new AuthenticationValues(PhotonNetwork.NickName);
        ChatAppSettings chatSettings = PhotonNetwork.PhotonServerSettings.AppSettings.GetChatSettings();
        this.chatClient.ChatRegion = "EU";
        this.chatClient.ConnectUsingSettings(chatSettings);
    }

    public void SendMessage()
    {
        this.chatClient.PublishMessage(roomName, input.text);
    }


    public void DebugReturn(DebugLevel level, string message)
    {
        Debug.LogError(level + " " + message);
    }

    public void OnChatStateChange(ChatState state)
    {
        
    }

    public void OnConnected()
    {
        this.chatClient.Subscribe(new string[] { roomName });
        this.chatClient.SetOnlineStatus(ChatUserStatus.Online);
        chatMessages.text += "\n" + PhotonNetwork.NickName + " connected to chat!" + "\n";
    }

    public void OnDisconnected()
    { 
        chatMessages.text += "\n" + PhotonNetwork.NickName + " disconnected from chat!" + "\n";
        ConnectToChat();
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        for (int i = 0; i < senders.Length; i++)
        {
            chatMessages.text += senders[i] + ": " + messages[i];
        }
    }


    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {

    }

    public void OnUnsubscribed(string[] channels)
    {
        
    }

    public void OnUserSubscribed(string channel, string user)
    {
        
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        
    }
}
