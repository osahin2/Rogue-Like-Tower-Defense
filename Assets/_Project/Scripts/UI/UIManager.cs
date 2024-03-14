using Assets._Project.Scripts.UI.Inventory;
using System.Collections;
using UnityEngine;

namespace UIScripts
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private MenuPanelManager _panelManager;
        public void Construct()
        {
            _panelManager.Construct();
        }

        public void OpenMenu()
        {
            _panelManager.Show();
        }
        public void CloseMenu()
        {
            _panelManager.Hide();
        }
    }
}