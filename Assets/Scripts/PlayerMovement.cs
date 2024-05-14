// File: Assets/Scripts/PlayerMovement.cs
using Fusion;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    private CharacterController _controller;
    //private Rigidbody2D _controller;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    public float PlayerSpeed = 4f;

    // ����ȭ�� �ӵ�, ���� ���� �� flip ���¸� Networked �Ӽ����� ����
    [Networked] private bool IsFlipped { get; set; }
    [Networked] private float Speed { get; set; }
    [Networked] private bool IsDead { get; set; }

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }
    public override void FixedUpdateNetwork()
    {
        // �ڽ��� �÷��̾ ����
        if (HasStateAuthority == false)
        {
            return;
        }

        Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0) * Runner.DeltaTime * PlayerSpeed;
        _controller.Move(move);

        // �ӵ� ����ȭ
        Speed = move.magnitude;

        // �¿� �̵��� ���� flipX ����ȭ
        if (move.x != 0)
        {
            bool newFlip = move.x < 0;

            // ����ȭ�� ������ ���� ����� ��쿡�� ������Ʈ
            if (IsFlipped != newFlip)
            {
                IsFlipped = newFlip;
                UpdateFlip();
            }
        }

        // ���� ���� ���� ����
        if (Input.GetKeyDown(KeyCode.K))
        {
            IsDead = !IsDead;
            //Debug.Log($"IsDead toggled to {IsDead}");
        }

        // ���� �̵��� ���� ���� ��������Ʈ ���� ����
        if (move != Vector3.zero)
        {
            gameObject.transform.up = Vector3.up;
        }
    }

    // flip ���� ���� �� ����ȭ�� ���� ��������Ʈ�� ����
    private void UpdateFlip()
    {
        _spriteRenderer.flipX = IsFlipped;
    }

    public override void Render()
    {
        _animator.SetFloat("Speed", Speed);
        _animator.SetBool("Dead", IsDead);
        Debug.Log($"Animator Dead set to {IsDead}");
        UpdateFlip();
    }
}
