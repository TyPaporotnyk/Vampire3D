using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody _rb;

    private static readonly int SpeedHash =
        Animator.StringToHash("Speed");

    private static readonly int IsGroundedHash =
        Animator.StringToHash("Grounded");

    private static readonly int VerticalVelocityHash =
        Animator.StringToHash("VerticalVelocity");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponentInParent<Rigidbody>();
    }

    public void StartAnimation(string animationName)
    {
        _animator.SetBool(animationName, true);
    }

    public void StopAnimation(string animationName)
    {
        _animator.SetBool(animationName, false);
    }

    public void SetAnimation(string animationName, bool value)
    {
        _animator.SetBool(animationName, value);
    }
}
