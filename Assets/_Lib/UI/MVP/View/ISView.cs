public interface ISView
{
    bool IsInitialized { get; }
    bool IsVisible { get; }

    void Init();
    void DeInit();

    void Show();
    void Hide();
}