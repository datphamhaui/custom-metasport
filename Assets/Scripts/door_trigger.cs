using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door_trigger : MonoBehaviour
{
    bool isopen = false;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim= GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (!isopen)
        {
            anim.Play("Closed");
            isopen = true;
            
            Invoke("closeDoor", 5.0f);
        }
        
    }
    void closeDoor()
    {
        anim.Play("Opened");
        isopen = false;
    }
}
