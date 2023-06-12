using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;
using Photon.Pun;

public class Teleporter_trigger : MonoBehaviourPunCallbacks
{
    public GameObject panel;
    public GameObject prompt;
    public GameObject mouselocksys;
    public string scenename;
    public Transform target;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PhotonView>() != null)
        {
            if (other.GetComponent<PhotonView>().IsMine)
            {
                mouselocksys.GetComponent<Mouselock_controller>().locking(false);
                panel.SetActive(true);
                prompt.GetComponent<TextMeshProUGUI>().text = "Do you want to teleport to " + scenename + " ?";
            }
        }
        
    }
    public void TeleportYes()
    {
        mouselocksys.GetComponent<Mouselock_controller>().deblocking();
        StartCoroutine(DisconnectAndLoad());
        /*StartCoroutine("Teleport");
        panel.SetActive(false);*/

    }

    IEnumerator DisconnectAndLoad()
    {
        PhotonNetwork.Disconnect();
        while (PhotonNetwork.IsConnected)
            yield return null;
        SceneManager.LoadScene(scenename);
    }


    private IEnumerator Teleport()
    {
        GameObject player = GameObject.FindGameObjectWithTag("photonmanager").GetComponent<LauncherScript>().thePlayer;
        player.GetComponent<CharacterController>().enabled = false;
        yield return new WaitForSeconds(0.01f);
        player.transform.position = target.position;
        yield return new WaitForSeconds(0.01f);
        player.GetComponent<CharacterController>().enabled = true;
    }
    public void TeleportNo()
    {
        mouselocksys.GetComponent<Mouselock_controller>().deblocking();
        panel.SetActive(false);
    }
}
