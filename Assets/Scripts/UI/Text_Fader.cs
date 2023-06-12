using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
public class Text_Fader : MonoBehaviour
{
    public bool isfading=true;
    public float minFadingDistance=5.0f;
    public float maxFadingDistance=15.0f;
    public bool isfacingatplayer=true;
    public bool isreversefading = false;
    Color text_alpha;
    TextMeshProUGUI text;
    RectTransform textTransform;
    // Start is called before the first frame update
    void Start()
    {
        text= GetComponent<TextMeshProUGUI>();
        textTransform= GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
            if (text != null)
            {
                if (isfading)
                {
                    text_alpha = text.color;
                    text_alpha.a = fadeout();
                    text.color = text_alpha;
                }
                if (isreversefading)
                {
                    text_alpha = text.color;
                    text_alpha.a = reversefadeout();
                    text.color = text_alpha;
                }

        }
            if (textTransform != null)
            {
                if (isfacingatplayer)
                {
                    facing();
                }
            }
    }
    void facing()
    {
        Vector3 targetPostition = Camera.main.transform.position;
        targetPostition.y= textTransform.position.y;
        textTransform.LookAt(targetPostition);
        textTransform.Rotate(0, 180, 0);

    }
    float fadeout()
    {
        float dist = Vector3.Distance(Camera.main.transform.position, text.transform.position);
        if (dist < minFadingDistance)
        {
            return 1.0f;
        }
        if(dist > maxFadingDistance)
        {
            return 0.0f;
        }
        return Mathf.Max(1-((dist - minFadingDistance) / (maxFadingDistance - minFadingDistance)), 0.0f);


    }
    float reversefadeout()
    {
        float dist = Vector3.Distance(Camera.main.transform.position, text.transform.position);
        if (dist < minFadingDistance)
        {
            return 0.0f;
        }
        if (dist > maxFadingDistance)
        {
            return 1.0f;
        }
        return Mathf.Max(((dist - minFadingDistance) / (maxFadingDistance - minFadingDistance)), 0.0f);


    }
}
