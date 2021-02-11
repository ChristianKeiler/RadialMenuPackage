using System.Collections.Generic;
using UnityEngine;

namespace Keiler.RadialMenu
{
    [ExecuteInEditMode]
    public class RadialLayoutGroup : MonoBehaviour
    {
        [SerializeField]
        protected float m_childAlignmentRadius = 0;

        public float ChildAlignmentRadius { get { return m_childAlignmentRadius; } set { m_childAlignmentRadius = value; } }

        protected int m_childCount = 0;
        protected Dictionary<int, Transform> m_children = new Dictionary<int, Transform>();

        private void Update()
        {
            if (m_childCount != transform.childCount)
            {
                UpdateChildPosition();
            }
        }

        private void OnValidate()
        {
            UpdateChildPosition();
        }

        protected virtual void UpdateChildPosition()
        {
            m_childCount = transform.childCount;
            m_children.Clear();

            if (m_childCount > 1)
            {
                int idx = 0;
                float angle = 360.0f / m_childCount;
                foreach (Transform child in transform)
                {
                    float curAngle = angle * idx;
                    Vector3 pos = new Vector3(m_childAlignmentRadius * Mathf.Sin(curAngle * Mathf.Deg2Rad), m_childAlignmentRadius * Mathf.Cos(curAngle * Mathf.Deg2Rad));
                    child.localPosition = pos;
                    m_children.Add(idx, child);
                    idx++;
                }
            }
            else if (m_childCount == 1)
            {
                foreach (Transform child in transform)
                {
                    child.position = Vector3.zero;
                    m_children.Add(0, child);
                }
            }
        }
    }
}
