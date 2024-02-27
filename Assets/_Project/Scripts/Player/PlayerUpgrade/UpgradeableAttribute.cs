using System;
using System.Collections.Generic;
using UnityEngine;
namespace Player.Upgrades
{
    [Serializable]
    public class UpgradeableAttribute<T>
    {
        public List<T> Attributes;
    }
}