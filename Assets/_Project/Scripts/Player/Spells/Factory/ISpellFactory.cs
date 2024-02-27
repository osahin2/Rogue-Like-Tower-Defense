namespace Player.Spells
{
    public interface ISpellFactory
    {
        ISpell CreateSpell(SpellType type);
    }
}