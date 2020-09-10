﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalState : MonoBehaviour
{
    private bool switchedOn = false;
    // Start is called before the first frame update
    void Start()
    {
        //GameObject.FindWithTag("Audio_Normal").GetComponent<AudioSource>().PlayDelayed(3.7f);
    }

    // Update is called once per frame
    void Update()
    {
        if (switchedOn == false && GameObject.FindWithTag("Audio_Intro").GetComponent<AudioSource>().isPlaying == false)
        {
            GameObject.FindWithTag("Audio_Normal").GetComponent<AudioSource>().Play();
            switchedOn = true;
        }
    }
}