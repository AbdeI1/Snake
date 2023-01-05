using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeTailController : MonoBehaviour
{

    private new Transform transform;
    private int dir;
    private int cooldown;
    private Queue<int> moves;

    SnakeGlobals g;

    // Start is called before the first frame update
    void Start()
    {
        g = GameObject.Find("Snake").GetComponent<SnakeGlobals>();
        transform = GetComponent("Transform").transform;
        dir = (int)g.StartingDirection;
        cooldown = 0;
        moves = new Queue<int>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!g.Paused)
        {
            int tdir = -1;
            if (Input.GetKeyDown("up"))
            {
                dir = 0;
            }
            else if (Input.GetKeyDown("right"))
            {
                dir = 1;
            }
            else if (Input.GetKeyDown("down"))
            {
                dir = 2;
            }
            else if (Input.GetKeyDown("left"))
            {
                dir = 3;
            }
            if (cooldown > (g.Speed == 0 ? 10000000000 : 180 / g.Speed))
            {
                moves.Enqueue(dir);
                if (moves.Count > g.Length)
                {
                    tdir = moves.Dequeue();
                    transform.Translate(g.Directions[tdir]);
                }
                cooldown = 0;
            }
            cooldown++;
            if (transform.position.x > g.BottomRight.x || transform.position.x < g.TopLeft.x || transform.position.y > g.TopLeft.y || transform.position.y < g.BottomRight.y)
            {
                if (tdir != -1)
                {
                    transform.Translate(g.Directions[(tdir + 2) % 4]);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SnakeBody")
        {
            Destroy(other.gameObject);
        }
    }

}
