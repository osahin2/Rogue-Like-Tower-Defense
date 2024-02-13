public abstract class SPresenter<TModel, TView> : ISPresenter
    where TModel : ISModel
    where TView : ISView
{
    public bool IsInitialized { get; private set; }

    protected readonly TModel _model;
    protected readonly TView _view;

    #region Constructor
    protected SPresenter(TModel model, TView view)
    {
        _model = model;
        _view = view;
    }

    #endregion Constructor

    #region ISPresenter : Initialize | OnInitialize
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

    #endregion ISPresenter : Initialize | OnInitialize

    #region ISPresenter : DeInitialize | OnDeInitialize
    public void DeInitialize()
    {
        IsInitialized = false;
        OnDeInitialized();
    }
    protected abstract void OnDeInitialized();

    #endregion ISPresenter : DeInitialize | OnDeInitialize

    #region ISPresenter : Show | OnShow
    public void Show()
    {
        _view.Show();

        OnShow();

        OnAddEvents();
    }
    protected abstract void OnShow();

    #endregion ISPresenter : Show | OnShow

    #region ISPresenter : Hide | OnHide
    public void Hide()
    {
        OnRemoveEvents();

        _view.Hide();

        OnHide();

    }
    protected abstract void OnHide();
    #endregion ISPresenter : Hide | OnHide

    #region Events : OnAddEvents | OnRemoveEvents
    protected abstract void OnAddEvents();
    protected abstract void OnRemoveEvents();

    #endregion Events : OnAddEvents | OnRemoveEvents

}