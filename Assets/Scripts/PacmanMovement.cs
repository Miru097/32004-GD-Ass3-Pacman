using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanMovement : MonoBehaviour
{
    private bool switchedOn = false;
    private GameObject pacman = default;
    private float timer = default;
    private Animator pacmanAnimator = default;

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
            timer = 0;
        }
        if (GameObject.FindWithTag("SE_notEat").GetComponent<AudioSource>().isPlaying == true && timer >= 0.4f)
        {
            if(pacman.transform.position.x!=-2 && pacman.transform.position.y == 14)
            {
                pacman.transform.Translate(Vector2.right);
            }

            else if (pacman.transform.position.x == -2 && pacman.transform.position.y != 10)
            {
                pacman.transform.Translate(Vector2.down);
            }
            else if (pacman.transform.position.x != -13 && pacman.transform.position.y == 10)
            {
                pacman.transform.Translate(Vector2.left);
            }
            else
            {
                pacman.transform.Translate(Vector2.up);
            }
            if (pacman.transform.position.x == -13 && pacman.transform.position.y == 14)
            {
                pacmanAnimator.SetTrigger("Pacman_TurnRight");
            }
            if(pacman.transform.position.x == -2 && pacman.transform.position.y == 10)
            {
                pacmanAnimator.SetTrigger("Pacman_TurnLeft");
            }
            timer = 0;
        }
        timer += Time.deltaTime;
    }
}
