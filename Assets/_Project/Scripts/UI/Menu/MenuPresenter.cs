using System.Collections.Generic;

namespace UIScripts.Menu
{
    public class MenuPresenter : SPresenter<MenuModel, MenuView>
    {
        private readonly MenuModel _menuModel;
        private readonly MenuView _menuView;
        private MenuPresenter(MenuModel model, MenuView view) : base(model, view)
        {
            _menuModel = model;
            _menuView = view;
        }
        protected override void OnInit()
        {

        }
        protected override void OnDeInit()
        {
        }
        protected override void OnShow()
        {
            _menuView.SelectMenuTab(MenuState.Play);
            _menuModel.SetCurrentTab(MenuState.Play);
        }
        protected override void OnHide()
        {

        }
        private void OnMenuButtonClicked(MenuState state)
        {
            if (state == MenuState.Locked)
            {
                return;
            }

            _menuView.DeSelectMenuTab(_menuModel.CurrentTab.Value.State);
            _menuView.SelectMenuTab(state);
            _menuModel.SetCurrentTab(state);
        }
        protected override void OnAddEvents()
        {
            foreach (var menuButton in _menuView.MenuButtons)
            {
                menuButton.AddListener(OnMenuButtonClicked);
            }
        }
        protected override void OnRemoveEvents()
        {
            foreach (var menuButton in _menuView.MenuButtons)
            {
                menuButton.RemoveListener(OnMenuButtonClicked);
            }
        }

        public class Builder
        {
            private readonly MenuModel model = new();
            public Builder WithMenuTabs(List<MenuTabData> tabs)
            {
                model.SetMenuTabs(tabs);
                return this;
            }

            public MenuPresenter Build(MenuView view)
            {
                view.SetMenuButtons(model.MenuTabs);
                return new MenuPresenter(model, view);
            }
        }

    }
}