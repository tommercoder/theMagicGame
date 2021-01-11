using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DiasGames.FootstepSystem;

public class AddFootstepSystem : EditorWindow
{
    Animator character;
    FootstepData footstepData;
    Rigidbody rigidBody;

    [MenuItem("Tools/Dias Games/Add Footstep System")]
    public static void ShowWindow()
    {
        GetWindow<AddFootstepSystem>(true, "Add Footstep System to a character");
    }

    private void Awake()
    {
        footstepData = AssetDatabase.LoadAssetAtPath<FootstepData>("Assets/Dias Games/Foot Solutions/Footstep System/Default Footsteps.asset");
    }

    private void OnGUI()
    {
        GUILayout.Space(10);

        // Set label style for header
        GUIStyle m_Style = new GUIStyle(EditorStyles.label);
        m_Style.wordWrap = true;
        m_Style.fontStyle = FontStyle.Bold;

        // Draw GUI label and fields
        GUILayout.Label("Select from scene the character you want to add Footstep System", m_Style);

        character = EditorGUILayout.ObjectField("Character", character, typeof(Animator), true) as Animator;
        if (character == null)
            EditorGUILayout.HelpBox("Please select a character to add system.", MessageType.Error, false);
        else if (!character.isHuman)
            EditorGUILayout.HelpBox("This selected character is not a Humanoid Character. Please, select a Humanoid Character", MessageType.Error, false);
        else
            rigidBody = character.GetComponent<Rigidbody>();

        footstepData = EditorGUILayout.ObjectField("Footstep Data", footstepData, typeof(FootstepData), true) as FootstepData;

        GUIStyle button = new GUIStyle(EditorStyles.miniButton);
        button.stretchWidth = false;
        button.fixedHeight = 25f;
        button.fontSize = 12;

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        if(GUILayout.Button("Add Footstep System", button))
        {
            AddSystem(character, HumanBodyBones.LeftFoot);
            AddSystem(character, HumanBodyBones.RightFoot);
            Close();
        }

        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
    }

    void AddSystem(Animator anim, HumanBodyBones foot)
    {
        GameObject footObject = anim.GetBoneTransform(foot).gameObject;

        FootstepController controller = footObject.GetComponent<FootstepController>();
        if (controller == null)
            controller = footObject.AddComponent<FootstepController>();

        SerializedObject serializedObject = new SerializedObject(controller);
        serializedObject.Update();

        SerializedProperty footstepProperty = serializedObject.FindProperty("m_FootstepData");
        SerializedProperty footReferenceProperty = serializedObject.FindProperty("m_FootReference");
        SerializedProperty footRigidbody = serializedObject.FindProperty("m_Rigidbody");

        Debug.Log(footstepData);

        footRigidbody.objectReferenceValue = rigidBody;
        footstepProperty.objectReferenceValue = footstepData;
        footReferenceProperty.enumValueIndex = (foot == HumanBodyBones.RightFoot) ? 0 : 1;

        AudioSource audioSource = footObject.GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = footObject.AddComponent<AudioSource>();

        audioSource.spatialBlend = 1;
        serializedObject.ApplyModifiedProperties();
    }
}
