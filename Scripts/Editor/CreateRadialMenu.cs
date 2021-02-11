using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Keiler.RadialMenu
{
    public class CreateRadialMenu : EditorWindow
    {
        private int m_numberOfItems = 2;
        private GameObject m_radialMenu;

        [MenuItem("RadialMenu/Radial Menu")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(CreateRadialMenu));
        }

        private void OnGUI()
        {
            GUILayout.Label("Radial Menu", EditorStyles.boldLabel);

            m_numberOfItems = EditorGUILayout.IntField("Number of Items", m_numberOfItems);

            if (GUILayout.Button(new GUIContent("Create")))
            {
                Create();
            }
        }

        private void Create()
        {
            Canvas canvas = FindCanvas();

            AddRadialMenu(canvas.transform);
        }

        private Canvas FindCanvas()
        {
            Scene activeScene = SceneManager.GetActiveScene();
            Canvas canvas = null;
            foreach (var root in activeScene.GetRootGameObjects())
            {
                canvas = root.GetComponentInChildren<Canvas>(true);
                if (canvas)
                {
                    break;
                }
            }

            if (!canvas)
            {
                GameObject gameObject = new GameObject("Canvas", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
                gameObject.layer = 5;
                canvas = gameObject.GetComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                Undo.RegisterCreatedObjectUndo(gameObject, "Created Canvas");

                GameObject eventSystem = new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));
                Undo.RegisterCreatedObjectUndo(eventSystem, "Created EventSystem");

            }

            return canvas;
        }

        private void AddRadialMenu(Transform root)
        {
            m_radialMenu = Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Packages/com.keiler.radialmenu/Resources/Prefabs/RadialMenu.prefab"), root);
            m_radialMenu.name = "RadialMenu";
            m_radialMenu.layer = 5;

            RadialMenu radialMenuComponent = m_radialMenu.GetComponent<RadialMenu>();
            for (int i = 0; i < m_numberOfItems; i++)
            {
                AddItem();
            }
            m_radialMenu.transform.SetParent(root, false);
            Undo.RegisterCreatedObjectUndo(m_radialMenu, "Created Radial Menu");
        }

        private void AddItem()
        {
            GameObject item = Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Packages/com.keiler.radialmenu/Resources/Prefabs/MenuItem.prefab"), m_radialMenu.transform);
            item.name = $"Item{m_radialMenu.transform.childCount - 1}";
            Undo.RegisterCreatedObjectUndo(item, "Add Item to RadialMenu");
        }
    }
}
