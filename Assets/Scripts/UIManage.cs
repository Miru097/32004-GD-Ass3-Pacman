using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadPacmanLevel()
    {
        DontDestroyOnLoad(this);
        SceneManager.LoadScene(0);
    }
    public void LoadDesignLevel()
    {
        DontDestroyOnLoad(this);
        SceneManager.LoadScene(2);
    }
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

    }
    }
