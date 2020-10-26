using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    BoardSlot highlightedSlot;
    private void OnEnable()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            highlightedSlot = hit.collider.GetComponent<BoardSlot>();
            highlightedSlot.Highlight(true);
        }
    }
    private void OnDisable()
    {
        if(highlightedSlot != null)
        highlightedSlot.Highlight(false);
    }
}
