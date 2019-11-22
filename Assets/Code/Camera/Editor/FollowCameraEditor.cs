//
//  CONTRIBUTORS: Montana Games (www.montana-games.com)                  
//  Victor Bisterfeld (www.gbviktor.de) viktor@montana-games.com 
//  -------------------------------------------------------------------------------------------------
//

using UnityEngine;
using UnityEditor;
using System;

/// <summary>
/// Editor for Validate data in FollowCamera Script
/// </summary>
[CustomEditor(typeof(FollowCamera))]
public class FollowCameraEditor : Editor
{
	
	// Fields ----------------------------------------------------------------------------------
	FollowCamera _temp;
    CameraTarget[] followCamerasInScene;
    public static bool fold = true;

    //  Initialization --------------------------------------------------------------------------

    private void OnEnable()
    {
        _temp = target as FollowCamera;
        FindTargetsInScene();
        _temp.SelectFirstTargetIfNotExist();
    }

    

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (!_temp) return;
        EditorGUILayout.Space();

        EditorGUILayout.BeginVertical();
        fold = EditorGUILayout.BeginFoldoutHeaderGroup(fold, "Обьекты доступные для слежения камерой");
        
        if (fold)
        {
            EditorGUILayout.Space();

            for (int i = 0; i < followCamerasInScene.Length; i++)
            {
                EditorGUILayout.BeginHorizontal();
                
                if (GUILayout.Button(followCamerasInScene[i] == _temp.target ? "✓" : " ", GUILayout.Width(25), GUILayout.Height(15)))
                {
                    _temp.ChangeTarget(followCamerasInScene[i]);
                }
                EditorGUILayout.ObjectField(followCamerasInScene[i].transform.gameObject, typeof(GameObject), true);

                EditorGUILayout.EndHorizontal();

            }

            if(followCamerasInScene.Length==0) { EditorGUILayout.LabelField("Найдено 0 целей в сцене...");}

            EditorGUILayout.Space();
        }
        
        HandleErrorTargetsInSceneNotFound();

        EditorGUILayout.EndFoldoutHeaderGroup();
        EditorGUILayout.EndVertical();
    }

    private void HandleErrorTargetsInSceneNotFound()
    {
        if (followCamerasInScene == null || followCamerasInScene.Length == 0)
            EditorGUILayout.HelpBox("Обьекты для слежения камерой не найдены в сцене, добавьте компонент FollowCamera на обьект за которым камера должна следить.", MessageType.Error);
    }

    private void FindTargetsInScene()
    {
        followCamerasInScene = SceneView.FindObjectsOfType<CameraTarget>();
    }
}