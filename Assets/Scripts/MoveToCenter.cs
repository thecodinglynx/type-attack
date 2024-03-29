using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToCenter : MonoBehaviour
{
    private float minSpeed = 20f;
    private float maxSpeed = 40f;

    [SerializeField]
    private float speed;
    private Vector3 screenCenter;

    void Start()
    {
        screenCenter = new Vector3(0, 0, 1);
        speed = Random.Range(minSpeed, maxSpeed);
    }

    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, screenCenter, step);

        // point towards spacestation/center
        var direction = screenCenter - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
