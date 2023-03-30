using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsManager : MonoBehaviour
{
    [Header("Debug Settings")]
    public bool debug;
    public int debugLevelIndex;
    public int debugDifficultyLevel;

    [Header("References")]
    public Transform levelsContainer;
    public Material levelMat;

    [Header("Levels Settings")]
    public GameObject[] levelsPrefab;
    public LevelScriptableObject[] levelsData;
    public Texture[] levelsTexture;

    private void Start()
    {
        if (debug)
        {
            Puzzle puzzle = Instantiate(levelsPrefab[debugLevelIndex]).GetComponent<Puzzle>();
            levelMat.mainTexture = levelsTexture[debugLevelIndex];
            puzzle.Init(levelsData[debugLevelIndex].Get(debugDifficultyLevel), levelMat);
            puzzle.transform.SetParent(levelsContainer);
        }
    }
}
