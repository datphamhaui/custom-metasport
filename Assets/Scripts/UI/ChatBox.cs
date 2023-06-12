using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System;

    public class ChatBox : MonoBehaviourPun
    {
        public GameObject chatLogText;
        public GameObject chatInput;
        public GameObject chatPanel;
        TextMeshProUGUI tmplog;
        TMP_InputField tmpinput;
        void Start()
        {
            tmplog = chatLogText.GetComponent<TextMeshProUGUI>();
            tmpinput = chatInput.GetComponent<TMP_InputField>();
        }
        void Update() {
            if (EventSystem.current.currentSelectedGameObject!=null && EventSystem.current.currentSelectedGameObject.name == "Message input")
            {
                if (Input.GetKeyUp(KeyCode.Return))
                    {
                        OnChatInputSend();
                        tmpinput.ActivateInputField();
                    }
                if (Input.GetKeyUp(KeyCode.LeftControl))
                {
                    EventSystem.current.SetSelectedGameObject(null);
                }
            }
            
            
        }

        public void panelhider()
        {
            chatPanel.SetActive(!chatPanel.activeInHierarchy);
        }

        public void fillprivate(TextMeshProUGUI reciever)
        {
            tmpinput.text = "/w " + reciever.text+" ";
        }


        // called when sending msg
        public void OnChatInputSend() {
            if (chatInput != null) {
                if (tmpinput.text.Length > 0) {
                    if (tmpinput.text.StartsWith("/w"))
                    {
                        
                        List<string> words = new List<string>(tmpinput.text.Split(' '));
                        if (words.Count < 3)
                        {
                            return;
                        }
                        string reciever = words[1];
                        words.RemoveRange(0, 2);
                        photonView.RPC("LogPrivate", RpcTarget.All, PhotonNetwork.LocalPlayer.NickName,reciever, String.Join(" ", words.ToArray()));
                        tmpinput.text = "";
                    }
                    else
                    {
                        photonView.RPC("Log", RpcTarget.All, PhotonNetwork.LocalPlayer.NickName, tmpinput.text);
                    }
                    
                }
                //EventSystem.current.SetSelectedGameObject(null);
            }
            else {
                Debug.Log("reference to chatInput not found");
            }
        }
        [PunRPC]
        void Log( string playerName, string message) {
            tmplog.text += string.Format("<b>{0} :</b> {1}\n\n", playerName, message);
            tmpinput.text = "";
        }
        public void LogServer(string message)
        {
            tmplog.text += string.Format("<b><color=green>{0}</color></b>\n\n", message);
        }

        [PunRPC]
        void LogPrivate(string sender, string reciever, string message)
        {
            if(PhotonNetwork.LocalPlayer.NickName == reciever)
            {
                tmplog.text += string.Format("<color=orange><b>{0} :</b> {1}</color>\n\n", sender, message);
            }
            if (PhotonNetwork.LocalPlayer.NickName == sender)
            {
                tmplog.text += string.Format("<color=orange><b>Sent to {0} :</b> {1}</color>\n\n", reciever, message);
            }
        }
    }