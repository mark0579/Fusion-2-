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
        // ��Ʈ��ũ �Է� ��������
        if (GetInput(out NetworkAnimatorData inputData))
        {
            Speed = inputData.Speed;
            Dead = inputData.Dead;

            // �ִϸ����Ϳ� �� ����
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
