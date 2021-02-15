using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Keiler.RadialMenu
{
    public static class CreateRadialMenu/* : EditorWindow*/
    {
        private static int m_numberOfItems = 5;
        private static GameObject m_radialMenu;

        //[MenuItem("RadialMenu/Radial Menu")]
        //public static void ShowWindow()
        //{
        //    EditorWindow.GetWindow(typeof(CreateRadialMenu));
        //}

        //private void OnGUI()
        //{
        //    GUILayout.Label("Radial Menu", EditorStyles.boldLabel);

        //    m_numberOfItems = EditorGUILayout.IntField("Number of Items", m_numberOfItems);

        //    if (GUILayout.Button(new GUIContent("Create")))
        //    {
        //        Create();
        //    }
        //}

        [MenuItem("GameObject/UI/Radial Menu")]
        private static void Create()
        {
            Canvas canvas = FindCanvas();

            AddRadialMenu(canvas.transform, false);
        }

        [MenuItem("GameObject/UI/Radial Menu - TextMeshPro")]
        private static void CreateTMP()
        {
            Canvas canvas = FindCanvas();

            AddRadialMenu(canvas.transform, true);
        }

        private static Canvas FindCanvas()
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

                EventSystem eventSystemComponent = null;
                foreach(var root in activeScene.GetRootGameObjects())
                {
                    eventSystemComponent = root.GetComponentInChildren<EventSystem>(true);
                    if(eventSystemComponent)
                    {
                        break;
                    }
                }
                if (!eventSystemComponent)
                {
                    GameObject eventSystem = new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));
                    Undo.RegisterCreatedObjectUndo(eventSystem, "Created EventSystem");
                }
            }

            return canvas;
        }

        private static void AddRadialMenu(Transform root, bool useTextMeshPro)
        {
            m_radialMenu = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Packages/com.keiler.radialmenu/Resources/Prefabs/RadialMenu.prefab"), root);
            m_radialMenu.name = "RadialMenu";
            m_radialMenu.layer = 5;

            RadialMenu radialMenuComponent = m_radialMenu.GetComponent<RadialMenu>();
            radialMenuComponent.m_TMPisDefault = useTextMeshPro;
            for (int i = 0; i < m_numberOfItems; i++)
            {
                AddItem(useTextMeshPro);
            }
            m_radialMenu.transform.SetParent(root, false);
            Undo.RegisterCreatedObjectUndo(m_radialMenu, "Created Radial Menu");
        }

        private static void AddItem(bool useTextMeshPro)
        {
            GameObject item;
            if(useTextMeshPro)
            {
                item = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Packages/com.keiler.radialmenu/Resources/Prefabs/TMPMenuItem.prefab"), m_radialMenu.transform);
            }
            else
            {
                item = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Packages/com.keiler.radialmenu/Resources/Prefabs/MenuItem.prefab"), m_radialMenu.transform);
            }
            item.name = $"Item{m_radialMenu.transform.childCount - 1}";
            Undo.RegisterCreatedObjectUndo(item, "Add Item to RadialMenu");
        }
    }
}
