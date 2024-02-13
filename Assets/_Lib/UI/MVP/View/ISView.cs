public interface ISView
{
    bool IsInitialized { get; }
    bool IsVisible { get; }

    void Initialize();
    void DeInitialize();

    void Show();
    void Hide();
}