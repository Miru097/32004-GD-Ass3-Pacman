using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PacStudentController : MonoBehaviour
{
    private Vector2 nextPosition = Vector2.zero;
    private Tweener tweener;
    private float duration = 0.1f;
    private string lastInput = default;
    private string currentInput = default;
    private string lastCurrentInput = default;
    public AudioSource pacMoveSource = default;
    public AudioClip[] pacMoveClips = new AudioClip[3];
    private bool mapAudio = false;
    RaycastHit2D hitRecord = default;
    public ParticleSystem dust;
    public ParticleSystem wall;
    public ParticleSystem dead;
    private Vector2 wallParticlePosition;
    int Score = 0;
    public AudioSource ghostMoveSource = default;
    public AudioClip[] ghostMoveClips = new AudioClip[3];
    [SerializeField] private GameObject[] ghosts = new GameObject[4];
    private int GhostScaredTotalTime = 10;
    private int[] GhostDeadTotalTime = new int[4];
    private GameObject GhostScaredTimer = default;
    private GameObject GhostScaredTimerText = default;
    private GameObject RoundTimerText = default;
    private GameObject LiveText = default;
    private GameObject GameTimerText = default;
    private bool ghostNormalSwitchedOn = false;
    private bool ghostScaredSwitchedOn = false;
    private bool ghostDeadSwitchedOn = false;
    private bool ghostRecoveringFlag = false;
    private bool gameOverFlag = false;
    private bool[] ghostDeadFlag = new bool[4];
    private bool[] deadOnce = new bool[4];
    private int RoundTotalTime = 3;
    private float Timer;
    private int min;
    private int sec;
    private int milsec;
    private float gameOverCountDown;
    private GameObject dot;
    private GameObject PowerPellet;

    // Start is called before the first frame update
    void Start()
    {
        nextPosition = (Vector2)transform.position;
        tweener = GetComponent<Tweener>();
        lastInput = null;
        currentInput = null;
        lastCurrentInput = null;
        wall.Stop();
        dead.Stop();
        GhostScaredTimer = GameObject.FindGameObjectWithTag("GhostScaredTimer");
        GhostScaredTimerText = GameObject.FindGameObjectWithTag("GhostScaredTimerText");
        RoundTimerText = GameObject.FindGameObjectWithTag("CountDown");
        LiveText = GameObject.FindGameObjectWithTag("LiveText");
        GameTimerText = GameObject.FindGameObjectWithTag("GameTimerText");
        dot = GameObject.FindWithTag("Dot");
        PowerPellet = GameObject.FindWithTag("PowerPellet");
        Score = 0;
        PlayerPrefs.SetInt("Score", 0);
        gameOverFlag = false;
        gameOverCountDown = 3;
        Timer = 0;
        for (int i = 0; i < 4; i++)
        {
            GhostDeadTotalTime[i] = 5;
            ghostDeadFlag[i] = false;
        }
        StartCoroutine("RoundCountDown");
    }

    // Update is called once per frame
    private void Update()
    {
        if (RoundTotalTime == 0 && !gameOverFlag)
        {
            RoundTimerText.GetComponent<TextMeshProUGUI>().text = "GO!";
            StopCoroutine("RoundCountDown");
        }
        if (GameObject.FindWithTag("Audio_Intro").GetComponent<AudioSource>().isPlaying)
        {
            for (int i = 0; i < 4; i++)
            {
                ghosts[i].GetComponent<Animator>().enabled = false;
            }
        }
        if (!gameOverFlag)
        {
            if (ghostNormalSwitchedOn == false && GameObject.FindWithTag("Audio_Intro").GetComponent<AudioSource>().isPlaying == false
                && !ghostScaredSwitchedOn && !ghostDeadSwitchedOn)
            {
                ghostMoveSource.clip = ghostMoveClips[0];
                ghostMoveSource.Play();
                ghostNormalSwitchedOn = true;
                RoundTimerText.SetActive(false);
            }
            SetAnimator();
            if (GameObject.FindWithTag("Audio_Intro").GetComponent<AudioSource>().isPlaying == false)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (!ghosts[i].GetComponent<Animator>().enabled)
                        ghosts[i].GetComponent<Animator>().enabled = true;
                }
                setTimer();
                InputRecord();
                if ((Vector2)transform.position == nextPosition)
                {

                    CurrentRecord();
                    MovePacman();
                    MoveAudio();
                }
                if (GhostScaredTotalTime == 0)
                {
                    StopCoroutine("GhostScaredCountDown");
                    GhostScaredTotalTime = 10;
                    GhostScaredTimer.SetActive(false);
                    for (int i = 0; i < 4; i++)
                    {
                        if (!ghostDeadFlag[i])
                            ghosts[i].GetComponent<Animator>().SetTrigger("Normal");
                        deadOnce[i] = false;
                    }
                    ghostNormalSwitchedOn = false;
                    ghostScaredSwitchedOn = false;
                }
                if (GhostScaredTotalTime == 3 & ghostRecoveringFlag == false)
                {
                    ghostRecoveringFlag = true;
                    for (int i = 0; i < 4; i++)
                    {
                        if (!ghostDeadFlag[i])
                            ghosts[i].GetComponent<Animator>().SetTrigger("Recovering");
                    }
                }
                for (int i = 0; i < 4; i++)
                {
                    if (GhostDeadTotalTime[i] == 0)
                    {
                        ghostDeadFlag[i] = false;
                        ghosts[i].GetComponent<Animator>().SetTrigger("Normal");
                        GhostDeadTotalTime[i] = 5;
                    }
                }
                if (!ghostDeadFlag[0] && !ghostDeadFlag[1] &&
                    !ghostDeadFlag[2] && !ghostDeadFlag[3])
                {
                    if (GhostScaredTotalTime > 0 && !ghostScaredSwitchedOn && ghostDeadSwitchedOn && GhostScaredTotalTime != 10)
                    {
                        ghostMoveSource.clip = ghostMoveClips[1];
                        ghostMoveSource.Play();
                        ghostScaredSwitchedOn = true;
                        ghostDeadSwitchedOn = false;
                    }
                }
            }
            CheckDot();
        }
        if (gameOverFlag)
        {

            gameOverCountDown -= Time.deltaTime;
            if (gameOverCountDown <= 0)
            {
                SceneManager.LoadScene(1);
            }
        }
        
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name != "Cherry(Clone)")
        {
            if (collision.gameObject.name != "Cherry(Clone)" && (collision.gameObject.name == "MapUpperLeft44" || collision.gameObject.name == "MapUpperRight44" ||
                collision.gameObject.name == "MapLowerLeft44" || collision.gameObject.name == "MapLowerRight44"))
            {
                Destroy(GameObject.Find(collision.gameObject.name));
                for (int i = 0; i < 4; i++)
                {
                    if (!ghostDeadFlag[i])
                        ghosts[i].GetComponent<Animator>().SetTrigger("Scared");
                }
                if (ghostNormalSwitchedOn && !ghostDeadSwitchedOn && !ghostScaredSwitchedOn)
                {
                    ghostMoveSource.clip = ghostMoveClips[1];
                    ghostMoveSource.Play();
                    ghostNormalSwitchedOn = false;
                    ghostScaredSwitchedOn = true;
                }
                GhostScaredTotalTime = 10;
                ghostRecoveringFlag = false;
                StartCoroutine("GhostScaredCountDown");
                GhostScaredTimer.SetActive(true);
            }
            else if (collision.gameObject.name != "Cherry(Clone)" && collision.gameObject.name == "Ghost1")
            {
                if ((GhostScaredTotalTime == 10 && !ghostDeadFlag[0]) || (GhostScaredTotalTime != 10 && deadOnce[0] && !ghostDeadFlag[0]))
                {
                    GhostWalkingState();
                }
                else
                {
                    if (!ghostDeadFlag[0])
                        GhostScaredState(0);
                }
            }
            else if (collision.gameObject.name != "Cherry(Clone)" && collision.gameObject.name == "Ghost2")
            {
                if ((GhostScaredTotalTime == 10 && !ghostDeadFlag[1]) || (GhostScaredTotalTime != 10 && !ghostDeadFlag[1] && deadOnce[1]))
                {
                    GhostWalkingState();
                }
                else
                {
                    if (!ghostDeadFlag[1])
                        GhostScaredState(1);
                }
            }
            else if (collision.gameObject.name != "Cherry(Clone)" && collision.gameObject.name == "Ghost3")
            {
                if ((GhostScaredTotalTime == 10 && !ghostDeadFlag[2]) || (GhostScaredTotalTime != 10 && deadOnce[2]) && !ghostDeadFlag[2])
                {
                    GhostWalkingState();
                }
                else
                {
                    if (!ghostDeadFlag[2])
                        GhostScaredState(2);
                }
            }
            else if (collision.gameObject.name != "Cherry(Clone)" && collision.gameObject.name == "Ghost4")
            {
                if ((GhostScaredTotalTime == 10 && !ghostDeadFlag[3]) || (GhostScaredTotalTime != 10 && deadOnce[3] && !ghostDeadFlag[3]))
                {
                    GhostWalkingState();
                }
                else
                {
                    if (!ghostDeadFlag[3])
                        GhostScaredState(3);
                }
            }
            else
            {
                Destroy(GameObject.Find(collision.gameObject.name));
                Score += 10;
                PlayerPrefs.SetInt("Score", Score);
            }

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
        if (!hitRecord || (hitRecord && hitRecord.collider.gameObject.name == "Map") || currentInput == null)
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
        if (currentInput != null)
        {
            tweener.AddTween(transform, transform.position, nextPosition, duration);
            Vector2 dir = nextPosition - (Vector2)transform.position;
            GetComponent<Animator>().SetFloat("DirX", dir.x);
        }

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
                if (hitRecord.collider.gameObject.name != "Map" && hitRecord.collider.gameObject.name != "Pacman" && hitRecord.collider.gameObject.name != "Ghost1"
                    && hitRecord.collider.gameObject.name != "Ghost2" && hitRecord.collider.gameObject.name != "Ghost3" && hitRecord.collider.gameObject.name != "Ghost4")
                {
                    pacMoveSource.clip = pacMoveClips[0];
                    pacMoveSource.Play();
                }
                if (hitRecord.collider.gameObject.name != "Map" && hitRecord.collider.gameObject.name == "Pacman")
                {
                    pacMoveSource.clip = pacMoveClips[1];
                    pacMoveSource.Play();
                }
            }
        }
    }
    private void GhostWalkingState()
    {
        if (Convert.ToInt32(LiveText.GetComponent<Text>().text) > 1)
        {
            LiveText.GetComponent<Text>().text = Convert.ToString(Convert.ToInt32(LiveText.GetComponent<Text>().text) - 1);
            Destroy(GameObject.Find("Heart" + LiveText.GetComponent<Text>().text));
            dead.transform.position = wallParticlePosition;
            dead.Play();
            lastInput = null;
            currentInput = null;
            lastCurrentInput = null;
            tweener.AddTween(transform, transform.position, new Vector2(-13, 14), 0);
            nextPosition = new Vector2(-13, 14);
        }
        else
        {
            Destroy(GameObject.Find("Heart0"));
            GameOver();
        }

    }
    private void GhostScaredState(int i)
    {
        ghosts[i].GetComponent<Animator>().SetTrigger("Dead");
        ghostDeadFlag[i] = true;
        deadOnce[i] = true;
        if (!ghostDeadSwitchedOn)
        {
            ghostMoveSource.clip = ghostMoveClips[2];
            ghostMoveSource.Play();
            ghostScaredSwitchedOn = false;
            ghostDeadSwitchedOn = true;
        }
        Score += 300;
        PlayerPrefs.SetInt("Score", Score);
        GhostDeadTotalTime[i] = 5;
        StartCoroutine("GhostDeadCountDown", i);
    }
    IEnumerator GhostScaredCountDown()
    {
        while (GhostScaredTotalTime >= 0)
        {
            GhostScaredTimerText.GetComponent<Text>().text = GhostScaredTotalTime.ToString();
            yield return new WaitForSeconds(1);
            GhostScaredTotalTime--;
        }
    }
    IEnumerator GhostDeadCountDown(int i)
    {
        while (GhostDeadTotalTime[i] > 0 && ghostDeadFlag[i])
        {
            yield return new WaitForSeconds(1);
            GhostDeadTotalTime[i]--;
        }
    }
    IEnumerator RoundCountDown()
    {
        while (RoundTotalTime > 0)
        {
            RoundTimerText.GetComponent<TextMeshProUGUI>().text = RoundTotalTime.ToString();
            yield return new WaitForSeconds(1);
            RoundTotalTime--;
        }
    }
    private void setTimer()
    {
        Timer += Time.deltaTime;
        min = (int)(Timer / 60);
        sec = (int)(Timer % 60);
        milsec = (int)((Timer - min - sec) * 100);
        //milsec = (int)(Timer * 100);
        //if (milsec >= 100)
        //{
        //    sec++;
        //    milsec = 0;
        //    Timer = 0;
        //}
        //if (sec >= 60)
        //{
        //    min++;
        //    sec = 0;
        //}
        GameTimerText.GetComponent<Text>().text = string.Format("{0:d2}:{1:d2}:{2:d2}", min, sec, milsec);
    }
    private void GameOver()
    {
        gameOverFlag = true;
        for (int i = 0; i < 4; i++)
        {
            if (ghosts[i].GetComponent<Animator>().enabled)
                ghosts[i].GetComponent<Animator>().enabled = false;
        }
        GhostScaredTimer.SetActive(false);
        tweener.AddTween(transform, transform.position, transform.position, 0);
        GetComponent<Animator>().enabled = false;
        dust.Stop();
        wall.Stop();
        dead.Stop();
        RoundTimerText.GetComponent<TextMeshProUGUI>().text = "Game Over";
        RoundTimerText.SetActive(true);
        if (PlayerPrefs.GetInt("HighScore") == 0|| PlayerPrefs.GetInt("HighScore") < Score)
        {
            PlayerPrefs.SetInt("HighScore", Score);
            PlayerPrefs.SetFloat("HighScoreTime", Timer);
        }
        else if(PlayerPrefs.GetInt("HighScore")== Score)
        {
            if (PlayerPrefs.GetFloat("HighScoreTime")> Timer)
            {
                PlayerPrefs.SetFloat("HighScoreTime", Timer);
            }
        }
    }
    private void CheckDot()
    {
        if (PowerPellet.transform.childCount==0&& dot.transform.childCount == 0)
        {
            GameOver();
            
        }
    }
}

