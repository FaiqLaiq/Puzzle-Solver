using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelDifficulty
{
    public int Count => levelData.Count;
    public float captureDistance;

    [SerializeField] List<PieceType> levelData = new();

    public void Add(int type, int pieceIndex)
    {
        if (levelData.Count <= type)
        {
            for (int i = levelData.Count; i <= type; i++)
            {
                levelData.Add(new());
            }
        }

        levelData[type].Add(pieceIndex);
    }

    public PieceType Get(int index)
    {
        return levelData[index];
    }

    public int Get(int type, int index)
    {
        return levelData[type].Get(index);
    }
}
