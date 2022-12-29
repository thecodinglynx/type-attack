using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject text;

    private List<KeyCode> keys = new List<KeyCode>{
        KeyCode.A,
        KeyCode.B,
        KeyCode.C,
        KeyCode.D,
        KeyCode.E,
        KeyCode.F,
        KeyCode.G,
        KeyCode.H,
        KeyCode.I,
        KeyCode.J,
        KeyCode.K,
        KeyCode.L,
        KeyCode.M,
        KeyCode.N,
        KeyCode.O,
        KeyCode.P,
        KeyCode.Q,
        KeyCode.R,
        KeyCode.S,
        KeyCode.T,
        KeyCode.U,
        KeyCode.V,
        KeyCode.W,
        KeyCode.X,
        KeyCode.Y,
        KeyCode.Z,
    };

    private string curWord;

    private List<string> words = new List<string>{
        "hello",
        "benji",
        "test",
        "fj"
    };

    void Start()
    {
        curWord = words[Random.Range(0, words.Count)];
        text.GetComponent<Text>().text = curWord;
        Debug.Log("Hello: " + curWord);
    }

    void Update()
    {
        if (Input.anyKeyDown) {
            foreach (KeyCode key in keys) {
                if (Input.GetKeyDown(key)) {
                    Debug.Log(key);
                }
            }
        }
    }


}
