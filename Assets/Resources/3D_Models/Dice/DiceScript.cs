using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DiceScript : MonoBehaviour {

    static Rigidbody rigibody;
    public static Vector3 diceVelocity;

    // Initialises teh rigid body
    void Start() {
        rigibody = GetComponent<Rigidbody> ();
    }

    // Update is called once per frame
    void Update() {
        diceVelocity = rigibody.velocity;

        if (Input.GetKeyDown (KeyCode.Space)) {
            DiceNumberTextScript.diceNumber = 0;
            float dirX = Random.Range (0, 500);
            float dirY = Random.Range (0, 500);
            float dirZ = Random.Range (0, 500);
            transform.position = new Vector3 (-1, 3, 0);
            transform.rotation = Quaternion.identity;
            rigibody.AddForce (transform.up * 500);
            rigibody.AddTorque (dirX, dirY, dirZ);
        }
    }
}
