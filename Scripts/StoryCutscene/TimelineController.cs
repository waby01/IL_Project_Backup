using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineController : MonoBehaviour
{
    public PlayableDirector director;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (director.state == PlayState.Paused)
                director.Play();
            else
                director.Pause();
        }
    }
}