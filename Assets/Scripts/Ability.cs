using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ability : MonoBehaviour
{
    public KeyCode nextKey {get; set;}

    [SerializeField, InterfaceType(typeof(Action))]
    private UnityEngine.Object _action;
    public Action action => _action as Action;

    private string curWord;
    private int nextCharIdx = 0;
    private Text textDisplay;
    private static string GREEN = "#00FF00";
 
    public void Start() {
        this.textDisplay = GetComponentInParent<Text>();
    }

    public void SetCurrentWord(string curWord) {
        this.curWord = curWord;
        textDisplay.text = curWord;
        nextCharIdx = 0;
        SetNextKey();
    }

    // updates current word. Returns true if full word complete, false otherwise
    public bool UpdateWord() {
        textDisplay.text = string.Format("<color={0}>{1}</color>{2}", GREEN, curWord.Substring(0, nextCharIdx+1), curWord.Substring(nextCharIdx+1));
        nextCharIdx++;
        if (nextCharIdx >= curWord.Length) {
            return true;
        }
        SetNextKey();
        return false;
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


