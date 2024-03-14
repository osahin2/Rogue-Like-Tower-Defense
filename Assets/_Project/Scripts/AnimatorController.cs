using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorController : MonoBehaviour
{
    [SerializeField] private float _crossFadeDuration = .1f;

    private Animator _animator;

    public void Init()
    {
        _animator = GetComponent<Animator>();
    }
    public float PlayAnimation(int animationHash) 
    {
        if (!_animator.enabled)
        {
            return 0f;
        }
        _animator.CrossFade(animationHash, _crossFadeDuration);
        return _animator.GetCurrentAnimatorStateInfo(0).length;
    }

    public void SetActiveAnimator(bool active) => _animator.enabled = active;
}
