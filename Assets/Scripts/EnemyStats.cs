using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    private string id;

    public void SetId(string id) {
        this.id = id;
    }

    public string GetId() {
        return id;
    }
}
