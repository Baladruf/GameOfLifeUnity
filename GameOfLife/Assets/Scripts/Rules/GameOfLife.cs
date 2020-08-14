using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOfLife : PlayRules
{
    [SerializeField]
    private float delayTurn = 0.3f;
    private float timer;

    [SerializeField]
    private GridCellMaker grid;

    private int width, height;
    private int[] neighbourhCell;
    private bool canPlay = false;

    [SerializeField]
    private bool debug = false;

    void Start()
    {
        timer = delayTurn;
        width = grid.WIDTH;
        height = grid.HEIGHT;
        neighbourhCell = new int[width * height];
    }

    // Update is called once per frame
    void Update()
    {
        if (!canPlay)
            return;

        if(timer > 0)
        {
            timer -= Time.deltaTime;
            return;
        }

        timer = delayTurn;
        CalculateGameOfLife();
        ApplyLife();
        InfoUI.Instance.IncrementTurn();

        if (debug)
            canPlay = false;
    }

    public override void Play()
    {
        canPlay = !canPlay;
        InfoUI.Instance.SetStateGame(canPlay);
    }

    private void CalculateGameOfLife()
    {
        for(int y = 0; y < height; y++) // y with j
        {
            for(int x = 0; x < width; x++) // x with i
            {
                CellData _cell = grid.GetCell(x, y);
                neighbourhCell[x + (y * width)] = 0;

                for(int j = -1; j <= 1; j++)
                {
                    for (int i = -1; i <= 1; i++)
                    {
                        if (PreventError(x, y, i, j))
                            continue;
                        if(grid.GetCell(x + i, y + j).isAlive)
                        {
                            neighbourhCell[x + (y * width)]++;
                        }
                    }
                }
            }
        }
    }

    private void ApplyLife()
    {
        for (int y = 0; y < height; y++) // y with j
        {
            for (int x = 0; x < width; x++) // x with i
            {
                Rules(grid.GetCell(x, y), neighbourhCell[x + (y * width)]);
            }
        }
    }

    private bool PreventError(int _x, int _y, int _i, int _j)
    {
        if(_i == 0 && _j == 0)
        {
            return true;
        }

        if(_x + _i < 0 || _y + _j < 0)
        {
            return true;
        }

        if (_x + _i >= width || _y + _j >= height)
        {
            return true;
        }

        return false;
    }

    private void Rules(CellData _cell, int _neighbourgCount)
    {
        if (_cell.isAlive)
        {
            if(!(_neighbourgCount == 2 || _neighbourgCount == 3))
            {
                _cell.SetLife(false);
                if (debug)
                {
                    Debug.Log(_cell.name + " : is dead");
                }
            }
        }
        else
        {
            if (_neighbourgCount == 3)
            {
                _cell.SetLife(true);
                if (debug)
                {
                    Debug.Log(_cell.name + " : is born");
                }
            }
        }
    }
}
