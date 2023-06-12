using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

namespace Photon.Pun.Demo.PunBasics
{
    public class ConnectedPlayers : MonoBehaviourPunCallbacks
    {
        //for each player, create a box in the connected players panel
        //listen for players connection and disconnection and update list.
        //TODO: add private chat button, make display on click
        public GameObject playercounter;
        public GameObject dropdownobj;
        ChatBox chatbox;
        TMP_Dropdown playerlist;
        TextMeshProUGUI tmpcounter;
        private void Start() {
            chatbox = GetComponent<ChatBox>();
            tmpcounter = playercounter.GetComponent<TextMeshProUGUI>();
            playerlist = dropdownobj.GetComponent<TMP_Dropdown>();
            playerlist.ClearOptions();           
        }
    
        public void displayPlayers() {
            foreach (Player player in PhotonNetwork.PlayerList) 
            { 
                playerlist.options.Add(new TMP_Dropdown.OptionData(player.NickName));
            }
            chatbox.LogServer("Welcome to MetaSport ^_^");
            updateplayercount(Mathf.Max(1, PhotonNetwork.CurrentRoom.PlayerCount));
            
        }

        void updateplayercount(int count)
        {
            tmpcounter.text = "Players online: " + count.ToString();
        }
   
       
        public override void OnPlayerEnteredRoom( Player other  )
		    {
                addPlayer(other.NickName, PhotonNetwork.CurrentRoom.PlayerCount);
            chatbox.LogServer(other.NickName + " joined the server");
        }
        public override void OnPlayerLeftRoom( Player other  )
		    {
                removePlayer(other.NickName, PhotonNetwork.CurrentRoom.PlayerCount);
            chatbox.LogServer(other.NickName + " left the server");
        }

        void addPlayer(string name, int newcount)
        {
            playerlist.options.Add(new TMP_Dropdown.OptionData(name));
            updateplayercount(newcount);

        }
        void removePlayer(string name, int newcount)
        {
            TMP_Dropdown.OptionData toremove =null;
            foreach (TMP_Dropdown.OptionData option in playerlist.options)
            {
                if (option.text == name)
                {
                    toremove = option;
                    break;
                }
            }
            playerlist.options.Remove(toremove);
            updateplayercount(newcount);

        }
    }

}