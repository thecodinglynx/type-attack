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
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "SpaceStation") {
            DamageSpaceStation();
        }
        Die();
    }

    private void DamageSpaceStation() {
        EventManager.TriggerEvent(EventManager.Event.SPACESTATION_ATTACKED, new Dictionary<string, object> { { "id", 1 } });
    }

    private void Die() {
        gameObject.GetComponent<Renderer>().enabled = false;
        EventManager.TriggerEvent(EventManager.Event.ENEMY_DESTROYED, new Dictionary<string, object> { { "id", id } });
        var ps = GetComponent<ParticleSystem>();
        ps.Play();
        Destroy(gameObject, ps.main.duration);
    }
}
