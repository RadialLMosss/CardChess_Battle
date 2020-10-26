using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPiece : MonoBehaviour
{
    public enum PieceColor { WHITE, BLACK };

    public PieceColor color;
    public float healthValue;
    public int pieceValue;

    [SerializeField] GameObject[] sensors = null;

    // Realtime Variables
    [HideInInspector] public List<BoardSlot> possibleMoves;
    [HideInInspector] public BoardSlot position;

    // Hypotetical Variables
    [HideInInspector] public List<BoardSlot> hypoteticalPossibleMoves;
    [HideInInspector] public BoardSlot hypoteticalPosition;
    [HideInInspector] public BoardSlot hypoteticalBestMove;
    [HideInInspector] public int hypoteticalPointsToGet;
    [HideInInspector] public int hypoteticalOverallPontuation;

    public void Move(BoardSlot newPos)
    {
        if(position != null)
        {
            position.pieceOnTop = null;
        }

        position = newPos;
        position.pieceOnTop = this;
        transform.position = new Vector3 (newPos.transform.position.x, newPos.transform.position.y, transform.position.z);
        // update possible moves

        // change turn
    }


    public void SimulatedMove()
    {
        hypoteticalPosition = hypoteticalBestMove;
        hypoteticalPosition.hypoteticalPieceOnTop = this;

        // update hypotetical possible moves
    }


    public void Deselect()
    {
        for (int i = 0; i < sensors.Length; i++)
        {
            sensors[i].SetActive(false);
        }
    }
    public void Select()
    {
        for (int i = 0; i < sensors.Length; i++)
        {
            sensors[i].SetActive(true);
        }
    }
}
