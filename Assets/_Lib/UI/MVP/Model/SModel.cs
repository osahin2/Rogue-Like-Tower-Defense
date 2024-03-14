public abstract class SModel : ISModel
{
    public bool IsInitialized { get; private set; }

    public void Init()
    {
        if (IsInitialized)
        {
            return;
        }

        IsInitialized = true;
        OnInit();
    }

    protected abstract void OnInit();

    public void DeInit()
    {
        IsInitialized = false;
        OnDeInit();
    }
    protected abstract void OnDeInit();


}
