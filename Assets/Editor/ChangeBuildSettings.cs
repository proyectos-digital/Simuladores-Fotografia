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

    [MenuItem("Build/Fotografia/Caso1 Plaza")]
    static void FotoCaso1()
    {
        UpdateProductNameAndBuildSettings(0);
    }

    [MenuItem("Build/Fotografia/Caso2 Polideportivo")]
    static void FotoCaso2()
    {
        UpdateProductNameAndBuildSettings(1);
    }

    [MenuItem("Build/Fotografia/Caso3 Rural")]
    static void FotoCaso3()
    {
        UpdateProductNameAndBuildSettings(2);
    }

    [MenuItem("Build/Fotografia/Caso4 Municipio")]
    static void FotoCaso4()
    {
        UpdateProductNameAndBuildSettings(3);
    }

    [MenuItem("Build/Fotografia/Caso5 Studio")]
    static void FotoCaso5()
    {
        UpdateProductNameAndBuildSettings(4);
    }

    [MenuItem("Build/Fotografia/Caso6 Studio People")]
    static void FotoCaso6()
    {
        UpdateProductNameAndBuildSettings(5);
    }

    [MenuItem("Build/Tv/Caso3 Nosferatu")]
    static void TvCaso3()
    {
        UpdateProductNameAndBuildSettings(6);
    }

    [MenuItem("Build/Tv/Caso4 Polideportivo")]
    static void TvCaso4()
    {
        UpdateProductNameAndBuildSettings(7);
    }

    [MenuItem("Build/Tv/Caso5 Municipio")]
    static void TvCaso5()
    {
        UpdateProductNameAndBuildSettings(8);
    }
    [MenuItem("Build/Tv/Caso6 Festival")]
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
