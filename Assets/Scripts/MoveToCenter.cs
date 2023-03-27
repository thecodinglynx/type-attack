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

        Vector3 relativePos = transform.position- screenCenter;
        Vector3 desiredUp = new Vector3(relativePos.y, -relativePos.x, 0) * Mathf.Sign(-relativePos.x);
        Quaternion rotation = Quaternion.LookRotation (-Vector3.forward, desiredUp);   
        transform.rotation = rotation;
    }
}
