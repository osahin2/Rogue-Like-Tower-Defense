using Player;
using UnityEditor;
using UnityEngine;
using Extension;

namespace Rogue_Enemy.EditorScripts
{
    [CustomEditor(typeof(EnemyManager))]
    public class EnemySpawnCircleEditor : Editor
    {
        private PlayerController controller;
        private void OnSceneGUI()
        {
            var manager = (EnemyManager)target;
            controller = controller.IsNull() ? FindObjectOfType<PlayerController>() : controller;

            if (controller.IsNull())
            {
                return;
            }

            Handles.color = Color.red;
            Handles.DrawWireArc(controller.transform.position, Vector3.back, Vector3.up, 360, manager.SpawnRadius);
        }
    }
}