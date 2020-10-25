using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPiece : MonoBehaviour
{
    public enum PieceColor { WHITE, BLACK };

    public PieceColor color;
    public float healthValue;
    public int pieceValue;

    // Realtime Variables
    [HideInInspector] public List<BoardSlot> possibleMoves;
    [HideInInspector] public BoardSlot position;

    // Hypotetical Variables
    [HideInInspector] public List<BoardSlot> hypoteticalPossibleMoves;
    [HideInInspector] public BoardSlot hypoteticalPosition;
    [HideInInspector] public BoardSlot hypoteticalBestMove;
    [HideInInspector] public int hypoteticalPointsToGet;
    [HideInInspector] public int hypoteticalOverallPontuation;


    public void Move()
    {
        position = hypoteticalBestMove;
        position.pieceOnTop = this;

        // update possible moves

        // change turn
    }

    public void SimulatedMove()
    {
        hypoteticalPosition = hypoteticalBestMove;
        hypoteticalPosition.hypoteticalPieceOnTop = this;

        // update hypotetical possible moves
    }
}
