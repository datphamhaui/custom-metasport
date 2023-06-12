using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NFT_trigger : MonoBehaviour
{
    public GameObject window;

    private void OnMouseDown()
    {
        window.SetActive(true);
    }
}
