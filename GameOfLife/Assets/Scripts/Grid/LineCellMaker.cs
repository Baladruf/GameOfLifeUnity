using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCellMaker : RandomizeGrid
{
    [SerializeField]
    private CellData cellPrefab;

    [SerializeField]
    private Transform cellHolder;

    [SerializeField]
    private int width = 100; 
    public int WIDTH { get { return width; } }

    [SerializeField]
    private float offet = 1.1f;

    [SerializeField, Range(0f, 1f)]
    private float percentAlea = 0.3f;

    [SerializeField]
    private bool startAlea = false;
    [SerializeField]
    private bool startMiddle = true;

    public CellData[] currentTurn { get; private set; }

    private int turn = 0;

    void Awake()
    {
        CreateCellLine();
        if (startAlea)
            Randomize();
        else if (startMiddle)
        {
            float _middle = ((float)width) * 0.5f;
            currentTurn[Mathf.CeilToInt(_middle)].SetLife(true);
        }
    }

    public void CreateCellLine()
    {
        currentTurn = new CellData[width];
        Transform _holder = Instantiate<Transform>(cellHolder);
        _holder.name = "T" + turn;
        for (int i = 0; i < width; i++)
        {
            Vector3 _pos = new Vector3((i * offet) - (width * 0.5f), turn * offet *-1f, 0f);
            currentTurn[i] = Instantiate<CellData>(cellPrefab, _pos, Quaternion.identity, _holder);
            currentTurn[i].name = "C" + i;
        }
        turn++;
    }

    public override void Randomize()
    {
        for(int i = 0; i < width; i++)
        {
            currentTurn[i].SetLife( Random.Range(0f, 1f)  <= percentAlea );
        }
    }
}
