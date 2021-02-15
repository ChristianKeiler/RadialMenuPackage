using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Keiler.RadialMenu
{
    public abstract class RadialMenuItemAnimation : MonoBehaviour
    {
        public abstract IEnumerator Animate(bool active, List<Transform> items);
    }
}
