using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class DiceNumberTextScript : MonoBehaviour {

    Text text;
    public static int diceNumber;


    // Start is called before the first frame update
    void Start() {
        // Initialized the canvas game object
        GameObject canvasGo = new GameObject("Canvas");

        text = canvasGo.AddComponent<Text> ();
    }


    // Update is called once per frame
    void Update() {
        text.text = diceNumber.ToString ();
    }
}
