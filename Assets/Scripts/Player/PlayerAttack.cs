using UnityEngine;
using Fusion;

public class PlayerAttack : NetworkBehaviour
{
    public NetworkPrefabRef projectilePrefab; // 네트워크 프리팹
    public float attackInterval = 2.0f;
    private float attackTimer;

    private Scanner scanner;

    void Start()
    {
        attackTimer = attackInterval;
        scanner = GetComponent<Scanner>();
    }

    public override void FixedUpdateNetwork()
    {
        if (!HasStateAuthority) return; // 네트워크 권한이 있는 인스턴스에서만 실행

        attackTimer -= Runner.DeltaTime;
        if (attackTimer <= 0)
        {
            Attack();
            attackTimer = attackInterval;
        }
    }

    void Attack()
    {
        if (projectilePrefab.IsValid && scanner.nearestTarget != null)
        {
            Vector3 spawnPosition = transform.position;
            Vector3 direction = (scanner.nearestTarget.position - transform.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);
            Runner.Spawn(projectilePrefab, spawnPosition, rotation, Object.InputAuthority, (runner, obj) => {
                Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.velocity = direction * 10f; // 속도 설정
                }
            });
        }
    }
}
