using UnityEngine;

public class Shield : MonoBehaviour
{
    public string id {get; set;}

     private float rotateSpeed = 1f;
     private float radius;
 
     private Vector3 center;
     private float angle;
 
     private void Start()
     {
         center = new Vector3(0, 0, 1);
         radius = 100;
     }
 
     private void Update()
     {
         angle += rotateSpeed * Time.deltaTime;
         var offset = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 1) * radius;
         transform.position = center + offset;
         transform.Rotate(0, 0, Random.Range(0, 2), Space.Self);
     }
}
