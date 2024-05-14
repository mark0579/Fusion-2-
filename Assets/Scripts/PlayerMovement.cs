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

    // 동기화할 속도, 죽음 상태 및 flip 상태를 Networked 속성으로 선언
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
        // 자신의 플레이어만 제어
        if (HasStateAuthority == false)
        {
            return;
        }

        Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0) * Runner.DeltaTime * PlayerSpeed;
        _controller.Move(move);

        // 속도 동기화
        Speed = move.magnitude;

        // 좌우 이동에 따라 flipX 동기화
        if (move.x != 0)
        {
            bool newFlip = move.x < 0;

            // 동기화된 변수의 값이 변경된 경우에만 업데이트
            if (IsFlipped != newFlip)
            {
                IsFlipped = newFlip;
                UpdateFlip();
            }
        }

        // 죽음 상태 제어 예시
        if (Input.GetKeyDown(KeyCode.K))
        {
            IsDead = !IsDead;
            //Debug.Log($"IsDead toggled to {IsDead}");
        }

        // 상하 이동만 있을 때는 스프라이트 반전 유지
        if (move != Vector3.zero)
        {
            gameObject.transform.up = Vector3.up;
        }
    }

    // flip 상태 변경 시 동기화된 값을 스프라이트에 적용
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
