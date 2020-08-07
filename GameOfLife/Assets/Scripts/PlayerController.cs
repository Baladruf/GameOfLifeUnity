using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private GridCellMaker grid;

    [SerializeField]
    private GameOfLife gameOfLife;

    [SerializeField]
    private float speed = 5f;

    void Start()
    {
        float x = grid.WIDTH * grid.OFFSET * 0.5f - 0.5f;
        float y = grid.HEIGHT * grid.OFFSET * 0.5f - 0.5f;

        transform.position = new Vector3(x, y, -10f);
    }

    void Update()
    {
        Movement();
        Randomize();
        Play();
        ResetCounterText();
    }

    private void Movement()
    {
        Vector3 _move = new Vector3(
            Input.GetAxis("Horizontal") * speed,
            Input.GetAxis("Vertical") * speed,
            0f
            );

        transform.Translate(_move);
        cam.orthographicSize += Input.GetAxis("Zoom") * speed;
    }

    private void Play()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            gameOfLife.Play();
        }
    }

    private void Randomize()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            grid.RandomizeGrid();
        }
    }

    private void ResetCounterText()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            InfoUI.Instance.ResetTurn();
        }
    }
}
