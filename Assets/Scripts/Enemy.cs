using UnityEngine;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    public string id {get; set;}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet") {
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "SpaceStation") {
            DamageSpaceStation();
        }
        if (collision.gameObject.tag == "Shield") {
            DestroyShield(collision.gameObject);
            Destroy(collision.gameObject);
        }
        Die();
    }

    private void DamageSpaceStation() {
        EventManager.TriggerEvent(EventManager.Event.SPACESTATION_ATTACKED, new Dictionary<string, object> { { "id", 1 } });
    }

    private void DestroyShield(GameObject shield) {
        EventManager.TriggerEvent(EventManager.Event.SHIELD_DESTROYED, new Dictionary<string, object> { { "id", shield.GetComponent<Shield>().id } });
    }

    private void Die() {
        gameObject.GetComponent<Renderer>().enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = false;
        EventManager.TriggerEvent(EventManager.Event.ENEMY_DESTROYED, new Dictionary<string, object> { { "id", id } });
        var ps = GetComponent<ParticleSystem>();
        ps.Play();
        Destroy(gameObject, ps.main.duration);
    }
}
