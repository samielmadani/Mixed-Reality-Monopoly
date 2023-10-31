using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class MonopolyGame : MonoBehaviour
{
        
    private bool isPlayerMoving = false;
    private int diceResult = 0;

    // Old dice roll function. Was going to have a 3d dice that rolls on user screen with gravity but doesnt work given phone orientation effects the roll.
    public void RollDice()
    {
        if (!isPlayerMoving)
        {
            diceResult = Random.Range(1, 7);
        }
    }

    // Exit game functionality

    public void DisconnectPlayer()
    {
        StartCoroutine(DisconnectAndLoad());
    }

    IEnumerator DisconnectAndLoad()
    {
        PhotonNetwork.Disconnect();
        while (PhotonNetwork.IsConnected)
            yield return null;
        SceneManager.LoadScene(0);
    }

}

