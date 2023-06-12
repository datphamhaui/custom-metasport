using UnityEngine;
using System.Collections;

public class UI_elements : MonoBehaviour
{
    public static GameObject highlight;
    public GameObject mapPanel;
    public GameObject mouselocksys;
    private void Awake()
    {
        highlight = GameObject.FindGameObjectWithTag("cur_high");
        highlight.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (NonUIInput.GetKeyDown(KeyCode.Tab))
        {
            mapPanel.SetActive(!mapPanel.activeInHierarchy);
        }
    }

    public void teleport(Transform target)
    {
        StartCoroutine("Teleport",target);
        mapPanel.SetActive(false);
        mouselocksys.GetComponent<Mouselock_controller>().deblocking();
    }

    private IEnumerator Teleport(Transform target)
    {
        GameObject player = GameObject.FindGameObjectWithTag("photonmanager").GetComponent<LauncherScript>().thePlayer;
        player.GetComponent<CharacterController>().enabled = false;
        yield return new WaitForSeconds(0.01f);
        player.transform.position = target.position;
        player.transform.eulerAngles = target.eulerAngles;
        yield return new WaitForSeconds(0.01f);
        player.GetComponent<CharacterController>().enabled = true;
    }
}
