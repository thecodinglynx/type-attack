using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy;
    private float minX, maxX, minY, maxY;
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
        screenCenter = new Vector2(0, 0);
        minX = screenCenter.x - (Screen.width * 0.5f) - 20;
        maxX = screenCenter.x + (Screen.width * 0.5f) + 20;
        minY = screenCenter.y + (Screen.height * 0.5f) + 20;
        maxY = screenCenter.y - (Screen.height * 0.5f) - 20;

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

    // returns closest enemy, returned object may be null if none on the screen.
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
        if (closest == null) {
            return;
        }
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
            var newEnemy = Instantiate(enemy, new Vector3(x, y, 1), Quaternion.identity);
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
