using UnityEngine;
using UnityEditor;

namespace DiasGames.FootstepSystem
{
    [CustomEditor(typeof(FootstepData))]
    [CanEditMultipleObjects]
    public class FootstepInspector : Editor
    {
        SerializedProperty volumeProperty, pitchProperty;
        SerializedProperty clips;

        private void OnEnable()
        {
            volumeProperty = serializedObject.FindProperty("volumeVariance");
            pitchProperty = serializedObject.FindProperty("pitchVariance");
            clips = serializedObject.FindProperty("m_FootstepsClips");
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.BeginHorizontal(GUI.skin.box);
            GUILayout.FlexibleSpace();
            serializedObject.Update();

            GUILayout.Label(serializedObject.targetObject.name, EditorStyles.boldLabel);

            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(volumeProperty);
            EditorGUILayout.PropertyField(pitchProperty);

            EditorGUILayout.Space();

            for (int i = 0; i < clips.arraySize; i++)
                DrawClips(i);

            EditorGUILayout.Space();

            if(GUILayout.Button("Add Ground Type"))
            {
                clips.arraySize++;
            }

            if (GUILayout.Button("Remove Ground Type"))
            {
                GenericMenu menu = new GenericMenu();

                for (int i = 0; i < clips.arraySize; i++)
                    menu.AddItem(new GUIContent(clips.GetArrayElementAtIndex(i).FindPropertyRelative("m_GroundType").stringValue), false, RemoveGround, i);

                menu.ShowAsContext();
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void RemoveGround(object index)
        {
            clips.DeleteArrayElementAtIndex((int)index);
            serializedObject.ApplyModifiedProperties();
        }

        private void DrawClips(int i)
        {
            EditorGUILayout.BeginVertical(GUI.skin.box);

            SerializedProperty clip = clips.GetArrayElementAtIndex(i);
            EditorGUI.indentLevel++;
            if (EditorGUILayout.PropertyField(clip))
            {
                EditorGUI.indentLevel++;
                SerializedProperty name = clip.FindPropertyRelative("m_GroundType");
                SerializedProperty walkClips = clip.FindPropertyRelative("m_Walking");
                SerializedProperty runClips = clip.FindPropertyRelative("m_Running");
                SerializedProperty landClips = clip.FindPropertyRelative("m_Landing");

                EditorGUILayout.PropertyField(name);
                EditorGUILayout.Space();

                EditorGUILayout.PropertyField(walkClips, true);
                EditorGUILayout.Space();
                EditorGUILayout.PropertyField(runClips, true);
                EditorGUILayout.Space();
                EditorGUILayout.PropertyField(landClips, true);
                EditorGUILayout.Space();

                EditorGUI.indentLevel--;
            }

            EditorGUI.indentLevel--;
            EditorGUILayout.EndVertical();
        }

    }

}
