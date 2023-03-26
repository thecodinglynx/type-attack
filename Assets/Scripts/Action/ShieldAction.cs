using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldAction : MonoBehaviour, Action
{
    [SerializeField]
    private GameObject shield;

    private Dictionary<string, GameObject> shields;

    void OnEnable()
    {
        EventManager.StartListening(EventManager.Event.SHIELD_DESTROYED, OnShieldDestroyed);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventManager.Event.SHIELD_DESTROYED, OnShieldDestroyed);
    }

    public void perform()
    {
        if (shields == null) {
            shields = new Dictionary<string, GameObject>();
        }
        var newShield = Instantiate(shield, new Vector3(0, 0, 0), Quaternion.identity);
        var id = System.Guid.NewGuid().ToString();
        newShield.GetComponent<Shield>().id = id;
        shields.Add(id, newShield);

        GameObject spaceStation = GameObject.FindGameObjectWithTag("SpaceStation");
        newShield.transform.SetParent(spaceStation.transform, true);
    }

    void OnShieldDestroyed(Dictionary<string, object> message)
    {
        var id = (string)message["id"];
        shields.Remove(id);
    }
}
