using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameManager))]
public class GameManager_Editor : Editor
{
    [MenuItem("GameObject/Game Manager/GameManager", false, -10)]
    static void CreateGameManager() 
    {
        GameObject target = (GameObject)PrefabUtility.InstantiatePrefab(Resources.Load("Management/Game/Game Manager"));
        if (Selection.activeTransform != null)
        {
            target.transform.parent = Selection.activeTransform;
            target.transform.localPosition = Vector3.zero;
        }
        else
        {
            target.transform.position = Vector3.zero;
        }

        Selection.activeTransform = target.transform;
        Undo.RegisterCreatedObjectUndo(target, "Create GameManager");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
}
