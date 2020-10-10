using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManage : MonoBehaviour
{

    private GameObject ScoreText = default;
    private GameObject TimeText = default;
    private float Timer;
    private int min;
    private int sec;
    private int milsec;
    // Start is called before the first frame update
    void Start()
    {
        ScoreText = GameObject.FindGameObjectWithTag("ScoreText");
        TimeText = GameObject.FindGameObjectWithTag("TimeText");
        ScoreText.GetComponent<Text>().text = Convert.ToString(PlayerPrefs.GetInt("HighScore"));
        changeFormat();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void LoadPacmanLevel()
    {
        SceneManager.LoadScene(0);
    }
    public void LoadDesignLevel()
    {
        SceneManager.LoadScene(2);
    }
    private void changeFormat()
    {
        Timer = PlayerPrefs.GetFloat("HighScoreTime");
        min = (int)(Timer / 60);
        sec = (int)(Timer % 60);
        milsec = (int)((Timer - min - sec) * 100);
        TimeText.GetComponent<Text>().text = string.Format("{0:d2}:{1:d2}:{2:d2}", min, sec, milsec);
    }
}
