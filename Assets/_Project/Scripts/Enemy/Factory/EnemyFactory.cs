﻿using System.Collections.Generic;
using UnityEngine;

namespace Rogue_Enemy.Factory
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly List<GenericEnemyFactory> _enemyFactoryList = new();

        private readonly Dictionary<EnemyType, IGenericEnemyFactory> _enemyFactoryMap = new();

        public EnemyFactory(List<GenericEnemyFactory> enemyFactories)
        {
            _enemyFactoryList = enemyFactories;
        }
        public void Init()
        {
            InitializeAndAddFactoriesToDict();
        }
        private void InitializeAndAddFactoriesToDict()
        {
            foreach (var enemyFactory in _enemyFactoryList)
            {
                var go = new GameObject($"{enemyFactory.EnemyType}_Parent");
                enemyFactory.Construct(go.transform);
                _enemyFactoryMap.Add(enemyFactory.EnemyType, enemyFactory);
            }
        }

        public IEnemy Get(EnemyType enemyType) => CreateEnemy(enemyType);

        private IEnemy CreateEnemy(EnemyType enemyType)
        {
            var factory = GetEnemyFactoryFromDictionary(enemyType);
            var enemy = factory.GetEnemy();
            return enemy;
        }

        private IGenericEnemyFactory GetEnemyFactoryFromDictionary(EnemyType enemyType)
        {
            if (_enemyFactoryMap.ContainsKey(enemyType))
            {
                return _enemyFactoryMap[enemyType];
            }

            throw new KeyNotFoundException($"{enemyType} Not Found In Factory Dictionary");
        }


    }
}
