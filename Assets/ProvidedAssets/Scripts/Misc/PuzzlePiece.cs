using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    public int type;
    [Tooltip("Position on which this piece currently is. Set it according to cloning game")]
    public int currentID;
    [Tooltip("Position on which this piece should be")]
    [HideInInspector] public int orignalID;
    [Tooltip("Add this offset to dragged piece, so that it must be seen on top of everything")]
    public float zOffset = -0.01f;
    
    [HideInInspector] public Vector3 startPos;

    // Private Variables
    bool isDragging = false;

    Vector3 offset;

    PuzzlePiece target;
    Puzzle puzzle;
    Puzzle Puzzle
    {
        get
        {
            if (puzzle == null)
                puzzle = transform.parent.GetComponent<Puzzle>();
            
            return puzzle;
        }
    }

    // Properties
    public bool HasTarget => target != null;
    public PuzzlePiece Target => target;

    public void SetMaterial(Material mat)
    {
        GetComponent<MeshRenderer>().material = mat;
    }

    public void SetTargetPiece(PuzzlePiece target)
    {
        this.target = target;
    }

    public bool PlacePieceOn(Vector3 position, int ID, bool smooth = false)
    {
        if (smooth)
            TweenWrapper.PositionByTime(gameObject, transform.position, position, 0.3f);
        else
            transform.position = position;

        currentID = ID;

        if (currentID == orignalID)
            enabled = false;

        return !enabled;
    }

    public void Highlight(bool highlight)
    {
        TweenWrapper.Stop(gameObject, TweenWrapper.TweenType.Scale);

        Debug.Log("Highlight: " + gameObject.name + " -> " + highlight);
        if (highlight)
            TweenWrapper.ScaleBySpeed(gameObject, transform.localScale, Vector3.one * 0.9f, 2);
        else
            transform.localScale = Vector3.one;
    }

    void OnMouseDown()
    {
        startPos = transform.position;
        isDragging = true;
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
    }

    void OnMouseUp()
    {
        isDragging = false;
        Puzzle.OnPieceDraggingEnd(this);
        //transform.position = startPos;
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f));
            transform.position = new Vector3(mousePosition.x + offset.x, mousePosition.y + offset.y, zOffset);
            Puzzle.OnPieceDragging(this);
        }
    }
}
