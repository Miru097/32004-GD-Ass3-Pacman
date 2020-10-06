using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Border : MonoBehaviour
{
    private Image borderImage = default;
    public Sprite[] borderSprite = default;
    private float borderTimer=0;
    private float borderTime=0.25f;
    int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        borderImage = GameObject.FindGameObjectWithTag("border").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (borderTimer > 0)
            borderTimer -= Time.deltaTime;
        if (borderTimer < 0)
            borderTimer = 0;
        if (borderTimer == 0)
        {
            borderChange();
            borderTimer = borderTime;
        }
    }
    public void borderChange()
    {
        if (i < 4)
        {
            borderImage.sprite = borderSprite[i];
            i++;
        }
        else
        {
            i = 0;
            borderImage.sprite = borderSprite[i];
            i++;
        }
    }
}
