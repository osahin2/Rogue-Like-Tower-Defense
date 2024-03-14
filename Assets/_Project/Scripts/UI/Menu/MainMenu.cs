using System.Collections.Generic;
using UnityEngine;

namespace UIScripts.Menu
{
    public enum MenuState
    {
        Play,
        Inventory,
        Map,
        Locked
    }
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private MenuView _menuView;
        [SerializeField] private List<MenuTabData> _tabs;

        private MenuPresenter _presenter;
        public void Construct()
        {
            _presenter = new MenuPresenter.Builder()
                .WithMenuTabs(_tabs)
                .Build(_menuView);
        }
        public void Show()
        {
            _presenter.Init();
            _presenter.Show();
        }
        public void Hide()
        {
            _presenter.Hide();
            _presenter.DeInit();
        }

    }
}