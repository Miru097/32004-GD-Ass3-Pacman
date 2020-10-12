using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tweeners : MonoBehaviour
{
    
    private List<Tween> activeTweens = new List<Tween>();
    void Update()
    {
       for(int i = 0; i < activeTweens.Count; i++)
        {
            if (Vector3.Distance(activeTweens[i].Target.position, activeTweens[i].EndPos) > 0.1f)
            {
                float timeFraction = (Time.time - activeTweens[i].StartTime) / activeTweens[i].Duration;
                activeTweens[i].Target.position = Vector3.Lerp(activeTweens[i].StartPos, activeTweens[i].EndPos, timeFraction);
            }
            else
            {
                activeTweens[i].Target.position = activeTweens[i].EndPos;
                //activeTweens.Remove(activeTweens[i]);
                activeTweens.RemoveAt(i);
            }
        }
        /*
        foreach(Tween t in activeTweens)
        {
            if (Vector3.Distance(t.Target.position, t.EndPos) > 0.1f)
            {
                float timeFraction = (Time.time - t.StartTime) / t.Duration;
                t.Target.position = Vector3.Lerp(t.StartPos, t.EndPos, timeFraction);

            }
            else
            {
                t.Target.position = t.EndPos;
                activeTweens.Remove(t);
            }
        }
        */
    }

    public bool TweenExists(Transform target)
    {
        foreach(Tween t in activeTweens)
        {
            if (t.Target == target)
                return true;
        }
        return false;
    }
    public bool AddTween(Transform targetObject, Vector3 startPos, Vector3 endPos, float duration)
    {
        if (TweenExists(targetObject) == true)
        {
            return false;
        }
        else{
            Tween newTarget = new Tween(targetObject, startPos, endPos, Time.time, duration);
            activeTweens.Add(newTarget);
            return true;
            
        }
    }
}
