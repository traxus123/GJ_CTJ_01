using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Music : MonoBehaviour
{
    public AudioClip introMusic;
    public AudioClip loopMusic;
    public AudioClip lastLevel;

    public bool islastLevel;
    
    static public AudioSource music;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find(name) != gameObject)
            Destroy(gameObject);

        DontDestroyOnLoad(this);

        music = GetComponent<AudioSource>();
        if (music != null)
        {
            if (islastLevel)
                music.clip = lastLevel;
            else
                music.clip = introMusic;

            music.volume = 0.3f;
            music.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (music != null && !music.isPlaying)
        {
            if (islastLevel)
                music.clip = lastLevel;
            else
                music.clip = loopMusic;

            music.Play();
            music.volume = 0.3f;
            music.loop = true;
        }
    }
}
