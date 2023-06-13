using UnityEngine;
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
        //if (Object.HasInputAuthority)
        //{
        //    if (NonUIInput.GetKeyDown(KeyCode.I))
        //    {
        //        RPC_Emote();            

        //    }

        //}
        
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    void RPC_Emote()
    {
        anim.SetTrigger("Emoting");
    }
}
