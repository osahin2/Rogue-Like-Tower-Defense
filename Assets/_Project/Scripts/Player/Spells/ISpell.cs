using System;

namespace Player.Spells
{
    public enum SpellType
    {
        AirForce,
        SlowArea,
    }
    public interface ISpell : IDisposable
    {
        SpellType Type { get; }
        void Use();
    }
}