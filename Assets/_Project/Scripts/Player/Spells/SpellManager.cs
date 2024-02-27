using Rogue_LevelData;
using Service_Locator;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Player.Spells
{
    public class SpellManager : MonoBehaviour
    {
        [SerializeField] private List<SpellConfig> _spellConfigs;

        private ISpellFactory _spellFactory;

        private List<ISpell> _activatedSpells = new();
        private bool AnyActivatedSpell => _activatedSpells.Count > 0;
        public void Init()
        {
            _spellFactory = new SpellFactory(_spellConfigs, transform);
        }
        public void Dispose()
        {
            Clear();
        }
        private void Update()
        {
            if (!AnyActivatedSpell)
            {
                return;
            }

            UseActivatedSpells();
        }

        private void UseActivatedSpells()
        {
            foreach (var spell in _activatedSpells)
            {
                spell.Use();
            }
        }
        public void AddSpell(SpellType type)
        {
            if (CheckIfSpellIsActivated(type))
            {
                Debug.Log("Spell was already activated");
                return;
            }
            var spell = _spellFactory.CreateSpell(type);
            _activatedSpells.Add(spell);
        }

        private void Clear()
        {
            foreach(var spell in _activatedSpells)
            {
                spell.Dispose();
            }
            _activatedSpells.Clear();
        }

        private bool CheckIfSpellIsActivated(SpellType type)
        {
            return _activatedSpells.Any(x => x.Type == type);
        }
    }
}