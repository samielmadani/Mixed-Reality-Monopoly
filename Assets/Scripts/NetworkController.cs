using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkController : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {

        PhotonNetwork.ConnectUsingSettings();
        
    }

    // public override void OnPlayerEnteredRoom(Player newPlayer)
    // {
    //     if (PhotonNetwork.IsMasterClient)
    //     {
    //         // Assign a unique ID for the new player based on the player count
    //         int newPlayerID = MonopolyGame.instance.players.Count + 1;

    //         // Add the new player to the game
    //         MonopolyGame.instance.AddPlayerToGame(newPlayer.NickName, newPlayerID);
    //     }
    // }

    public override void OnConnectedToMaster()
    {
        Debug.Log("We are connected to " + PhotonNetwork.CloudRegion);
    }

}
