using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnakeGlobals : MonoBehaviour
{
    public Vector2Int TopLeft;
    public Vector2Int BottomRight;

    public double Speed;
    public int Length;
    public int Score;
    public int AppleCalories;

    public bool Paused;

    public enum Dir
    {
        up,
        right,
        down,
        left
    }
    public Dir StartingDirection;

    public readonly Dictionary<int, Vector3> Directions = new Dictionary<int, Vector3>()
    { {0, new Vector3(0, 1, 0)},
      {1, new Vector3(1, 0, 0)},
      {2, new Vector3(0, -1, 0)},
      {3, new Vector3(-1, 0, 0)}
    };

    public GameObject SnakeBody;
    public GameObject Apple;

    public Text ScoreText;
    public Text LengthText;

    private void Start()
    {
        AppleCalories = 2;
    }

    private void Update()
    {
        ScoreText.text = "Score   : " + Score;
        LengthText.text = "Length : " + Length;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Paused = !Paused;
        }
    }
}
