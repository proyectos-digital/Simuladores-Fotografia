using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "SceneProductNameConfig", menuName = "ScriptableObjects/SceneProductNameConfig", order = 1)]
public class SceneProductNameConfig : ScriptableObject
{
    [System.Serializable]
    public struct SceneProductPair
    {
        public string sceneName;
        public string productName;
    }

    public SceneProductPair[] sceneProductPairs; // Array de pares de nombre de escena y nombre de producto
}
