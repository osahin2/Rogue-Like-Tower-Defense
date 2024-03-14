using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UIScripts.Menu
{
    public class MenuTabButton : MonoBehaviour
    {
        private event Action<MenuState> _onClick;

        [SerializeField] private MenuState _state;
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _buttonText;
        [SerializeField] private Button _button;
        [SerializeField] private Image _selectedImage;
        [SerializeField] private float _iconSelectedHeight = 5f;
        [SerializeField] private float _animDuration = .15f;
        public MenuState State => _state;
        public bool IsLocked { get; private set; }

        private Tween _iconMove;
        private float _iconDefaultPosition;

        public void Init()
        {
            _iconDefaultPosition = _icon.rectTransform.anchoredPosition.y;
            _button.onClick.AddListener(() =>
            {
                Debug.Log("Tab Button Clicked");
                _onClick?.Invoke(_state);
            });
        }
        public void DeInit()
        {
            _button.onClick.RemoveAllListeners();
        }
        public void SetMenuTabData(MenuTabData data)
        {
            _icon.sprite = data.Icon;
            _state = data.State;
            _buttonText.text = data.TabName;
        }

        public void Select()
        {
            var targetY = _iconDefaultPosition + _iconSelectedHeight;
            _selectedImage.enabled = true;
            IconMoveAnimation(targetY);
        }
        public void DeSelect()
        {
            _selectedImage.enabled = false;
            IconMoveAnimation(_iconDefaultPosition);
        }

        private void IconMoveAnimation(float targetY)
        {
            _iconMove?.Kill(true);
            _iconMove = _icon.rectTransform.DOAnchorPosY(
                targetY,
                _animDuration);
        }
        public void AddListener(Action<MenuState> onClicked)
        {
            _onClick += onClicked;
        }
        public void RemoveListener(Action<MenuState> onClicked)
        {
            _onClick -= onClicked;
        }
        public void RemoveAllListeners()
        {
            _onClick = null;
        }
    }
}