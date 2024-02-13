using System.Collections;
using UnityEngine;

namespace Assets._Lib.UI.UIView
{
    public abstract class SUIView : MonoBehaviour, ISView
    {
        public bool IsInitialized {  get; private set; }

        public bool IsVisible {  get; private set; }

        #region ISView : Initialize | OnInitialized
        public void Initialize()
        {
            if (IsInitialized)
            {
                return;
            }
            IsInitialized = true;
            OnInitialized();
        }
        protected abstract void OnInitialized();

        #endregion ISView : Initialize | OnInitialized

        #region ISView : DeInitialize | OnDeInitialized
        public void DeInitialize()
        {
            IsInitialized = false;
            OnDeInitialized();
        }
        protected abstract void OnDeInitialized();

        #endregion ISView : DeInitialize | OnDeInitialized


        #region ISView : Show | OnShow
        public void Show()
        {
            IsVisible = true;

            OnShow();
            OnAddEvents();
        }
        protected abstract void OnShow();

        #endregion ISView : Show | OnShow

        #region ISView : Hide | OnHide
        public void Hide()
        {
            IsVisible = false;

            OnHide();
            OnRemoveEvents();
        }
        protected abstract void OnHide();
        #endregion ISView : Hide | OnHide


        #region OnAddEvents | OnRemoveEvents
        protected abstract void OnAddEvents();
        protected abstract void OnRemoveEvents();

        #endregion OnAddEvents | OnRemoveEvents

    }
}