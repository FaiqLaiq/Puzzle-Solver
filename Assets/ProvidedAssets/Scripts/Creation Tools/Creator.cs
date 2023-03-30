using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class Creator : MonoBehaviour
{
    [Header("Level Limit Settings")]
    public int levelStartNo = 1;
    public int levelEndNo = 1;

    [Header("Path Settings")]
    public string basicPath = "Assets/ProvidedAssets/";
    [Tooltip("Must be path from Resources Folder")]
    public string modelPath = "Levels/Level";
    public string prefabPath = "Assets/ProvidedAssets/Prefabs/";
    public string materialPath = "Assets/ProvidedAssets/Art/Resources/Levels/Level";
    public string jsonPath = "Assets/ProvidedAssets/Json/levelsData.json";
    public string scriptableObjectsPath = "Assets/ProvidedAssets/Json/levelsData.json";

    [Header("Infix Settings")]
    public InFix modelInfix;
    public InFix prefabInfix;
    public InFix textureInfix;
    public InFix materialInfix;
    public InFix scriptableObjectInfix;

    [Space]
    public string[] difficultyLevels = { "Easy", "Medium", "Hard" };


    PrefabCreator prefabCreator = new();
    MaterialCreator materialCreator = new();
    //JsonCreator jsonCreator;
    ScriptableObjectsCreator scriptableObjectsCreator = new();

    string ModelPath => modelPath;
    string PrefabPath => basicPath + prefabPath;
    string MaterialPath => basicPath + materialPath;
    string JsonPath => basicPath + jsonPath;
    string ScriptableObjectPath => basicPath + scriptableObjectsPath;

    private void Start()
    {
        //jsonCreator = new(JsonPath);

        StartCreation();
    }


    void StartCreation()
    {
        for (int i = levelStartNo; i <= levelEndNo; i++)
        {
            LevelScriptableObject level = ScriptableObject.CreateInstance<LevelScriptableObject>();

            for (int j = 0; j < difficultyLevels.Length; j++)
            {
                string texturePath = $"{ModelPath}{i}/{textureInfix.Get(i.ToString())}";
                string materialPath = $"{MaterialPath}{i}/{materialInfix.Get(i.ToString())}";

                string modelPath = $"{ModelPath}{i}/{modelInfix.Get("_" + i + "-" + difficultyLevels[j])}";
                string prefabPath = PrefabPath + prefabInfix.Get("_" + i + "-" + difficultyLevels[j]);

                //Material material = materialCreator.CreateMat(texturePath, materialPath);
                List<string> childrenNames = prefabCreator.CreatePrefab(modelPath, prefabPath/*, material*/);

                if (childrenNames != null) scriptableObjectsCreator.Edit(level, j, childrenNames);
            }
            string scriptableObjectPath = ScriptableObjectPath + scriptableObjectInfix.Get(i.ToString());

            scriptableObjectsCreator.Save(level, scriptableObjectPath);
        }

        //jsonCreator.WriteToJson();
    }
}
