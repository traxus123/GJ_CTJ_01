using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : Enemy
{
    public Transform Player;
    public Transform PFollower;
    public float Cooldown = 5.0f;
    public float speed = 5.0f;

    private Vector3 destination;
    private GameObject Figure;
    private Queue<Vector3> mouvmentCharacter = new Queue<Vector3>();
    private Queue<Vector3> mouvmentFigure = new Queue<Vector3>();
    
    protected override void Awake()
    {
        base.Awake();

        destination = Player.position;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        Queue<Vector3> mouvement = mouvmentCharacter;

        Figure = GameObject.FindGameObjectWithTag("Figure");
        if (Figure != null)
            mouvement = mouvmentFigure;
        
        float speedX = speed;
        if (Slow)
            speedX /= 2.0f;

        if (Figure == null)
            mouvement.Enqueue(Player.position);
        else
            mouvement.Enqueue(Figure.transform.position);

        if (Cooldown > 0.0f)
        {
            Cooldown -= Time.deltaTime;
        }
        else
        {
            if (!Stun)
            {
                transform.position = Vector3.MoveTowards(transform.position, destination, speedX * Time.deltaTime);
                if (Vector3.Distance(transform.position, destination) < 0.1f)
                {
                    destination = mouvement.Peek();
                    mouvement.Dequeue();
                }
            }
        }
    }
}
