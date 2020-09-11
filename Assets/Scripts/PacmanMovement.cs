using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanMovement : MonoBehaviour
{
    private bool switchedOn = false;
    private GameObject pacman = default;
    private Animator pacmanAnimator = default;

    float timeElapsed;
    const float lerpDurationA = 4.8f;
    const float lerpDurationB = 2f;

    private float oldX;
    private float newX;

    //Move by speed
    //float speed = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        pacman = GameObject.FindWithTag("Pacman");
        pacman.transform.position = new Vector2(-13, 14);
        pacmanAnimator = pacman.GetComponent<Animator>();
        pacmanAnimator.SetTrigger("Pacman_TurnRight");
        timeElapsed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (switchedOn == false && GameObject.FindWithTag("Audio_Intro").GetComponent<AudioSource>().isPlaying == false)
        {
            GameObject.FindWithTag("SE_notEat").GetComponent<AudioSource>().Play();
            switchedOn = true;
        }
        if (GameObject.FindWithTag("SE_notEat").GetComponent<AudioSource>().isPlaying == true)
        {
            oldX = pacman.transform.position.x;
            StartLoop();
            newX = pacman.transform.position.x;
            SetTrigger();
        }
    }
    private void StartLoop()
    {

        if (timeElapsed >= lerpDurationA && timeElapsed < (lerpDurationA + lerpDurationB))
        {
            pacman.transform.position = Vector3.Lerp(new Vector3(-2f, 14f, 0), new Vector3(-2f, 10f, 0), (timeElapsed - lerpDurationA) / lerpDurationB);
            timeElapsed += Time.deltaTime;
        }
        if (timeElapsed >= (lerpDurationA + lerpDurationB) && timeElapsed < (lerpDurationA * 2 + lerpDurationB))
        {
            pacman.transform.position = Vector3.Lerp(new Vector3(-2f, 10f, 0), new Vector3(-13f, 10f, 0), (timeElapsed - lerpDurationA - lerpDurationB) / lerpDurationA);
            timeElapsed += Time.deltaTime;
        }
        if (timeElapsed >= (lerpDurationA * 2 + lerpDurationB) && timeElapsed < 2 * (lerpDurationA + lerpDurationB))
        {
            pacman.transform.position = Vector3.Lerp(new Vector3(-13f, 10f, 0), new Vector3(-13f, 14f, 0), (timeElapsed - lerpDurationA * 2 - lerpDurationB) / lerpDurationB);
            timeElapsed += Time.deltaTime;
        }
        if (timeElapsed >= 0 && timeElapsed < lerpDurationA)
        {
            pacman.transform.position = Vector3.Lerp(new Vector3(-13f, 14f, 0), new Vector3(-2f, 14f, 0), timeElapsed / lerpDurationA);
            timeElapsed += Time.deltaTime;
        }
        if (timeElapsed >= 2 * (lerpDurationA + lerpDurationB))
        {
            timeElapsed = 0;
        }
    }
    private void SetTrigger()
    {
        if (oldX < newX)
        {
            pacmanAnimator.SetTrigger("Pacman_TurnRight");
        }
        else
        {
            pacmanAnimator.SetTrigger("Pacman_TurnLeft");
        }
    }
}
/*
if(pacman.transform.position.x>=-13 && pacman.transform.position.x<-2&& pacman.transform.position.y == 14)
{
    pacman.transform.position += (Vector3.right * speed * Time.deltaTime);
}
if(pacman.transform.position.x==-2 && pacman.transform.position.y <= 14 &&pacman.transform.position.y>10)
{
    pacman.transform.position += (Vector3.down * speed * Time.deltaTime);
}
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