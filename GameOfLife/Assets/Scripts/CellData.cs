using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellData : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer sprite;

    [SerializeField]
    private Color ColorLife, ColorDead;

    public bool isAlive { get; private set; }

    public void SetLife(bool _isLife)
    {
        isAlive = _isLife;

        sprite.color = isAlive ? ColorLife : ColorDead;
    }

    private void OnMouseDown()
    {
        SetLife(!isAlive);
    }
}