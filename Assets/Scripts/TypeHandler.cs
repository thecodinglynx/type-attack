using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject text;

    [SerializeField]
    private Defender defender;

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
    private int nextCharIdx = 0;
    private KeyCode nextKey;
    private Text textDisplay;

    private static string GREEN = "#00FF00";

    private static List<string> WORDS = new List<string>()
    {
        "happy",
        "love",
        "kind",
        "fun",
        "play",
        "smile",
        "friend",
        "beautiful",
        "bright",
        "cheerful",
        "cute",
        "delightful",
        "enthusiastic",
        "friendly",
        "generous",
        "gentle",
        "joyful",
        "lively",
        "merry",
        "optimistic",
        "playful",
        "pleasant",
        "positive",
        "proud",
        "pure",
        "radiant",
        "refreshing",
        "simple",
        "soothing",
        "sunny",
        "thoughtful",
        "tender",
        "trusting",
        "warm",
        "whimsical",
        "blissful",
        "cheery",
        "content",
        "cozy",
        "cuddly",
        "dreamy",
        "easygoing",
        "fantastical",
        "fluffy",
        "glamorous",
        "gracious",
        "heavenly"
    };

    void Start()
    {
        textDisplay = text.GetComponent<Text>();
        ShowNextWord();
    }

    void Update()
    {
        if (Input.anyKeyDown && Input.GetKeyDown(nextKey)) {
            textDisplay.text = string.Format("<color={0}>{1}</color>{2}", GREEN, curWord.Substring(0, nextCharIdx+1), curWord.Substring(nextCharIdx+1));
            defender.ShootAtClosest();
            nextCharIdx++;
            if (nextCharIdx >= curWord.Length) {
                ShowNextWord();
            } else {
                SetNextKey();
            }
        }   
    }

    private void ShowNextWord() {
        nextCharIdx = 0;
        curWord = WORDS[UnityEngine.Random.Range(0, WORDS.Count)];
        textDisplay.text = curWord;
        SetNextKey();
    }

    private void SetNextKey() {
        try {
            nextKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), curWord[nextCharIdx].ToString().ToUpper());
        }
        catch (ArgumentException err) {
            Debug.LogWarning(string.Format("Invalid key: {0}, {1}", curWord[nextCharIdx], err));
        }
    }
}
