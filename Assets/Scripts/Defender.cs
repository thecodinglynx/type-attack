using UnityEngine;

public class Defender : MonoBehaviour
{
    [SerializeField]
    private GameObject projectile;

    [SerializeField]
    private EnemySpawner enemySpawner;

    private Vector3 screenCenter;

    public void ShootAtClosest() {
        screenCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        var closest = enemySpawner.GetClosestEnemy();
        if (closest == null) {
            return;
        }
        var bullet = Instantiate(projectile, screenCenter, Quaternion.identity);
        bullet.GetComponent<Attack>().ShootAt(closest);
    }
}
