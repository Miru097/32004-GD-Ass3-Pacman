using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PacUiManage : MonoBehaviour
{
    Text aspectRatio = default;
    GameObject GhostScaredTimer = default;
    // Start is called before the first frame update
    void Start()
    {
        aspectRatio = GameObject.FindGameObjectWithTag("AspectRatioText").GetComponent<Text>();
        GhostScaredTimer = GameObject.FindGameObjectWithTag("GhostScaredTimer");
        GhostScaredTimer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Camera.main.aspect >1.34)
        {
            aspectRatio.text = "16:9";
        }
        else
        {
            aspectRatio.text = "4:3";
        }
        
        
    }
    public void LoadMenuLevel()
    {
        SceneManager.LoadScene(1);
    }
    public void ChangeAspect()
    {
       
    }
}
