using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeProductName : MonoBehaviour
{
    public SceneProductNameConfig productNameConfig; // Referencia al ScriptableObject de configuración

    [MenuItem("Custom/Change Product Name for Active Scene")]
    static void ChangeProductNameMethod()
    {
        // Obtener el índice de la escena activa en Build Settings
        int activeSceneBuildIndex = GetCurrentActiveSceneBuildIndex();

        // Si no se encuentra la escena activa en Build Settings, mostrar un mensaje de error y salir
        if (activeSceneBuildIndex == -1)
        {
            Debug.LogError("Active scene is not in Build Settings. Please add it to Scenes In Build.");
            return;
        }

        // Obtener el nombre de la escena activa en Build Settings
        string activeSceneName = SceneUtility.GetScenePathByBuildIndex(activeSceneBuildIndex);
        activeSceneName = System.IO.Path.GetFileNameWithoutExtension(activeSceneName);

        // Buscar el nombre de la escena activa en productNameConfig y cambiar el productName si se encuentra
        foreach (SceneProductNameConfig.SceneProductPair pair in GetInstance().sceneProductPairs)
        {
            if (pair.sceneName.Equals(activeSceneName))
            {
                PlayerSettings.productName = pair.productName;
                Debug.Log("Product Name changed to: " + pair.productName + " for scene: " + activeSceneName);
                return;
            }
        }

        Debug.LogWarning("No matching scene found in SceneProductNameConfig for active scene: " + activeSceneName);
    }

    // Método para obtener una instancia del scriptable object SceneProductNameConfig
    static SceneProductNameConfig GetInstance()
    {
        //SceneProductNameConfig config = AssetDatabase.LoadAssetAtPath
        SceneProductNameConfig config = AssetDatabase.LoadAssetAtPath<SceneProductNameConfig>("Assets/SceneProductNameConfig.asset");
        if (config == null)
        {
            Debug.LogError("SceneProductNameConfig not found. Please create and assign a SceneProductNameConfig asset.");
            return null;
        }

        return config;
    }

    // Método para obtener el índice de la escena activa en Build Settings
    static int GetCurrentActiveSceneBuildIndex()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        string activeScenePath = activeScene.path;

        for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
        {
            if (EditorBuildSettings.scenes[i].path == activeScenePath)
            {
                return i;
            }
        }

        return -1;
    }
}
