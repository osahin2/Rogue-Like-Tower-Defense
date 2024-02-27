using Player.Weapons;
using SaveLoad;
using System;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class PlayerData : ISaveData
    {
        [field: SerializeField] public string Id { get; set; }

        public WeaponType weaponType;
    }
}