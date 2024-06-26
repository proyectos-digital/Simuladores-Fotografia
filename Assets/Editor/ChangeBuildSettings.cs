using UnityEditor;
using UnityEngine;

public class ChangeBuildSettings : MonoBehaviour
{
    public static SceneProductNameConfig config;

    // Cargar el ScriptableObject al inicializar la clase
    static ChangeBuildSettings()
    {
        config = AssetDatabase.LoadAssetAtPath<SceneProductNameConfig>("Assets/Settings/SceneProductNameConfig.asset");
    }

    [MenuItem("Build/Fotografia/Fotografía Plaza")]
    static void FotoCaso1()
    {
        UpdateProductNameAndBuildSettings(0);
    }

    [MenuItem("Build/Fotografia/Fotografia Polideportivo")]
    static void FotoCaso2()
    {
        UpdateProductNameAndBuildSettings(1);
    }

    [MenuItem("Build/Fotografia/Fotografía Rural")]
    static void FotoCaso3()
    {
        UpdateProductNameAndBuildSettings(2);
    }

    [MenuItem("Build/Fotografia/Fotografía Municipio")]
    static void FotoCaso4()
    {
        UpdateProductNameAndBuildSettings(3);
    }

    [MenuItem("Build/Fotografia/Fotografía Studio")]
    static void FotoCaso5()
    {
        UpdateProductNameAndBuildSettings(4);
    }

    [MenuItem("Build/Fotografia/Fotografía Studio 2")]
    static void FotoCaso6()
    {
        UpdateProductNameAndBuildSettings(5);
    }

    [MenuItem("Build/Tv/Tv Nosferatu")]
    static void TvCaso3()
    {
        UpdateProductNameAndBuildSettings(6);
    }

    [MenuItem("Build/Tv/Tv Polideportivo")]
    static void TvCaso4()
    {
        UpdateProductNameAndBuildSettings(7);
    }

    [MenuItem("Build/Tv/Tv Municipio")]
    static void TvCaso5()
    {
        UpdateProductNameAndBuildSettings(8);
    }
    [MenuItem("Build/Tv/Tv Festival")]
    static void TvCaso6()
    {
        UpdateProductNameAndBuildSettings(9);
    }

    private static void UpdateProductNameAndBuildSettings(int index)
    {
        if (config == null)
        {
            Debug.LogError("Config file not loaded.");
            return;
        }

        if (index < 0 || index >= config.sceneProductPairs.Length)
        {
            Debug.LogError("Invalid index.");
            return;
        }

        SceneProductNameConfig.SceneProductPair pair = config.sceneProductPairs[index];

        // Cambiar el productName
        PlayerSettings.productName = pair.productName;
        Debug.Log("Product Name changed to: " + pair.productName + " for scene: " + pair.sceneName.name);

        // Limpiar y agregar la escena al Build Settings
        ClearAndAddSceneToBuildSettings(pair.sceneName);
    }

    private static void ClearAndAddSceneToBuildSettings(SceneAsset sceneAsset)
    {
        EditorBuildSettingsScene[] newScenes = new EditorBuildSettingsScene[1];
        newScenes[0] = new EditorBuildSettingsScene(AssetDatabase.GetAssetPath(sceneAsset), true);
        EditorBuildSettings.scenes = newScenes;
        Debug.Log("Scene added to Build Settings: " + sceneAsset.name);
    }
}
