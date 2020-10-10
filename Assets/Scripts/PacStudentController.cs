using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PacStudentController : MonoBehaviour
{
    private Vector2 nextPosition = Vector2.zero;
    private Tweener tweener;
    private float duration = 0.48f;
    private string lastInput = default;
    private string currentInput = default;
    private string lastCurrentInput = default;
    public AudioSource pacMoveSource = default;
    public AudioClip[] pacMoveClips = new AudioClip[3];
    private bool mapAudio = false;
    RaycastHit2D hitRecord = default;
    public ParticleSystem dust;
    public ParticleSystem wall;
    private Vector2 wallParticlePosition;
    int Score = 0;

    // Start is called before the first frame update
    void Start()
    {
        nextPosition = (Vector2)transform.position;
        //startTime = Time.time;
        tweener = GetComponent<Tweener>();
        lastInput = null;
        currentInput = null;
        lastCurrentInput = null;
        wall.Stop();
        Score = 0;
        PlayerPrefs.SetInt("Score", 0);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {

        SetAnimator();
        //if (GameObject.FindWithTag("Audio_Intro").GetComponent<AudioSource>().isPlaying == false)
        //{
        InputRecord();
        if ((Vector2)transform.position == nextPosition)
        {
            CurrentRecord();
            MovePacman();
            MoveAudio();
        }

        //}
    }
    private bool Valid(Vector2 dir)
    {
        Vector2 pos = transform.position;
        RaycastHit2D hit = Physics2D.Linecast(pos + dir, pos);
        wallParticlePosition = pos + dir;
        hitRecord = hit;
        return (hit.collider.gameObject.name != "Map");
        //return (hit.collider == GetComponent<Collider2D>());


    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision Enter:" + collision.gameObject.name + ":" + collision.contacts[0].point);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name != "Cherry(Clone)")
        {
            Destroy(GameObject.Find(collision.gameObject.name));
            Score += 10;
            PlayerPrefs.SetInt("Score", Score);
        }
        if (collision.gameObject.name == "Cherry(Clone)")
        {
            Destroy(GameObject.Find("Cherry(Clone)"));
            Score += 100;
            PlayerPrefs.SetInt("Score", Score);
        }

    }
    private void SetAnimator()
    {
        if (!hitRecord || (hitRecord && hitRecord.collider.gameObject.name == "Map"))
        {
            GetComponent<Animator>().enabled = false;
            dust.Stop();

        }
        else
        {
            GetComponent<Animator>().enabled = true;
            if (!dust.isPlaying)
            {
                dust.Play();
                wall.Stop();
            }
        }
    }
    private void InputRecord()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            lastInput = "W";
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            lastInput = "S";
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            lastInput = "A";
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            lastInput = "D";
        }
    }
    private void CurrentRecord()
    {
        if (lastInput == "W")
        {
            if (Valid(Vector2.up))
            {
                currentInput = "W";
            }
        }
        if (lastInput == "S")
        {
            if (Valid(Vector2.down))
            {
                currentInput = "S";
            }
        }
        if (lastInput == "A")
        {
            if (Valid(Vector2.left))
            {
                currentInput = "A";
            }
        }
        if (lastInput == "D")
        {
            if (Valid(Vector2.right))
            {
                currentInput = "D";
            }
        }
        if (lastCurrentInput != currentInput)
        {
            mapAudio = true;
            lastCurrentInput = currentInput;
        }
    }
    private void MovePacman()
    {
        if (currentInput == "W" && Valid(Vector2.up))
        {
            nextPosition = (Vector2)transform.position + Vector2.up;
        }
        if (currentInput == "S" && Valid(Vector2.down))
        {
            nextPosition = (Vector2)transform.position + Vector2.down;
        }
        if (currentInput == "A" && Valid(Vector2.left))
        {
            if ((Vector2)transform.position == new Vector2(-13f, 1f))
            {
                transform.position = new Vector2(12f, 1f);
            }
            nextPosition = (Vector2)transform.position + Vector2.left;

        }
        if (currentInput == "D" && Valid(Vector2.right))
        {
            if ((Vector2)transform.position == new Vector2(12f, 1f))
            {
                transform.position = new Vector2(-13f, 1f);
            }
            nextPosition = (Vector2)transform.position + Vector2.right;

        }
        tweener.AddTween(transform, transform.position, nextPosition, duration);
        Vector2 dir = nextPosition - (Vector2)transform.position;
        GetComponent<Animator>().SetFloat("DirX", dir.x);
    }
    private void MoveAudio()
    {
        if (hitRecord)
        {
            if (hitRecord.collider.gameObject.name == "Map" && mapAudio)
            {
                pacMoveSource.clip = pacMoveClips[2];
                pacMoveSource.Play();
                mapAudio = false;
                wall.transform.position = wallParticlePosition;
                wall.Play();
            }
            else
            {
                if (pacMoveSource.isPlaying == false && hitRecord.collider.gameObject.name != "Map" && hitRecord.collider.gameObject.name != "Pacman")
                {
                    pacMoveSource.clip = pacMoveClips[0];
                    pacMoveSource.Play();
                }
                if (pacMoveSource.isPlaying == false && hitRecord.collider.gameObject.name != "Map" && hitRecord.collider.gameObject.name == "Pacman")
                {
                    pacMoveSource.clip = pacMoveClips[1];
                    pacMoveSource.Play();
                }
            }
        }
    }
}

