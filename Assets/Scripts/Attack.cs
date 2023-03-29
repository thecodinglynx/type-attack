using UnityEngine;

public class Attack : MonoBehaviour
{
    private Rigidbody2D rb;
    private float force = 50f;
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
        var direction = (enemy.transform.position - transform.position).normalized;
        rb.velocity = new Vector3(direction.x, direction.y, 1) * force;
    }
}
