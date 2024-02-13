public interface ISModel
{
    bool IsInitialized { get; }

    void Initialize();
    void DeInitialize();
}