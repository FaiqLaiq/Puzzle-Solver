using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PrefabCreator
{
    public List<string> CreatePrefab(string modelPath, string prefabPath/*, Material material*/)
    {
        GameObject model = Resources.Load<GameObject>(modelPath);

        if (model == null)
        {
            Debug.LogError("Failed to load model for " + modelPath);
            return null;
        }

        // Instantiate the model in the scene
        GameObject instance = Object.Instantiate(model);

        // Adjust scale and rotation
        instance.transform.localScale = Vector3.one * 16;
        instance.transform.localEulerAngles = new Vector3(0, 180, 0);

        // Attach the Puzzle script to the model
        Puzzle puzzle = instance.AddComponent<Puzzle>();

        // Set level no and type of puzzle
        string[] dataSet1 = model.name.Split('-');
        puzzle.type = dataSet1[1];
        puzzle.LevelNo = int.Parse(dataSet1[0].Split('_')[1]);

        List<string> childrenNames = new(instance.transform.childCount);

        for (int j = 0; j < instance.transform.childCount; j++)
        {
            Transform child = instance.transform.GetChild(j);
            //child.GetComponent<MeshRenderer>().material = material;

            // Attach the PuzzlePiece script to the model's children from index 1 and onwards
            // because first child is frame of the puzzle
            if (j > 0)
            {
                childrenNames.Add(child.name);
                PuzzlePiece puzzlePiece = child.gameObject.AddComponent<PuzzlePiece>();

                puzzlePiece.type = int.Parse(child.name.Split('-')[2]);
                child.gameObject.AddComponent<MeshCollider>();
                puzzle.AddPiece(puzzlePiece);
            }
        }

        // Create the prefab from the modified model
        PrefabUtility.SaveAsPrefabAsset(instance, prefabPath);

        // Destroy the model and the instance in the scene
        Object.Destroy(model);
        Object.Destroy(instance);

        // Print a message in the console to indicate that the prefab creation is done
        Debug.Log("Prefab " + prefabPath + " created and saved");

        return childrenNames;
    }
}
