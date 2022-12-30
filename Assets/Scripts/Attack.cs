using UnityEngine;

public class Attack : MonoBehaviour
{
    private Rigidbody2D rb;
    private float force = 2f;
    private GameObject enemy;
    private float distanceThreshold = 50;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (enemy != null) {
            var direction = enemy.transform.position - transform.position;
            if (Vector3.Distance(enemy.transform.position, transform.position) > distanceThreshold) {
                rb.velocity = new Vector2(direction.x, direction.y) * force;
            }
        }
    }

    public void ShootAt(GameObject enemy) {
        this.enemy = enemy;
    }
}
