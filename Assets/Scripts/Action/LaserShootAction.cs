using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShootAction : MonoBehaviour, Action
{
    [SerializeField]
    private GameObject projectile;

    private Vector3 screenCenter;

    public void perform() {
        screenCenter = new Vector3(0, 0, 1);
        var closest = EnemySpawner.Instance.GetClosestEnemy();
        if (closest == null) {
            return;
        }
        var bullet = Instantiate(projectile, screenCenter, Quaternion.identity);
        bullet.GetComponent<Attack>().ShootAt(closest);
    }
}
