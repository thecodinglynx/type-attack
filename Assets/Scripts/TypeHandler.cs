using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeHandler : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> abilityPrefabs;

    private List<Ability> abilities = new List<Ability>();

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
        if (abilities.Count > 0) {
            return;
        }

        var parent = GameObject.FindGameObjectWithTag("Canvas").transform;
        foreach(var cur in abilityPrefabs) {
            var curInstance = GameObject.Instantiate(
                cur, 
                new Vector3(10, 0, 0), 
                Quaternion.identity
            );
            curInstance.transform.SetParent(parent, false);

            RectTransform uitransform = curInstance.GetComponent<RectTransform>();
            uitransform.anchorMin = new Vector2(0, 1);
            uitransform.anchorMax = new Vector2(0, 1);
            uitransform.pivot = new Vector2(0, 1);

            abilities.Add(curInstance.GetComponent<Ability>());
        }

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

    void OnDestroy() {
        foreach (Ability ability in abilities) {
            Destroy(ability.gameObject);
        }
    }

    private string getRandomWord() {
        return WORDS[UnityEngine.Random.Range(0, WORDS.Count)];
    }
}
