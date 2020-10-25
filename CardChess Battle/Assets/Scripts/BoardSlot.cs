using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSlot : MonoBehaviour
{
    public ChessPiece pieceOnTop;
    public Card cardEffect;

    [HideInInspector] public ChessPiece hypoteticalPieceOnTop;
}
