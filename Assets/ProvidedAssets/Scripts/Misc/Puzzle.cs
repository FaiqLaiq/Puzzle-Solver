using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

public class Puzzle : MonoBehaviour
{
    public delegate void LevelProgressChanged(int progress, int max);
    public static event LevelProgressChanged OnLevelProgressChanged;

    public delegate void LevelLoaded(int level, string type);
    public static event LevelLoaded OnLevelLoaded;

    public bool isReady;

    public int LevelNo;
    public string type;

    public float captureDistance;
    public List<PuzzlePiece> puzzlePieces;

    List<Vector3> positions;
    int unsortedPiecesCount;
    int sortedPiecesCount;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Init(LevelDifficulty levelDifficulty, Material levelMat)
    {
        bool initializePositions = positions == null;

        captureDistance = levelDifficulty.captureDistance;

        if (puzzlePieces != null)
        {
            if (initializePositions)
                positions = new(puzzlePieces.Count);

            transform.GetChild(0).GetComponent<MeshRenderer>().material = levelMat;

            for (int i = 0; i < puzzlePieces.Count; i++)
            {
                puzzlePieces[i].SetMaterial(levelMat);
                puzzlePieces[i].enabled = true;
                puzzlePieces[i].orignalID = i;
                if (initializePositions)
                    positions.Add(puzzlePieces[i].transform.position);
            }

            List<int> typesIndex = new(levelDifficulty.Count);
            for (int i = 0; i < levelDifficulty.Count; i++) typesIndex.Add(0);

            for (int i = 0; i < puzzlePieces.Count; i++)
            {
                int type = puzzlePieces[i].type;
                puzzlePieces[i].currentID = levelDifficulty.Get(type, typesIndex[type]++);
                if (!puzzlePieces[i].PlacePieceOn(positions[puzzlePieces[i].currentID], puzzlePieces[i].currentID))
                    unsortedPiecesCount++;
            }
            sortedPiecesCount = 0;
        }

        OnLevelLoaded?.Invoke(LevelNo, type);
        OnLevelProgressChanged?.Invoke(sortedPiecesCount, unsortedPiecesCount);
    }

    public void AddPiece(PuzzlePiece piece)
    {
        if (puzzlePieces == null)
            puzzlePieces = new List<PuzzlePiece>();

        puzzlePieces.Add(piece);
    }

    public void OnPieceDragging(PuzzlePiece piece)
    {
        if (piece.HasTarget)
        {
            float distance = Vector2.Distance(piece.transform.position, piece.Target.transform.position);
            //Debug.Log("Distance HasTarget: " + distance);

            if (distance > captureDistance)
            {
                piece.Target.Highlight(false);
                piece.SetTargetPiece(null);
            }
        }
        else
        {
            for (int i = 0; i < puzzlePieces.Count; i++)
            {
                if (puzzlePieces[i].currentID == piece.currentID || !puzzlePieces[i].enabled) continue;
                float distance = Vector2.Distance(piece.transform.position, puzzlePieces[i].transform.position);
                //Debug.Log(puzzlePieces[i].name + " Distance: " + distance);
                if (distance <= captureDistance && piece.type == puzzlePieces[i].type)
                {
                    piece.SetTargetPiece(puzzlePieces[i]);
                    piece.Target.Highlight(true);
                    break;
                }
            }
        }
    }

    public void OnPieceDraggingEnd(PuzzlePiece piece)
    {
        if (piece.HasTarget)
        {
            int id = piece.currentID;

            if (piece.PlacePieceOn(piece.Target.transform.position, piece.Target.currentID, true))
                sortedPiecesCount++;
            if (piece.Target.PlacePieceOn(piece.startPos, id, true))
                sortedPiecesCount++;

            piece.Target.Highlight(false);
            piece.SetTargetPiece(null);

            OnLevelProgressChanged?.Invoke(sortedPiecesCount, unsortedPiecesCount);
        }
        else
        {
            piece.PlacePieceOn(piece.startPos, piece.currentID, true);
        }
    }
}
