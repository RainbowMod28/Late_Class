using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandler : MonoBehaviour
{
    private AudioSource[] mysounds;

    private AudioSource walk;
    private AudioSource run;

    void Start()
    {
        mysounds = GetComponents <AudioSource>();

        walk = mysounds[0];
        run = mysounds[1];
    }

    public void PlayRun()
    {
        run.Play();
    }

    public void PlayWalk()
    {
        walk.Play();
    }


}
