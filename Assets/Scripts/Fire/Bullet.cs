using UnityEngine;
using Fusion;

public class Bullet : NetworkBehaviour
{
    public float damage = 10f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Monster"))
        {
            Monster monster = other.GetComponent<Monster>();
            if (monster != null)
            {
                monster.TakeDamage(damage);
                Runner.Despawn(Object);
            }
        }
    }
}
