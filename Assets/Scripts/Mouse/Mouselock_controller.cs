using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
public class Mouselock_controller : MonoBehaviour
{
    MonoBehaviour cameraScript;
    PlayerInput player;
    public GameObject cursor;
    public static bool isLocked = false;
    bool playerIsIn = false;
    bool withControl = true;

    // Update is called once per frame
    private void Start()
    {
        cameraScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CinemachineBrain>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        if (NonUIInput.GetKeyDown(KeyCode.LeftControl))
        {
            lockscreen();
        }
    }

    public void lockscreen()
    {
        if (withControl)
        {

            if (isLocked)
            {
                deblocking();
            }
            else
            {
                locking(true);
            }
        }
        else
        {
            if (isLocked)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }

        }
    }
    public void locking(bool control)
    {
        if (!playerIsIn)
        {
            player = GameObject.FindGameObjectWithTag("photonmanager").GetComponent<ManagerFusion>()._playerPrefabs[MyClikc.x].GetComponent<PlayerInput>();
            playerIsIn = true;
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        cursor.SetActive(false);
        cameraScript.enabled = false;
        player.enabled = false;

        withControl = control;
        isLocked = true;
    }
    public void deblocking()
    {
        withControl = true;
        isLocked = false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        cursor.SetActive(true);
        cameraScript.enabled = true;
        player.enabled = true;


    }
}
