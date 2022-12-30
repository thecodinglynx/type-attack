using UnityEngine;

public class Defender : MonoBehaviour
{
    [SerializeField]
    private GameObject projectile;

    [SerializeField]
    private EnemySpawner enemySpawner;

    private Vector3 screenCenter;

    public void ShootAtClosest() {
        screenCenter = new Vector3(0, 0, 1);
        var closest = enemySpawner.GetClosestEnemy();
        if (closest == null) {
            return;
        }
        var bullet = Instantiate(projectile, screenCenter, Quaternion.identity);
        bullet.GetComponent<Attack>().ShootAt(closest);
    }
}
