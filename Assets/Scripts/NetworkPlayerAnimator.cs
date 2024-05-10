// NetworkPlayerAnimator.cs
using Fusion;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class NetworkPlayerAnimator : NetworkBehaviour
{
    private Animator _animator;

    [Networked] private float Speed { get; set; }
    [Networked] private bool Dead { get; set; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public override void FixedUpdateNetwork()
    {
        // 네트워크 입력 가져오기
        if (GetInput(out NetworkAnimatorData inputData))
        {
            Speed = inputData.Speed;
            Dead = inputData.Dead;

            // 애니메이터에 값 적용
            UpdateAnimator();
        }
    }

    private void UpdateAnimator()
    {
        _animator.SetFloat("Speed", Speed);
        _animator.SetBool("Dead", Dead);
    }

    public override void Render()
    {
        UpdateAnimator();
    }
}
