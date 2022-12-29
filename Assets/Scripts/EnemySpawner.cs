using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy;
    private int minX = -100;
    private int maxX = Screen.width + 100;
    private int maxY = -100;
    private int minY = Screen.height + 100;
    private Vector2 screenCenter;

    private Dictionary<string, GameObject> enemies;

    void Start()
    {
        screenCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        enemies = new Dictionary<string, GameObject>();
        StartCoroutine("InstantiateEnemy");
    }

    void Update() 
    {
        var closest = GetClosestEnemy();
        closest.GetComponent<SpriteRenderer>().color = Color.cyan;
    }

    public GameObject GetClosestEnemy() {
        GameObject closest = null;
        var closestDist = float.MaxValue;
        foreach(KeyValuePair<string, GameObject> kv in enemies) {
            var dist = Vector2.Distance(kv.Value.transform.position, screenCenter);
            if (dist < closestDist) {
                closest = kv.Value;
                closestDist = dist;
            }
        }
        return closest;
    }

    IEnumerator InstantiateEnemy() {
        for(;;)
        {
            var spawnHorizontal = randomBool();
            float x,y;
            if (spawnHorizontal) {
                x = Random.Range(minX, maxX);
                y = randomBool() ? minY : maxY;
            } else {
                x = randomBool() ? minX : maxX;
                y = Random.Range(maxY, minY);
            }
            var newEnemy = Instantiate(enemy, new Vector3(x, y, 0), Quaternion.identity);
            var id = System.Guid.NewGuid().ToString();
            newEnemy.GetComponent<EnemyStats>().SetId(id);

            enemies.Add(id, newEnemy);  
            yield return new WaitForSeconds(Random.Range(2, 5));
        }
    }

    private bool randomBool() {
        return Random.value > 0.5f;
    }
}
