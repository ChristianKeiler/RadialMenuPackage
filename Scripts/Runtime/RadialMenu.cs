using System.Collections;
using System.Linq;
using UnityEngine;

namespace Keiler.RadialMenu
{
    public class RadialMenu : RadialLayoutGroup
    {
        public enum ButtonShape
        {
            Slice,
            Circle
        }

        [SerializeField]
        private ButtonShape m_buttonShape;

        [SerializeField]
        private Vector2 m_buttonSize = Vector2.zero;

        [SerializeField]
        private Sprite m_itemSprite;

        [SerializeField]
        private GameObject m_customMenuItemPrefab;

        [SerializeField]
        private RadialMenuItemAnimation m_itemAnimation;

        #region EditorHelpers
        public GameObject m_menuItemPrefab = null;

        public bool m_TMPisDefault = false;

        #endregion

        private void Awake()
        {
            UpdateChildPosition();
        }

        public void SetActive(bool enable)
        {
            if (m_itemAnimation)
            {
                StartCoroutine(m_itemAnimation.Animate(enable, transform.Cast<Transform>().ToList()));
            }
        }

        private void OnValidate()
        {
            UpdateChildPosition();
        }

        protected override void UpdateChildPosition()
        {
            switch (m_buttonShape)
            {
                case ButtonShape.Slice:
                    m_childCount = transform.childCount;

                    if (m_childCount > 1)
                    {
                        int idx = 0;
                        float angle = 360.0f / m_childCount;
                        foreach (Transform child in transform)
                        {
                            child.localPosition = Vector3.zero;
                            child.localRotation = Quaternion.Euler(0, 0, WrapAngle((angle / 2) - angle * idx));
                            UnityEngine.UI.Image image = child.GetComponentInChildren<UnityEngine.UI.Image>(true);
#if UNITY_EDITOR
                            RectTransform childTransform = child.GetComponent<RectTransform>();
                            if (m_itemSprite)
                            {
                                if(m_buttonSize != Vector2.zero)
                                {
                                    childTransform.sizeDelta = m_buttonSize;
                                }
                                else
                                {
                                    childTransform.sizeDelta = new Vector2(380f, 380f);
                                }

                                image.sprite = m_itemSprite;
                            }
                            else
                            {
                                childTransform.sizeDelta = new Vector2(380f, 380f);
                                image.sprite = UnityEditor.AssetDatabase.LoadAssetAtPath<Sprite>("Packages/com.keiler.radialmenu/Resources/Sprites/RadialMenuSliced.png");
                            }
#endif

                            image.type = UnityEngine.UI.Image.Type.Filled;
                            image.fillMethod = UnityEngine.UI.Image.FillMethod.Radial360;
                            image.fillAmount = 1.0f / m_childCount;

                            foreach (Transform button in child)
                            {
                                float curAngle = angle * idx;
                                Vector3 pos = new Vector3(m_childAlignmentRadius * Mathf.Sin(curAngle * Mathf.Deg2Rad), m_childAlignmentRadius * Mathf.Cos(curAngle * Mathf.Deg2Rad));
                                button.position = transform.position + pos;
                                button.localRotation = Quaternion.Euler(0, 0, -child.localEulerAngles.z);
                                idx++;
                            }
                        }
                    }
                    else if (m_childCount == 1)
                    {
                        foreach (Transform child in transform)
                        {
                            child.position = Vector3.zero;
                        }
                    }
                    break;
                case ButtonShape.Circle:
                    base.UpdateChildPosition();
                    foreach(Transform child in transform)
                    {
                        UnityEngine.UI.Image image = child.GetComponent<UnityEngine.UI.Image>();

#if UNITY_EDITOR
                        RectTransform childTransform = child.GetComponent<RectTransform>();
                        if(m_itemSprite)
                        {
                            if (m_buttonSize != Vector2.zero)
                            {
                                childTransform.sizeDelta = m_buttonSize;
                            }
                            else
                            {
                                childTransform.sizeDelta = new Vector2(128f, 128f);
                            }

                            image.sprite = m_itemSprite;
                        }
                        else
                        {
                            childTransform.sizeDelta = new Vector2(128f, 128f);
                            image.sprite = UnityEditor.AssetDatabase.LoadAssetAtPath<Sprite>("Packages/com.keiler.radialmenu/Resources/Sprites/RadialMenuCircle.png");
                        }
#endif
                        image.type = UnityEngine.UI.Image.Type.Simple;

                        foreach(Transform button in child)
                        {
                            button.localPosition = Vector3.zero;
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        private float WrapAngle(float angle)
        {
            if (angle > 360f)
            {
                angle -= 360f;
            }
            else if (angle < 0f)
            {
                angle += 360f;
            }
            return angle;
        }
    }
}
