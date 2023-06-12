using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Fusion;

public class EmoteSystem : NetworkBehaviour
{
    Animator anim;
    void Start()
    {
        anim=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Object.HasStateAuthority)
        {
            if (NonUIInput.GetKeyDown(KeyCode.I))
            {
                RPC_Emote();            

            }

        }
        
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    void RPC_Emote()
    {
        anim.SetTrigger("Emoting");
    }
}
