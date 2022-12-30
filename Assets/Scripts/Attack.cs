using UnityEngine;

public class Attack : MonoBehaviour
{
    private Rigidbody2D rb;
    private float force = 8f;
    private GameObject enemy;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (enemy != null) {
            var direction = enemy.transform.position - transform.position;
            rb.velocity = new Vector2(direction.x, direction.y) * force;
        }
    }

    public void ShootAt(GameObject enemy) {
        this.enemy = enemy;
    }
}
