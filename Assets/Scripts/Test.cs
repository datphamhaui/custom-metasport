using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class Test : MonoBehaviour
{
    public Animator anim ;
    public Avatar[] avatars;
    GameObject geometry ;
    public GameObject[] myMesh;
    PhotonView photonView;//delete


    // Start is called before the first frame update
    void Start()
    {
      
        photonView = GetComponent<PhotonView>();

    }

    // Update is called once per frame
    /*
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && photonView.IsMine)
        {
            anim.avatar = avatars[1];
        }
        if (Input.GetKeyDown(KeyCode.N) && photonView.IsMine)
        {
            Object.Destroy(geometry.transform.GetChild(0).gameObject);

            Instantiate(myMesh[1], geometry.transform);

        }
    }
    */
    
      void Update()
    {
     
           if (photonView.IsMine && Input.GetKeyDown(KeyCode.M))
            {
             
                Change();
            }
        
    }
    void Change()
    {
        photonView.RPC("ChangeAvatar", RpcTarget.AllBuffered);
    }

    [PunRPC]
    void ChangeAvatar()
    {
        
        {
             geometry = GameObject.Find("Geometry");
            Object.Destroy(geometry.transform.GetChild(0).gameObject);

            Instantiate(myMesh[1], geometry.transform);

            anim.avatar = avatars[1];
        }
    }
    
}
