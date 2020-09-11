using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanMovement : MonoBehaviour
{
    private bool switchedOn = false;
    private GameObject pacman = default;
    private Animator pacmanAnimator = default;

    float timeElapsed;
    float lerpDuration = 4.8f;

    //Move by speed
    float speed = 2.5f;
    // Start is called before the first frame update
    void Start()
    {
        pacman = GameObject.FindWithTag("Pacman");
        pacman.transform.position = new Vector2(-13, 14);
        pacmanAnimator = pacman.GetComponent<Animator>();
        pacmanAnimator.SetTrigger("Pacman_TurnRight");

    }

    // Update is called once per frame
    void Update()
    {
        if (switchedOn == false && GameObject.FindWithTag("Audio_Intro").GetComponent<AudioSource>().isPlaying == false)
        {
            GameObject.FindWithTag("SE_notEat").GetComponent<AudioSource>().Play();
            switchedOn = true;
        }
        //move by distance
        if (GameObject.FindWithTag("SE_notEat").GetComponent<AudioSource>().isPlaying == true)
        {
            if (timeElapsed < lerpDuration)
            {
                if (pacman.transform.position.x != -2f && pacman.transform.position.y == 14f)
                {
                    pacman.transform.position = Vector3.Lerp(new Vector3(-13f, 14f, 0), new Vector3(-2f, 14f, 0), timeElapsed / lerpDuration);
                    timeElapsed += Time.deltaTime;
                }
                else if (pacman.transform.position.x == -2f && pacman.transform.position.y != 10f)
                {
                    pacman.transform.position = Vector3.Lerp(new Vector3(-2f, 14f, 0), new Vector3(-2f, 10f, 0), timeElapsed / 2f);
                }
            }



            /*
            if(pacman.transform.position.x>=-13 && pacman.transform.position.x<-2&& pacman.transform.position.y == 14)
            {
                pacman.transform.position += (Vector3.right * speed * Time.deltaTime);
                Debug.Log(pacman.transform.position);
            }
            if(pacman.transform.position.x==-2 && pacman.transform.position.y <= 14 &&pacman.transform.position.y>10)
            {
                pacman.transform.position += (Vector3.down * speed * Time.deltaTime);
                Debug.Log(pacman.transform.position+"1");
            }
            /*
            if (pacman.transform.position.x != -2 && pacman.transform.position.y == 14)
            {
                pacman.transform.position += (Vector3.right * speed*Time.deltaTime);
            }
            else if (pacman.transform.position.x == -2 && pacman.transform.position.y != 10)
            {
                pacman.transform.position += (Vector3.down * speed * Time.deltaTime);
            }
            else if (pacman.transform.position.x != -13 && pacman.transform.position.y == 10)
            {
                pacman.transform.position += (Vector3.left * speed * Time.deltaTime);
            }
            else
            {
                pacman.transform.position += (Vector3.up * speed * Time.deltaTime);
            }
            */
            setTrigger();
        }



        //move with distance
        /*
        if (GameObject.FindWithTag("SE_notEat").GetComponent<AudioSource>().isPlaying == true && timer >= 0.4f)
        {
            pacman.transform.position += (direction * speed);
            timer = 0;
        }
        timer += Time.deltaTime;
        */
    }
    private void setTrigger()
    {
        if (pacman.transform.position.x == -13 && pacman.transform.position.y == 14)
        {
            pacmanAnimator.SetTrigger("Pacman_TurnRight");
        }
        if (pacman.transform.position.x == -2 && pacman.transform.position.y == 10)
        {
            pacmanAnimator.SetTrigger("Pacman_TurnLeft");
        }
    }
}
