using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RoomData))]
public class MapCreaterEditor : Editor
{

    public override void OnInspectorGUI()
    {

        base.OnInspectorGUI();

        if (GUILayout.Button("SavePrefab"))
        {

            var asset = PrefabUtility.SaveAsPrefabAsset((target as RoomData).gameObject, $"{Application.dataPath}/Resources/Map/{(target as RoomData).gameObject.name}.prefab");
            AssetDatabase.SaveAssets(); 
            AssetDatabase.Refresh();

        }

    }

}
