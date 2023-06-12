using UnityEngine;

public class Quit_trigger : MonoBehaviour
{

    public GameObject panel;
    public GameObject mouselocksys;

    public void ShowQuit()
    {
        mouselocksys.GetComponent<Mouselock_controller>().locking(false);
        panel.SetActive(true);
        
    }

    public void QuitYes()
    {
        mouselocksys.GetComponent<Mouselock_controller>().deblocking();
        panel.SetActive(false);
        Application.Quit();
    }

    public void QuitNo()
    {
        mouselocksys.GetComponent<Mouselock_controller>().deblocking();
        panel.SetActive(false);
    }

}
