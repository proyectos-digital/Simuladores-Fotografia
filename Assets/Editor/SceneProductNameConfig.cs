using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneProductNameConfig", menuName = "ScriptableObjects/SceneProductNameConfig", order = 1)]
public class SceneProductNameConfig : ScriptableObject
{
    [System.Serializable]
    public struct SceneProductPair
    {
        public SceneAsset sceneName;
        [HideInInspector]public string productName;
    }

    public SceneProductPair[] sceneProductPairs; // Array de pares de nombre de escena y nombre de producto
}
