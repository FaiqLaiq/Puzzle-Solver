using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Scriptable Objects/Level")]
public class LevelScriptableObject : ScriptableObject
{
    public List<LevelDifficulty> difficulties = new();

    public void Add(int difficultyIndex, int type, int pieceIndex)
    {
        if (difficulties.Count <= difficultyIndex)
        {
            for (int i = difficulties.Count; i <= difficultyIndex; i++)
            {
                difficulties.Add(new());
            }
        }

        difficulties[difficultyIndex].Add(type, pieceIndex);
    }

    public LevelDifficulty Get(int index)
    {
        return difficulties[index];
    }
}
