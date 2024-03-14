using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MonoBehaviour), true, isFallback = true)]
public class ComponentDependencyEditor : Editor
{
    public override void OnInspectorGUI()
    {

        MonoBehaviour monoBehaviour = (MonoBehaviour)target;
        Type monoBehaviourType = monoBehaviour.GetType();

        // Check for the ComponentDependencyAttribute on the target class
        var dependencyAttributes = monoBehaviourType.GetCustomAttributes(typeof(ComponentDependencyAttribute), true);
        foreach (ComponentDependencyAttribute dependencyAttribute in dependencyAttributes)
        {
            Type requiredComponentType = dependencyAttribute.RequiredComponentType;
            Component dependencyComponent = monoBehaviour.GetComponent(requiredComponentType);

            if (dependencyComponent == null)
            {
                EditorGUILayout.HelpBox($"{requiredComponentType.Name} is required by {monoBehaviourType.Name}, but it is not present on the GameObject.", MessageType.Error);
            }
        }

        base.OnInspectorGUI();
    }
}