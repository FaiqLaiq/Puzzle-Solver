using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MaterialCreator
{
    public Material CreateMat(string texturePath, string materialPath)
    {
        Texture2D texture = Resources.Load<Texture2D>(texturePath);
        // Create a new material with the Unlit/Texture shader
        Material material = new(Shader.Find("Unlit/Texture"));

        // Apply the texture to the material
        material.mainTexture = texture;

        // Save the material in the folder
        AssetDatabase.CreateAsset(material, materialPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        return material;
    }
}
