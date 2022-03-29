#if (UNITY_EDITOR) 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[CustomEditor(typeof(AutomaticMaterialGenerator))]
class AutomaticMaterialGeneratorEditor : Editor
{
    /*
    AutomaticMaterialGenerator script;
    void OnEnable()
    {
        script = (AutomaticMaterialGenerator)target;
    }
    */
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Generate Materials"))
        {
            GenerateMaterials();
        }
    }

    public void GenerateMaterials()
    {
        string folderPath = GetClickedDirFullPath();
        if (folderPath.Contains("."))
            folderPath = folderPath.Remove(folderPath.LastIndexOf('/'));
        Debug.Log(folderPath);

        List<Texture2D> basemaps = new List<Texture2D>();
        string[] guids = AssetDatabase.FindAssets("_Base Map", new[] { folderPath });
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Debug.Log("Loading " + path);
            basemaps.Add( AssetDatabase.LoadAssetAtPath<Texture2D>(path));
        }

        List <Texture2D> normalmaps = new List<Texture2D>();
        guids = AssetDatabase.FindAssets("_Normal", new[] { folderPath });
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Debug.Log("Loading " + path);
            normalmaps.Add(AssetDatabase.LoadAssetAtPath<Texture2D>(path));
        }

        List<Texture2D> maskmaps = new List<Texture2D>();
        guids = AssetDatabase.FindAssets("_Mask Map", new[] { folderPath });
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Debug.Log("Loading " + path);
            maskmaps.Add(AssetDatabase.LoadAssetAtPath<Texture2D>(path));
        }

        List<Material> materials = new List<Material>();
        foreach (Texture2D basemap in basemaps)
        {
            string name = basemap.name.Replace("_BaseMap", "");
            Debug.Log(name);
            Material material = new Material(Shader.Find("HDRP/Lit"));
            material.name = name + ".mat";

            /*
            for (int i=0; i<= material.shader.GetPropertyCount()-1; i++)
            {
                Debug.Log( material.shader.GetPropertyName(i));
            }
            */

            material.SetTexture("_BaseColorMap", basemap);

            foreach (Texture2D normalmap in normalmaps)
                if(normalmap.name.Contains(name))
                {
                    material.SetTexture("_NormalMap", normalmap);
                    break;
                }

            foreach (Texture2D maskmap in maskmaps)
                if (maskmap.name.Contains(name))
                {
                    material.SetTexture("_MaskMap", maskmap);
                    break;
                }

            AssetDatabase.CreateAsset(material, folderPath + "/" + material.name);
        }
        AssetDatabase.SaveAssets();
    }

    private static string GetClickedDirFullPath()
    {
        string clickedAssetGuid = Selection.assetGUIDs[0];
        string clickedPath = AssetDatabase.GUIDToAssetPath(clickedAssetGuid);
        string clickedPathFull = Path.Combine(Directory.GetCurrentDirectory(), clickedPath);

        FileAttributes attr = File.GetAttributes(clickedPathFull);
        string path = attr.HasFlag(FileAttributes.Directory) ? clickedPathFull : Path.GetDirectoryName(clickedPathFull);
        return path.Remove(0, path.IndexOf("Assets"));
    }

}

[ExecuteInEditMode]
public class AutomaticMaterialGenerator : MonoBehaviour
{

}

#endif
