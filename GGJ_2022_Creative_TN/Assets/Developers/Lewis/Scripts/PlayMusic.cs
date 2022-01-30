using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    public AudioClip music;
    // Start is called before the first frame update
    void Start()
    {
        SoundManager._Instance.PlayMusic(music);
    }

}
