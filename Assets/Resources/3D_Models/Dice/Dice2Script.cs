using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice2Script : MonoBehaviour
{
    static Rigidbody rigidbody;
    public static Vector3 diceVelocity;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        diceVelocity = rigidbody.velocity;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            DiceNumberTextScript.diceNumber = 0;
            float dirX = Random.Range(0, 500);
            float dirY = Random.Range(0, 500);
            float dirZ = Random.Range(0, 500);
            transform.position = new Vector3(1, 3, 0);
            transform.rotation = Quaternion.identity;
            rigidbody.AddForce(transform.up * 500);
            rigidbody.AddTorque(dirX, dirY, dirZ);
        }
    }
}
