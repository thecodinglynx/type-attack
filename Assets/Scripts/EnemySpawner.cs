using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy;
    private float minX, maxX, minY, maxY;
    private Vector2 screenCenter;

    private Dictionary<string, GameObject> enemies = new Dictionary<string, GameObject>();

    public static EnemySpawner Instance { get; private set; }

    void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 

        screenCenter = new Vector2(0, 0);
        minX = screenCenter.x - (Screen.width * 0.5f);
        maxX = screenCenter.x + (Screen.width * 0.5f);
        minY = screenCenter.y + (Screen.height * 0.5f);
        maxY = screenCenter.y - (Screen.height * 0.5f);
    }

    void OnEnable()
    {
        EventManager.StartListening(EventManager.Event.ENEMY_DESTROYED, OnEnemyDestroyed);
        foreach(KeyValuePair<string, GameObject> kv in enemies) {
            kv.Value.SetActive(true);
        }
        
        StartCoroutine("InstantiateEnemy");
    }

    void OnDisable()
    {
        foreach(KeyValuePair<string, GameObject> kv in enemies) {
            kv.Value.SetActive(false);
        }
        EventManager.StopListening(EventManager.Event.ENEMY_DESTROYED, OnEnemyDestroyed);
    }

    void Update() 
    {
        MarkClosestEnemy();
    }

    void OnDestroy() {
        foreach(KeyValuePair<string, GameObject> kv in enemies) {
            Destroy(kv.Value);
        }
        enemies = new Dictionary<string, GameObject>();
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
            if (kv.Key == closest.GetComponent<Enemy>().id) {
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
            newEnemy.GetComponent<Enemy>().id = id;
            enemies.Add(id, newEnemy);  
            newEnemy.transform.SetParent(transform, false);
            yield return new WaitForSeconds(Random.Range(2, 5));
        }
    }

    private bool randomBool() {
        return Random.value > 0.5f;
    }
}
