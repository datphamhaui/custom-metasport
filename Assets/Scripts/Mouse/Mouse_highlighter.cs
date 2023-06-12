using UnityEngine;

public class Mouse_highlighter : MonoBehaviour
{
    GameObject highlight;
    void Start()
    {
        highlight = UI_elements.highlight;
    }

    private void OnMouseOver()
    {
        if (highlight != null)
        {
            if (!Mouselock_controller.isLocked)
                highlight.SetActive(true);
            else
                highlight.SetActive(false);
        }
        
    }
    private void OnMouseExit()
    {
        if (highlight != null)
        {
            highlight.SetActive(false);
        }
    }
}
