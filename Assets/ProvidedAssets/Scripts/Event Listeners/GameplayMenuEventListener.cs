using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayMenuEventListener : MonoBehaviour
{

    public TMP_Text levelTitleTxt;
    public Image levelProgressFill;
    public TMP_Text levelTypeTxt;


    // Start is called before the first frame update
    void Awake()
    {
        Puzzle.OnLevelProgressChanged += HandleLevelProgressChanged;
        Puzzle.OnLevelLoaded += HandleLevelLoaded;
    }

    void HandleLevelProgressChanged(int progess, int max)
    {
        levelProgressFill.fillAmount = (float)progess / max;
    }

    void HandleLevelLoaded(int level, string type)
    {
        
        levelTitleTxt.text = $"LEVEL {level}";
        levelTypeTxt.text = type;
    }
}
