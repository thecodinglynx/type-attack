using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private int health;
    private Color healthy = Color.green;
    private Color damaged = Color.red;

    [SerializeField]
    private List<GameObject> healthBars;

    void Start()
    {
        health = healthBars.Count;
    }

    // decreases health and returns the current health value
    public int DecreaseHealth() {
        health--;
        UpdateBars();
        return health;
    }

    public int IncreaseHealth() {
        health++;
        UpdateBars();
        return health;
    }

    private void UpdateBars() {
        for (int i=0; i<healthBars.Count; i++) {
            healthBars[i].GetComponent<SpriteRenderer>().color = i < health ? Color.green : Color.red;
        }
    }
}
