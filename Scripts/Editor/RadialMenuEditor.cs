using UnityEditor;
using UnityEngine;

namespace Keiler.RadialMenu
{
    [CustomEditor(typeof(RadialMenu))]
    public class RadialMenuEditor : Editor
    {
        private Transform transform;

        private SerializedProperty m_childAlignmentRadius;
        private SerializedProperty m_buttonShape;
        private SerializedProperty m_buttonSize;
        private SerializedProperty m_itemSprite;
        private SerializedProperty m_customMenuItemPrefab;
        private SerializedProperty m_menuItemAnimation;

        private SerializedProperty m_menuItemPrefab;
        private void OnEnable()
        {
            transform = ((RadialMenu)target).transform;

            m_childAlignmentRadius = serializedObject.FindProperty("m_childAlignmentRadius");
            m_buttonShape = serializedObject.FindProperty("m_buttonShape");
            m_buttonSize = serializedObject.FindProperty("m_buttonSize");
            m_itemSprite = serializedObject.FindProperty("m_itemSprite");
            m_customMenuItemPrefab = serializedObject.FindProperty("m_customMenuItemPrefab");
            m_menuItemAnimation = serializedObject.FindProperty("m_itemAnimation");

            m_menuItemPrefab = serializedObject.FindProperty("m_menuItemPrefab");
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.LabelField(new GUIContent("Radial Layout Group Settings"), EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(m_childAlignmentRadius, new GUIContent("Button Alignment Radius"));
            EditorGUI.indentLevel--;
            EditorGUILayout.Space();

            EditorGUILayout.LabelField(new GUIContent("Radial Menu Settings"), EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(m_buttonShape, new GUIContent("Button Shape"));
            if(m_itemSprite.objectReferenceValue != null && m_buttonSize.vector2Value == Vector2.zero)
            {
                EditorGUILayout.Space();
                EditorGUILayout.HelpBox("You have set a custom Sprite to be used as mask. Make sure you set the button sizes according to your needs." +
                    "Neglecting to assign the size will result in the script resizing the Mask to predefined values that most likely will look bad with your custom Mask sprite!", MessageType.Error);
                EditorGUILayout.Space();
            }
            EditorGUILayout.PropertyField(m_buttonSize, new GUIContent("Button Size", "Translates to [width, height]. If left at [0, 0] it will use default sizes predetermined by the author"));
            EditorGUI.indentLevel--;
            EditorGUILayout.Space();

            EditorGUILayout.LabelField(new GUIContent("Customization"), EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(m_itemSprite, new GUIContent("Mask Sprite"));

            GUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(m_customMenuItemPrefab, new GUIContent("Menu Item Prefab"));
            if(m_menuItemPrefab.objectReferenceValue != m_customMenuItemPrefab.objectReferenceValue)
            {
                if(GUILayout.Button(new GUIContent("Apply prefab")))
                {
                    m_menuItemPrefab.objectReferenceValue = m_customMenuItemPrefab.objectReferenceValue;
                    ReplaceItems();
                }
            }
            GUILayout.EndHorizontal();

            EditorGUILayout.PropertyField(m_menuItemAnimation, new GUIContent("Menu Item Animation", "Allows the RadialMenu to animate in and out. Define the animation yourself."));
            EditorGUI.indentLevel--;

            serializedObject.ApplyModifiedProperties();
            EditorGUILayout.Space();
            if (GUILayout.Button(new GUIContent("Add Item")))
            {
                AddItem();
            }
        }

        private void ReplaceItems()
        {
            int elements = transform.childCount;
            while(transform.childCount != 0)
            {
                DestroyImmediate(transform.GetChild(0).gameObject);
            }

            for(int i = 0; i < elements; i++)
            {
                AddItem();
            }
        }

        private void AddItem()
        {
            GameObject item;
            // if custom prefab is wanted instantiate this
            if (m_customMenuItemPrefab.objectReferenceValue != null)
            {
                item = Instantiate((GameObject)m_customMenuItemPrefab.objectReferenceValue, transform);
            }
            // otherwise instantiate either a TextMeshPro item or a default UI item instead
            else
            {
                if(((RadialMenu)target).m_TMPisDefault)
                {
                    item = Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Packages/com.keiler.radialmenu/Resources/Prefabs/TMPMenuItem.prefab"), transform);
                }
                else
                {
                    item = Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Packages/com.keiler.radialmenu/Resources/Prefabs/MenuItem.prefab"), transform);
                }
            }
            item.name = $"Item{transform.childCount - 1}";
            UnityEditor.Undo.RegisterCreatedObjectUndo(item, "Add Item to RadialMenu");
        }
    }
}
