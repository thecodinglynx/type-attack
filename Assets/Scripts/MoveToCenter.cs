using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToCenter : MonoBehaviour
{
    private float minSpeed = 20f;
    private float maxSpeed = 40f;

    [SerializeField]
    private float speed;
    private Vector2 screenCenter;

    void Start()
    {
        screenCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        speed = Random.Range(minSpeed, maxSpeed);
    }

    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, screenCenter, step);
    }
}
