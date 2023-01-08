using UnityEngine;

public class Boundary : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Bullet") {
            Destroy(other.gameObject);
        }
    } 
}
