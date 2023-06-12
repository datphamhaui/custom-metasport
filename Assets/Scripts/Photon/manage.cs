using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using Photon.Pun.UtilityScripts;
using UnityEngine.SceneManagement;

public class manage : MonoBehaviourPunCallbacks
{
    public Vector3 pos;
    public PhotonView playerPrefab;
    public Vector3 min = new Vector3(-5, 0, 0);
    public Vector3 max = new Vector3(5, 0, 7);
    public GameObject thePlayer;
    public GameObject chat;

    //moez: "511ce637-76d1-4b19-a1d0-b27151453454"
    //moez voice: "6a4cdbdf-315f-4702-9a31-fda9eda2ca43"

    //private string id_sync = "edf6e576-11c3-4581-9f40-2db714d8093e";//for scene 2 use : eabb3d0c-d85f-459a-a185-77e3cc91b45e
    //private string id_voice = "0479f8ae-df4b-4f5a-930a-0adfbef7e7b8";//for voice chat use :6a4cdbdf-315f-4702-9a31-fda9eda2ca43
    private string id_sync = "511ce637-76d1-4b19-a1d0-b27151453454";
    private string id_voice = "6a4cdbdf-315f-4702-9a31-fda9eda2ca43";
    // Start is called before the first frame update


    private void Awake()
    {
       // PhotonNetwork.AutomaticallySyncScene = true;
    }
    void Start()
    {
        PhotonNetwork.PhotonServerSettings.AppSettings.AppIdRealtime = id_sync;
        PhotonNetwork.PhotonServerSettings.AppSettings.AppIdVoice = id_voice;
        Debug.Log(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdRealtime);
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master 1");
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("joined lobby");
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 2;
        PhotonNetwork.JoinLobby();
        PhotonNetwork.JoinOrCreateRoom("Kayak game", options, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.CurrentRoom+"aaaaaaaa");
        Debug.Log("Joined room kayak successfully!");
        pos = new Vector3(UnityEngine.Random.Range(-5, 5), 0.1f, UnityEngine.Random.Range(0, 7));
        //Debug.Log(pos);
        //pos = new Vector3(38, 0, 16);
        thePlayer = PhotonNetwork.Instantiate(PlayerPrefs.GetString("PrefabName"), pos, Quaternion.identity);


        if (PhotonNetwork.LocalPlayer.NickName == "")
        {
            int p = UnityEngine.Random.Range(0, 10);
            string Username = "TesingBot" + p.ToString();
            PhotonNetwork.LocalPlayer.NickName = Username;
        }

        thePlayer.name = PhotonNetwork.LocalPlayer.NickName;

        /*foreach (Transform child in thePlayer.transform.GetChild(0).GetChild(0).transform)
        {
            child.gameObject.layer = 7;
        }*/



        //chat.GetComponent<Photon.Pun.Demo.PunBasics.ConnectedPlayers>().displayPlayers();

    }

    // Update is called once per frame
    void Update()
    {
        /*if (NonUIInput.GetKeyDown(KeyCode.Tab))
        {
            StartCoroutine(DisconnectAndLoad());
        }*/
    }
  
    /*void OnApplicationQuit()
    {
        PhotonNetwork.Disconnect();
    }*/

}

