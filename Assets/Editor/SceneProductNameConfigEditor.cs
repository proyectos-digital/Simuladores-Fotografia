using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using System.Collections.Generic;

[CustomEditor(typeof(SceneProductNameConfig))]
public class SceneProductNameConfigEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SceneProductNameConfig config = (SceneProductNameConfig)target;

        GUILayout.Space(10);

        // Mostrar un botón para cada par de escena y producto
        for (int i = 0; i < config.sceneProductPairs.Length; i++)
        {
            EditorGUILayout.BeginHorizontal();

            // Mostrar el nombre de la escena y el producto
            EditorGUILayout.LabelField(config.sceneProductPairs[i].sceneName != null ? config.sceneProductPairs[i].sceneName.name : "NULL", GUILayout.Width(100));
            config.sceneProductPairs[i].productName = EditorGUILayout.TextField(config.sceneProductPairs[i].productName);

            // Botón para actualizar productName y Build Settings para la escena actual
            if (GUILayout.Button("Update", GUILayout.Width(60)))
            {
                UpdateProductNameAndBuildSettings(config, i);
            }

            EditorGUILayout.EndHorizontal();
        }
    }

    private void UpdateProductNameAndBuildSettings(SceneProductNameConfig config, int index)
    {
        SceneProductNameConfig.SceneProductPair pair = config.sceneProductPairs[index];

        // Cambiar el productName
        PlayerSettings.productName = pair.productName;
        Debug.Log("Product Name changed to: " + pair.productName + " for scene: " + pair.sceneName.name);

        // Limpiar y agregar la escena al Build Settings
        ClearAndAddSceneToBuildSettings(pair.sceneName);
    }

    private void ClearAndAddSceneToBuildSettings(SceneAsset sceneAsset)
    {
        EditorBuildSettingsScene[] newScenes = new EditorBuildSettingsScene[1];
        newScenes[0] = new EditorBuildSettingsScene(AssetDatabase.GetAssetPath(sceneAsset), true);
        EditorBuildSettings.scenes = newScenes;
        Debug.Log("Scene added to Build Settings: " + sceneAsset.name);
    }
}
