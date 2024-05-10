using UnityEngine;
using Fusion;

public class PlayerAnimationController : NetworkBehaviour
{
    private Animator _animator;
    private CharacterController _controller;

    // 동기화할 속도와 죽음 상태
    [Networked] private float Speed { get; set; }
    [Networked] private bool IsDead { get; set; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _controller = GetComponent<CharacterController>();
    }

    public override void FixedUpdateNetwork()
    {
        // 자신의 플레이어만 제어
        if (HasStateAuthority == false)
        {
            return;
        }

        // 플레이어 이동 속도 계산
        Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        Speed = move.magnitude;

        // 죽음 상태 예시
        if (Input.GetKeyDown(KeyCode.K))
        {
            IsDead = !IsDead;
        }
    }

    public override void Render()
    {
        // 네트워크로 동기화된 파라미터를 Animator에 반영
        _animator.SetFloat("Speed", Speed);
        _animator.SetBool("Dead", IsDead);
    }
}
