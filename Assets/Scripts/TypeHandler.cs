using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeHandler : MonoBehaviour
{
    [SerializeField]
    private List<Ability> abilities;

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
        foreach(Ability cur in abilities) {
            cur.SetCurrentWord(getRandomWord());
        }
    }

    void Update()
    {
        foreach (Ability ability in abilities) {
            if (Input.anyKeyDown && Input.GetKeyDown(ability.nextKey)) {
                ability.action.perform();
                if (ability.UpdateWord()) {
                    ability.SetCurrentWord(getRandomWord());
                }
            } 
        }
    }

    private string getRandomWord() {
        return WORDS[UnityEngine.Random.Range(0, WORDS.Count)];
    }
}
