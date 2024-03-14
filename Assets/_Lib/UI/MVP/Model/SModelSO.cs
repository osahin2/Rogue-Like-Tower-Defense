using UnityEngine;

public abstract class SModelSO : ScriptableObject, ISModel
{
    public bool IsInitialized { get; private set; }

    public void Init()
    {
        
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