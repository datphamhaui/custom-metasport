using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using Photon.Pun;

public class nickname_loader: MonoBehaviour
{
    string Username;

    // Start is called before the first frame update
    void Awake()
    {

        GetAccountInfoRequest request = new GetAccountInfoRequest();
        PlayFabClientAPI.GetAccountInfo(request, Successs, fail);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void Successs(GetAccountInfoResult result)
    {
        Username = result.AccountInfo.Username;
        //PhotonNetwork.LocalPlayer.NickName = Username;
    }
    void fail(PlayFabError error)
    { 
    }

}
