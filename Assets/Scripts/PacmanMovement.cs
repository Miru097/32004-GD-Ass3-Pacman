using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanMovement : MonoBehaviour
{
    private bool switchedOn = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (switchedOn == false && GameObject.FindWithTag("Audio_Intro").GetComponent<AudioSource>().isPlaying == false)
        {
            GameObject.FindWithTag("SE_notEat").GetComponent<AudioSource>().Play();
            switchedOn = true;
        }
    }
}
