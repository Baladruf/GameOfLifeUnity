using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private RandomizeGrid gridRandomize;

    [SerializeField]
    private PlayRules rulesPlay;

    [SerializeField]
    private float speed = 5f;

    void Start()
    {

        if(!(gridRandomize is GridCellMaker))
        {
            return;
        }

        GridCellMaker _grid = (GridCellMaker)gridRandomize;
        float x = _grid.WIDTH * _grid.OFFSET * 0.5f - 0.5f;
        float y = _grid.HEIGHT * _grid.OFFSET * 0.5f - 0.5f;

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
            rulesPlay.Play();
        }
    }

    private void Randomize()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            gridRandomize.Randomize();
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
