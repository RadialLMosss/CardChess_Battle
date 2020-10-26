using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    [SerializeField] int minMaxDepth = 2; // AI level of inteligence
    [SerializeField] GameManager gameManager = null;


    List<ChessPiece> simulatedAIPieces = new List<ChessPiece>();
    List<ChessPiece> simulatedOtherPieces = new List<ChessPiece>();
    int overallSimulatedPontuation;
    ChessPiece chosenPiece;
    List<int> overallSimulatedPontuations = new List<int>();
    List<ChessPiece> chosenPieces = new List<ChessPiece>();
    List<BoardSlot> boardSlots = new List<BoardSlot>();

    // call it when it is the AI's turn
    public void MinMax()
    {
        // Init/Reset all values
        simulatedAIPieces = gameManager.aiPieces;
        simulatedOtherPieces = gameManager.otherPieces;
        overallSimulatedPontuation = 0;
        overallSimulatedPontuations.Clear();
        chosenPieces.Clear();
        ResetHypoteticalValues();

        // Start Simulations
        for (int p = 0; p < simulatedAIPieces.Count; p++)
        {
            for (int i = 0; i < minMaxDepth; i++)
            {
                // simulate its own best move
                SimulateBestMove(simulatedAIPieces);
                if (i == 0)
                {
                    print("removing from last best evaluated piece from list");
                    simulatedAIPieces.Remove(chosenPiece);
                    chosenPieces.Add(chosenPiece);
                }
                // predict enemy best move
                SimulateBestMove(simulatedOtherPieces);
            }
            // add pontuation to the list so we can compare then later
            overallSimulatedPontuations.Add(overallSimulatedPontuation);
        }


        // Chose the best chosenPiece of them all by comparing the overall pontuations
        int maxPontuation = 0;
        ChessPiece bestChosenPiece = chosenPiece;
        for (int i = 0; i < overallSimulatedPontuations.Count; i++)
        {
            if(overallSimulatedPontuations[i] > maxPontuation)
            {
                maxPontuation = overallSimulatedPontuations[i];
                bestChosenPiece = chosenPieces[i];
            }
        }

        // Play the best simulated move
        bestChosenPiece.Move(bestChosenPiece.hypoteticalBestMove);
    }

    void SimulateBestMove(List<ChessPiece> pieces)
    {
        // see how many points each piece can get
        foreach (ChessPiece piece in pieces)
        {
            int pointsToGet = 0;

            // see each possible destination for a piece
            for (int i = 0; i < piece.hypoteticalPossibleMoves.Count; i++)
            {
                // see if there is an enemy on top of this piece's possible move
                if (piece.hypoteticalPossibleMoves[i].hypoteticalPieceOnTop != null)
                {
                    if (piece.hypoteticalPossibleMoves[i].hypoteticalPieceOnTop.color != piece.color)
                    {
                        // see if the chances of winning the battle are way too little
                        if(piece.hypoteticalPossibleMoves[i].hypoteticalPieceOnTop.healthValue <= piece.healthValue*1.5f)
                        {
                            // see how many points this piece can make by attacking
                            pointsToGet = piece.hypoteticalPossibleMoves[i].hypoteticalPieceOnTop.pieceValue;
                        }
                        else
                        {
                            // if chances of winning are low but the the lost will be little compared to the damaged dealt, then attack
                            if(piece.hypoteticalPossibleMoves[i].hypoteticalPieceOnTop.pieceValue >= piece.pieceValue*3)
                            {
                                // see how many points this piece can make by attacking
                                pointsToGet = piece.hypoteticalPossibleMoves[i].hypoteticalPieceOnTop.pieceValue/2;
                            }
                        }
                    }
                }
                
                // see how points will be affected because of a card
                if (piece.hypoteticalPossibleMoves[i].cardEffect != null)
                {
                    pointsToGet += piece.hypoteticalPossibleMoves[i].cardEffect.value;
                }

                // see if this play is better than a past evaluated one and update values if it is
                if (pointsToGet > piece.hypoteticalPointsToGet)
                {
                    piece.hypoteticalPointsToGet = pointsToGet;
                    piece.hypoteticalBestMove = piece.hypoteticalPossibleMoves[i];
                }

                // set best move if it's still null
                if (piece.hypoteticalBestMove == null)
                {
                    piece.hypoteticalBestMove = piece.hypoteticalPossibleMoves[i];
                }
            }
        }

        // knowing every piece's 'hypoteticalPointsToGet' and 'hypoteticalBestMove', we pick the best of them
        int maxPoint = 0;
        chosenPiece = pieces[0];
        foreach(ChessPiece piece in pieces)
        {
            if(piece.hypoteticalPointsToGet > maxPoint)
            {
                maxPoint = piece.hypoteticalPointsToGet;
                chosenPiece = piece;
            }
        }

        // update overallPontuation to compare later
        if(pieces[0].color == gameManager.aiPieces[0].color)
        {
            overallSimulatedPontuation += chosenPiece.hypoteticalPointsToGet;
        }
        else
        {
            overallSimulatedPontuation -= chosenPiece.hypoteticalPointsToGet;
        }

        // move the chosen piece to it's best move position
        chosenPiece.SimulatedMove();
    }

    void ResetHypoteticalValues()
    {
        foreach (BoardSlot slot in boardSlots)
        {
            slot.hypoteticalPieceOnTop = slot.pieceOnTop;
        }
        foreach (ChessPiece piece in simulatedAIPieces)
        {
            piece.hypoteticalPosition = piece.position;
            piece.hypoteticalBestMove = null;
            piece.hypoteticalOverallPontuation = 0;
            piece.hypoteticalPointsToGet = 0;
            piece.hypoteticalPossibleMoves = piece.possibleMoves;
        }
        foreach(ChessPiece piece in simulatedOtherPieces)
        {
            piece.hypoteticalPosition = piece.position;
            piece.hypoteticalBestMove = null;
            piece.hypoteticalOverallPontuation = 0;
            piece.hypoteticalPointsToGet = 0;
            piece.hypoteticalPossibleMoves = piece.possibleMoves;
        }
    }

}
