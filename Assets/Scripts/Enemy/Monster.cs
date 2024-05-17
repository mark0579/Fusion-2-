using UnityEngine;
using Fusion;

public class Monster : NetworkBehaviour
{
    public float health = 100f;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void TakeDamageRpc(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
        else
        {
            ShowHitEffect();
        }
    }

    public void TakeDamage(float damage)
    {
        if (Object.HasStateAuthority)
        {
            TakeDamageRpc(damage);
        }
    }

    private void Die()
    {
        Runner.Despawn(Object);
    }

    private void ShowHitEffect()
    {
        animator.SetTrigger("Hit");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            if (bullet != null)
            {
                TakeDamage(bullet.damage);
                Runner.Despawn(bullet.Object);
            }
        }
    }
}
