using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceStation : MonoBehaviour
{
    [SerializeField]
    private float rotateSpeed = 1f;

    private Health healthBar;

    void Start()
    {
        var children = gameObject.GetComponentsInChildren<Transform>();
        healthBar = System.Array.Find(children, p => p.gameObject.name == "HealthBar").GetComponent<Health>();
    }

    void Update() 
    {
        transform.Rotate(0f, 0f, -rotateSpeed * Time.deltaTime);
    }

    void OnEnable()
    {
        EventManager.StartListening(EventManager.Event.SPACESTATION_ATTACKED, onSpaceStationAttacked);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventManager.Event.SPACESTATION_ATTACKED, onSpaceStationAttacked);
    }

    private void onSpaceStationAttacked(Dictionary<string, object> message) {
        if (healthBar.DecreaseHealth() <= 0) {
            Destroyed();
            // TODO: show gameover screen
        }
    }

    private void Destroyed() {
        gameObject.GetComponent<Renderer>().enabled = false;
        var ps = GetComponent<ParticleSystem>();
        ps.Play();
        Destroy(gameObject, ps.main.duration);
    }
}
