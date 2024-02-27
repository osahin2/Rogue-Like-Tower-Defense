using System.Collections.Generic;
using UnityEngine;

namespace Player.Spells
{
    public class SpellFactory : ISpellFactory
    {
        private List<SpellConfig> _spellConfigs;
        private Transform _spellParent;

        private readonly Dictionary<SpellType, SpellConfig> _configDict = new();
        public SpellFactory(List<SpellConfig> configs, Transform spellParent)
        {
            _spellConfigs = configs;
            _spellParent = spellParent;

            SetDictionary();
        }

        private void SetDictionary()
        {
            foreach (var config in _spellConfigs)
            {
                _configDict.Add(config.SpellType, config);
            }
        }
        public ISpell CreateSpell(SpellType type)
        {
            var config = GetConfig(type);
            var spell = config.CreateOrGet(_spellParent);
            spell.Construct(config);
            return spell;
        }

        private SpellConfig GetConfig(SpellType type)
        {
            if (_configDict.TryGetValue(type,out var config))
            {
                return config;
            }
            throw new KeyNotFoundException($"SpellFactory: {type} Spell Not Found In Dictionary");
        }

    }
}