public interface ISPresenter
{
    bool IsInitialized { get; }

    void Initialize();
    void DeInitialize();

    void Show();
    void Hide();
}
