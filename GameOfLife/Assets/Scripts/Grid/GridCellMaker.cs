using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCellMaker : RandomizeGrid
{
    [SerializeField]
    private CellData cellPrefab;

    [SerializeField]
    private Transform cellHolder;

    [SerializeField]
    private int width = 30, height = 30;

    [SerializeField]
    private float offsetCell = 1.1f;

    [SerializeField]
    private bool randomInitialize = false;

    [SerializeField, Range(0f, 1f)]
    private float percentLuck = 0.3f;

    private CellData[] gridCell; // x + ( y * width )
    public int WIDTH { get { return width; } }
    public int HEIGHT { get { return height; } }
    public float OFFSET { get { return offsetCell; } }

    void Awake()
    {
        gridCell = new CellData[width * height];
        for(int y = 0; y < height; y++)
        {
            for(int x = 0; x < width; x++)
            {
                Vector3 _pos = new Vector3(x * offsetCell, y * offsetCell, 0f);
                gridCell[x + (y * width)] = Instantiate<CellData>(cellPrefab, _pos, Quaternion.identity, cellHolder);
                gridCell[x + (y * width)].name = "P" + x + "-" + y;
                if (randomInitialize)
                {
                    float _rand = Random.Range(0f, 1f);
                    gridCell[x + (y * width)].SetLife(_rand <= percentLuck);
                }
            }
        }
    }

    public override void Randomize()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float _rand = Random.Range(0f, 1f);
                gridCell[x + (y * width)].SetLife(_rand <= percentLuck);
            }
        }

        InfoUI.Instance.ResetTurn();
    }

    public CellData GetCell(int x, int y)
    {
        return gridCell[x + (y * width)];
    }
}
