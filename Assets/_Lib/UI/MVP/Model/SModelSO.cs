using UnityEngine;

public abstract class SModelSO : ScriptableObject, ISModel
{
    public bool IsInitialized { get; private set; }

    public void Initialize()
    {
        
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