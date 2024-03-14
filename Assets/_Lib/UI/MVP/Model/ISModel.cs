public interface ISModel
{
    bool IsInitialized { get; }

    void Init();
    void DeInit();
}