using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLeveled : MonoBehaviour
{
    public string sceneToLoad;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Character")
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
