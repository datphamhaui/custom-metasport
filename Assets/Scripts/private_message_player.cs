using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class private_message_player : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI username;

    public void fillinput()
    {
        GameObject.FindGameObjectWithTag("chatmanager").GetComponent<ChatBox>().fillprivate(username);
    }
}
