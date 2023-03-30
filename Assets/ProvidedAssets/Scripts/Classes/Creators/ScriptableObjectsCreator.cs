using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ScriptableObjectsCreator
{
    public void Edit(LevelScriptableObject level, int difficultyLevel, List<string> puzzlePiecesNames)
    {
        for (int i = 0; i < puzzlePiecesNames.Count; i++)
        {
            int type = int.Parse(puzzlePiecesNames[i].Split('-')[2]);
            level.Add(difficultyLevel, type, i);
        }

        for (int i = 0; i < level.Get(difficultyLevel).Count; i++)
        {
            level.Get(difficultyLevel).Get(i).PerfectShuffle();
        }
    }

    public void Save(LevelScriptableObject level, string path)
    {
        AssetDatabase.CreateAsset(level, path);
    }
}
