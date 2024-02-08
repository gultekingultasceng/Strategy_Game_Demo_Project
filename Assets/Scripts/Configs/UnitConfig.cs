using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "UnitConfig", menuName = "Configs/UnitConfig")]
    public class UnitConfig : ScriptableObject
    {
        public int Health;
    }
}

