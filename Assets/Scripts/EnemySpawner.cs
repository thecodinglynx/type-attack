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

    void OnEnable()
    {
        EventManager.StartListening(EventManager.Event.ENEMY_DESTROYED, OnEnemyDestroyed);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventManager.Event.ENEMY_DESTROYED, OnEnemyDestroyed);
    }

    void Start()
    {
        screenCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        enemies = new Dictionary<string, GameObject>();
        StartCoroutine("InstantiateEnemy");
    }

    void Update() 
    {
        MarkClosestEnemy();
    }

    void OnEnemyDestroyed(Dictionary<string, object> message)
    {
        var id = (string)message["id"];
        enemies.Remove(id);
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

    public void MarkClosestEnemy() {
        var closest = GetClosestEnemy();
        foreach(KeyValuePair<string, GameObject> kv in enemies) {
            if (kv.Key == closest.GetComponent<Enemy>().GetId()) {
                kv.Value.GetComponent<SpriteRenderer>().color = Color.cyan;
            } else {
                kv.Value.GetComponent<SpriteRenderer>().color = Color.yellow;
            }
        }
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
            newEnemy.GetComponent<Enemy>().SetId(id);

            enemies.Add(id, newEnemy);  
            yield return new WaitForSeconds(Random.Range(2, 5));
        }
    }

    private bool randomBool() {
        return Random.value > 0.5f;
    }
}
