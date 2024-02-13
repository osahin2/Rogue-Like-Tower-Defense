public abstract class SModel : ISModel
{
    public bool IsInitialized { get; private set; }

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

    public void DeInitialize()
    {
        IsInitialized = false;
        OnDeInitialized();
    }
    protected abstract void OnDeInitialized();


}
