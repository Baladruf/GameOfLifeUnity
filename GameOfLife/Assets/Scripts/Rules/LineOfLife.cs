using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfLife : PlayRules
{
    private LineCellMaker lineMaker;

    [SerializeField]
    private float delayTurn = 0.3f;
    private float timer;

    [SerializeField]
    private int setup = 0;

    private bool canPlay = false;
    [SerializeField]
    private bool debug;

    private bool[] cellState;

    private void Awake()
    {
        lineMaker = FindObjectOfType<LineCellMaker>();
        timer = delayTurn;
        cellState = new bool[lineMaker.WIDTH];

        /*
        int test = 0;

        //first
        test += 1;

        //second
        test = test << 1;
        test += 1;

        //third
        test = test << 1;
        test += 1;

        Debug.Log("res = "+test);

        // var bit = (b & (1 << bitNumber-1)) != 0;
        */
    }

    void Update()
    {
        if (!canPlay)
            return;

        if (timer > 0)
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

    private void CalculateGameOfLife()
    {
        CellData[] _cellTab = lineMaker.currentTurn;
        for(int i = 0; i < _cellTab.Length; i++)
        {
            int _state = 0;
            for(int j = -1; j <= 1; j++)
            {
                _state = _state << 1;

                if(i + j == -1 || i + j == _cellTab.Length)
                {
                    continue;
                }

                if(_cellTab[i + j].isAlive)
                {
                    _state++;
                }
            }
            cellState[i] = ApplyRules(_state);
        }
    }

    private bool ApplyRules(int _value)
    {
        Debug.Assert(_value >= 0);
        Debug.Assert(_value < 8);

        bool res = (setup & (1 << _value)) != 0;
        return res;
    }

    private void ApplyLife()
    {
        lineMaker.CreateCellLine();

        CellData[] _cellTab = lineMaker.currentTurn;
        for (int i = 0; i < _cellTab.Length; i++)
        {
            _cellTab[i].SetLife(cellState[i]);
        }
    }

    public override void Play()
    {
        canPlay = !canPlay;
        InfoUI.Instance.SetStateGame(canPlay);
    }
}
