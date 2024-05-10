using UnityEngine;
using Fusion;

public class PlayerAnimationController : NetworkBehaviour
{
    private Animator _animator;
    private CharacterController _controller;

    // ����ȭ�� �ӵ��� ���� ����
    [Networked] private float Speed { get; set; }
    [Networked] private bool IsDead { get; set; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _controller = GetComponent<CharacterController>();
    }

    public override void FixedUpdateNetwork()
    {
        // �ڽ��� �÷��̾ ����
        if (HasStateAuthority == false)
        {
            return;
        }

        // �÷��̾� �̵� �ӵ� ���
        Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        Speed = move.magnitude;

        // ���� ���� ����
        if (Input.GetKeyDown(KeyCode.K))
        {
            IsDead = !IsDead;
        }
    }

    public override void Render()
    {
        // ��Ʈ��ũ�� ����ȭ�� �Ķ���͸� Animator�� �ݿ�
        _animator.SetFloat("Speed", Speed);
        _animator.SetBool("Dead", IsDead);
    }
}
