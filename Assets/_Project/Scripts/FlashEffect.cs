using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class FlashEffect : MonoBehaviour, IFlash
{
    [SerializeField] private float _flashAmount;
    [SerializeField] private float _flashTime;
    [SerializeField] private Color _flashColor;
    [SerializeField] private Material _flashMaterial;

    private SpriteRenderer _sprite;
    private Material _defaultMat;
    private Tween _flashTween;

    private static readonly int FlashAmountID = Shader.PropertyToID("_FlashAmount");
    private static readonly int FlashColor = Shader.PropertyToID("_FlashColor");

    public void Construct(SpriteRenderer sprite, Material defaultMat)
    {
        _sprite = sprite;
        _defaultMat = defaultMat;
    }

    public void Flash()
    {
        _flashTween?.Kill(true);

        _sprite.material = _flashMaterial;
        _sprite.material.SetColor(FlashColor, _flashColor);

        _flashTween = _sprite.material.DOFloat(_flashAmount, FlashAmountID, _flashTime)
            .SetEase(Ease.InFlash)
            .SetLoops(2, LoopType.Yoyo)
            .OnComplete(() =>
            {
                _sprite.material.SetColor(FlashColor, Color.white);
                _sprite.material = _defaultMat;
            });
    }
}
public interface IFlash
{
    void Construct(SpriteRenderer renderer, Material defaultMat);
    void Flash();
}
