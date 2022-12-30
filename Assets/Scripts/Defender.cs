using UnityEngine;

public class Defender : MonoBehaviour
{
    [SerializeField]
    private GameObject projectile;

    [SerializeField]
    private EnemySpawner enemySpawner;

    private Vector3 screenCenter;

    public void ShootAtClosest() {
        screenCenter = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0);
        var bullet = Instantiate(projectile, screenCenter, Quaternion.identity);
        bullet.GetComponent<Attack>().ShootAt(enemySpawner.GetClosestEnemy());
    }
}
