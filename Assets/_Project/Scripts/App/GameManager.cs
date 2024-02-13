using EventBusSystem;
using Service_Locator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameContext _gameContext;

        private void Awake()
        {
            _gameContext.Init();
        }
    }
}
