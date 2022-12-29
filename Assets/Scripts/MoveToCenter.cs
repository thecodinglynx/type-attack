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
        screenCenter = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0);
        speed = Random.Range(minSpeed, maxSpeed);
    }

    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, screenCenter, step);
    }
}
