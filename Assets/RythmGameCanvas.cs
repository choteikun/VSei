using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RythmGameCanvas : MonoBehaviour
{
    Animation anim;
    Image perfectImage;
    Image goodImage;

    void Start()
    {
        anim = GetComponent<Animation>();
        perfectImage = GameObject.Find("PerfectImage").GetComponent<Image>();
        goodImage = GameObject.Find("GoodImage").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PerfectEffect()
    {
        if (!anim.isPlaying)
        {
            anim.Play("PerfectEffect");
        }
        else
        {
            ResetAnimTime();
            anim.Play("PerfectEffect");
        }
    }
    public void GoodEffect()
    {
        if (!anim.isPlaying)
        {
            anim.Play("GoodEffect");
        }
        else
        {
            ResetAnimTime();
            anim.Play("GoodEffect");
        }
    }
    public void ResetAnimTime()
    {
        perfectImage.color = new Color(255, 255, 255, 0);
        goodImage.color = new Color(255, 255, 255, 0);
    }
}
