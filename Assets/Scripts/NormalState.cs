using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalState : MonoBehaviour
{
    private AudioSource audioSource = default;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!GameObject.FindWithTag("Audio_Intro").GetComponent<AudioSource>().isPlaying)
        {
            GameObject.FindWithTag("Audio_Normal").GetComponent<AudioSource>().Play();
            //_ = GameObject.FindWithTag("Audio_Normal").GetComponent<AudioSource>().enabled;
        }
    }
}
