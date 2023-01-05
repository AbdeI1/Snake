using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    private new Transform transform;

    private int dir;
    private int cooldown;

    SnakeGlobals g;

    // Start is called before the first frame update
    void Start()
    {
        Random.InitState(1);
        g = GameObject.Find("Snake").GetComponent<SnakeGlobals>();
        transform = GetComponent("Transform").transform;
        dir = (int)g.StartingDirection;
        cooldown = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!g.Paused)
        {
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
                GameObject body = Instantiate(g.SnakeBody, transform.position, Quaternion.identity);
                body.transform.parent = transform.parent;
                transform.Translate(g.Directions[dir]);
                cooldown = 0;
            }
            cooldown++;
            if (transform.position.x > g.BottomRight.x || transform.position.x < g.TopLeft.x || transform.position.y > g.TopLeft.y || transform.position.y < g.BottomRight.y)
            {
                transform.Translate(g.Directions[(dir + 2) % 4]);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SnakeBody")
        {
            Debug.Log("hit body");
        }
        else if (other.gameObject.tag == "Apple")
        {
            Destroy(other.gameObject);
            g.Length += g.AppleCalories;
            g.Score++;
            SpawnNewApple();
        }
    }

    private void SpawnNewApple()
    {
        int randx = Random.Range(g.TopLeft.x, g.BottomRight.x + 1);
        int randy = Random.Range(g.BottomRight.y, g.TopLeft.y + 1);
        int i = 0;
        while(CollidesWithSnakeBody(Physics.OverlapSphere(new Vector3(randx, randy, -0.01f), 1f)) && i < 100)
        {
            randx = Random.Range(g.TopLeft.x, g.BottomRight.x + 1);
            randy = Random.Range(g.BottomRight.y, g.TopLeft.y + 1);
            i++;
        }
        Instantiate(g.Apple, new Vector3(randx, randy, -0.01f), Quaternion.identity);
    }

    private bool CollidesWithSnakeBody(Collider[] cols)
    {
        foreach (Collider c in cols) 
        {
            if (c.gameObject.tag == "SnakeBody")
            {
                return true;
            }
        }
        return false;
    }
}
