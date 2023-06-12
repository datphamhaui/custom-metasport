
using UnityEngine;
using Photon.Voice.Unity;
using Photon.Pun;
using TMPro;


public class push2talk : MonoBehaviourPun
{
    Recorder rec;
    [SerializeField] GameObject icon;
    [SerializeField] GameObject text;
    void Start()
    {
        rec = GameObject.FindGameObjectWithTag("photonmanager").GetComponent<Recorder>();
        rec.TransmitEnabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (NonUIInput.GetKeyDown(KeyCode.T))
        {
            rec.TransmitEnabled = true;
            photonView.RPC("Talking", RpcTarget.All, PhotonNetwork.LocalPlayer.NickName, true);

        }
        if (NonUIInput.GetKeyUp(KeyCode.T))
        {
            rec.TransmitEnabled = false;
            photonView.RPC("Talking", RpcTarget.All, PhotonNetwork.LocalPlayer.NickName, false);
        }
    }

    [PunRPC]
    void Talking(string playerName, bool state)
    {
        if (text.GetComponent<TextMeshProUGUI>().text == playerName)
        { 
            icon.SetActive(state);
            Debug.Log("Sent to " + playerName);
        }
        else
        {
            Debug.Log("didn't send");
        }
    }

}
