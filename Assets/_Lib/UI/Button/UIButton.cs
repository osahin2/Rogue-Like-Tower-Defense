using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Lib.UI.ButtonUI
{
    public class UIButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        public void AddListener(Action onClicked)
        {
            _button.onClick.AddListener(() =>
            {
                onClicked?.Invoke();
            });
        }
        
    }
}