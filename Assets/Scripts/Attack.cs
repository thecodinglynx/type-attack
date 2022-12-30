using UnityEngine;

public class Attack : MonoBehaviour
{
    private Rigidbody2D rb;
    private float force = 2f;
    private GameObject enemy;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Launch();
    }

    public void ShootAt(GameObject enemy) {
        this.enemy = enemy;
    }

    private void Launch() {
        var direction = enemy.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y) * force;
    }
}
