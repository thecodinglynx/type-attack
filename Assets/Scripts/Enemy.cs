using UnityEngine;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    private string id;

    public void SetId(string id) {
        this.id = id;
    }

    public string GetId() {
        return id;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet") {
            EventManager.TriggerEvent(EventManager.Event.ENEMY_DESTROYED, new Dictionary<string, object> { { "id", id } });
            Destroy(gameObject);
        }
    }
}
