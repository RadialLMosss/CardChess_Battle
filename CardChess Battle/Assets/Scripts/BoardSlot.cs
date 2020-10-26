using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSlot : MonoBehaviour
{
    public ChessPiece pieceOnTop;
    public Card cardEffect;
    public bool isHighlighted;

    [HideInInspector] public ChessPiece hypoteticalPieceOnTop;

    public void Highlight(bool shouldTurnOn)
    {
        GetComponent<SpriteRenderer>().enabled = shouldTurnOn;
        isHighlighted = shouldTurnOn;
    }
}
