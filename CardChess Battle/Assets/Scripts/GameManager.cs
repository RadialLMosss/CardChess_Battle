using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<ChessPiece> aiPieces = new List<ChessPiece>();
    public List<ChessPiece> otherPieces = new List<ChessPiece>();


    private ChessPiece selectedPiece;

    private void Update()
    {
        // Mouse
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if(hit.collider.GetComponent<ChessPiece>() && hit.collider.GetComponent<ChessPiece>().color == otherPieces[0].color)
                {
                    if (selectedPiece != null)
                    {
                        selectedPiece.Deselect();
                    }
                    selectedPiece = hit.collider.GetComponent<ChessPiece>();
                    selectedPiece.Select();
                }

                else if(hit.collider.GetComponent<BoardSlot>() && hit.collider.GetComponent<BoardSlot>().isHighlighted)
                {
                    if(selectedPiece != null)
                    {
                        selectedPiece.Move(hit.collider.GetComponent<BoardSlot>());
                        selectedPiece.Deselect();
                        selectedPiece = null;
                    }
                }
            }
        }

        // Touch
        /*
        if (Input.GetTouch(1).phase == TouchPhase.Began)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(1).position);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {

            }
        }*/
    }
}
