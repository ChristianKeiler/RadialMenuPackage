using System.Collections.Generic;
using UnityEngine;

namespace Keiler.RadialMenu
{
    public abstract class RadialMenuItemAnimation : MonoBehaviour
    {
        public abstract void Animate(bool active, List<Transform> items);
    }
}
