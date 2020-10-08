using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tweener : MonoBehaviour
{
    private Tween activeTween;
    //private List<Tween> activeTweens = new List<Tween>();
    // Update is called once per frame
    void Update()
    {
        if (activeTween != null)
        {
            if (Vector3.Distance(activeTween.Target.position, activeTween.EndPos) > 0.1f)
            {
                float timeFraction = (Time.time - activeTween.StartTime) / activeTween.Duration;
                activeTween.Target.position = Vector3.Lerp(activeTween.StartPos, activeTween.EndPos, timeFraction);
                //activeTween.Target.position = activeTween.StartPos + activeTween.EndPos * timeFraction * timeFraction * timeFraction;
            }
            else
            {
                activeTween.Target.position = activeTween.EndPos;
                activeTween = null;
            }
        }
    }
    public void AddTween(Transform targetObject, Vector3 startPos, Vector3 endPos, float duration)
    {

        if (activeTween == null)
        {
            activeTween = new Tween(targetObject, startPos, endPos, Time.time, duration);
        }
        else
        {
            activeTween = new Tween(targetObject, startPos, endPos, Time.time, duration);
        }
    }
}

