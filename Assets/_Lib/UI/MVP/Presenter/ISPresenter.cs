public interface ISPresenter
{
    bool IsInitialized { get; }

    void Init();
    void DeInit();

    void Show();
    void Hide();
}
