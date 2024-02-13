using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SView : MonoBehaviour, ISView
{
    public bool IsInitialized { get; private set; }
    public bool IsVisible { get; private set; }


    #region IView : Initialize | OnInitialize
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

    #endregion IView : Initialize | OnInitialize

    #region IView : DeInitialize | OnDeInitialize
    public void DeInitialize()
    {
        IsInitialized = false;
        OnDeInitialized();
    }
    protected abstract void OnDeInitialized();
    #endregion IView : DeInitialize | OnDeInitialize

    #region IView : Show | OnShow
    public void Show()
    {
        IsVisible = true;

        OnShow();

        OnAddEvents();
    }
    protected abstract void OnShow();

    #endregion IView : Show | OnShow

    #region IView : Hide | OnHide
    public void Hide()
    {
        IsVisible = false;

        OnHide();

        OnRemoveEvents();
    }
    protected abstract void OnHide();

    #endregion IView : Hide | OnHide

    #region IView Events : OnAddEvents | OnRemoveEvents
    protected abstract void OnAddEvents();
    protected abstract void OnRemoveEvents();

    #endregion IView Events : OnAddEvents | OnRemoveEvents


}
