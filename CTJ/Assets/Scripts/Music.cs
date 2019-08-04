using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioClip introMusic;
    public AudioClip loopMusic;
    public AudioSource music;
    // Start is called before the first frame update
    void Start()
    {
        music = GetComponent<AudioSource>();
        music.clip = introMusic;
        music.Play(0);


    }

    // Update is called once per frame
    void Update()
    {
        if (!music.isPlaying)
        {
            music.clip = loopMusic;
            music.Play();
            music.loop = true;
        }
    }
}
