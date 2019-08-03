using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public Transform Player;
    public Transform PFollower;
    public int Time = 300;

    private Queue<Vector3> mouvment = new Queue<Vector3>();

    // Update is called once per frame
    void Update()
    {
        Debug.Log("m_Grounded " + Player.position);
        mouvment.Enqueue(Player.position);
        if(mouvment.Count >= Time){
            PFollower.position = mouvment.Peek();
            mouvment.Dequeue();
        }
    }
}
