using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip ShootingSound;
    public AudioClip GotHitSound;

    public static AudioController Instance;

    private void Awake()
    {
        Instance = this;
    }
}
