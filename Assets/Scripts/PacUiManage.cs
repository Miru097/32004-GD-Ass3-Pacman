using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PacUiManage : MonoBehaviour
{
    Text aspectRatio = default;
    GameObject GhostScaredTimer = default;
    Text ScoreText = default;
    private void Awake()
    {
        aspectRatio = GameObject.FindGameObjectWithTag("AspectRatioText").GetComponent<Text>();
        GhostScaredTimer = GameObject.FindGameObjectWithTag("GhostScaredTimer");
        GhostScaredTimer.SetActive(false);
        ScoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ChangeAspect();
        if (GameObject.FindWithTag("Audio_Intro").GetComponent<AudioSource>().isPlaying == false)
            ScoreUpdate();
    }
    public void LoadMenuLevel()
    {
        SceneManager.LoadScene(1);
    }
    public void ChangeAspect()
    {
        if (Camera.main.aspect > 1.34)
        {
            aspectRatio.text = "16:9";
        }
        else
        {
            aspectRatio.text = "4:3";
        }
    }
    public void ScoreUpdate()
    {
        ScoreText.text = Convert.ToString( PlayerPrefs.GetInt("Score"));
    }
}
