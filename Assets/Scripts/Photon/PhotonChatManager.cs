using ExitGames.Client.Photon;
using Fusion.Photon.Realtime;
using Photon.Chat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using TMPro;

public class PhotonChatManager : MonoBehaviour, IChatClientListener
{
    private ChatClient chatClient;
    private string appIdChat = "47ac65ba-8acb-4cea-9703-455874b4d953";
    private string appVersion = "0.1";
    private string userID;
    public TMP_InputField inputMessage;
    public TextMeshProUGUI chatHistory;


    // Start is called before the first frame update
    void Start()
    {
        userID = "User_" + Random.Range(0, 1000);
        chatClient = new ChatClient(this);
        chatClient.ChatRegion = "ASIA";
        chatClient.Connect(appIdChat, appVersion, new Photon.Chat.AuthenticationValues(userID));

    }

    // Update is called once per frame
    void Update()
    {
        chatClient.Service();
        if (Input.GetKeyUp(KeyCode.Return))
        {
            Debug.Log(inputMessage.text);
            chatClient.PublishMessage("channelA", inputMessage.text);
            inputMessage.text = "";
        }
    }

    public void OnChangeMessageInput(string value)
    {
        Debug.Log(value);
    }

    #region Callback IChatClientListener
    public void DebugReturn(DebugLevel level, string message)
    {
        Debug.Log("DebugReturn");
    }

    public void OnChatStateChange(ChatState state)
    {
        Debug.Log("OnChatStateChange");
    }

    public void OnConnected()
    {
        Debug.Log("OnConnected");
        chatClient.Subscribe(new string[] { "channelA", "channelB" });
    }

    public void OnDisconnected()
    {
        Debug.Log("OnDisconnected");
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        Debug.Log("OnGetMessages");
        Debug.Log("channelName " + channelName);
        foreach (string sender in senders)
        {
            Debug.Log("sender " + sender);

        }
        foreach (string message in messages)
        {
            Debug.Log("message " + message);
            chatHistory.text += string.Format("<b>{0} :</b> {1}\n\n", senders[0], message);

        }
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        Debug.Log("OnPrivateMessage");
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        Debug.Log("OnStatusUpdate");
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        Debug.Log("OnSubscribed");
    }

    public void OnUnsubscribed(string[] channels)
    {
        Debug.Log("OnUnsubscribed");
    }

    public void OnUserSubscribed(string channel, string user)
    {
        Debug.Log("OnUserSubscribed");
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        Debug.Log("OnUserUnsubscribed");
    }

    #endregion
}
