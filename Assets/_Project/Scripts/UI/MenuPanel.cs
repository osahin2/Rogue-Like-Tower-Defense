using Assets._Project.Scripts.UI.Menu;
using UnityEngine;

namespace UIScripts
{
    public abstract class MenuPanel : MonoBehaviour
    {
        [SerializeField] private MenuState _state;
        public MenuState MenuState => _state;

        public abstract void Construct();
        public abstract void Show();
        public abstract void Hide();
    }
}