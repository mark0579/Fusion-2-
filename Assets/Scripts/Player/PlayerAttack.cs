using UnityEngine;
using Fusion;

public class PlayerAttack : NetworkBehaviour
{
    public NetworkPrefabRef projectilePrefab; // ��Ʈ��ũ ������
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
        if (!HasStateAuthority) return; // ��Ʈ��ũ ������ �ִ� �ν��Ͻ������� ����

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
                    rb.velocity = direction * 10f; // �ӵ� ����
                }
            });
        }
    }
}
