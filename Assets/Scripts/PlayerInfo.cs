using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;


public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo playerInfo;
    public int myPiece;

    private void onEnable() {
        if (PlayerInfo.playerInfo == null) {
            PlayerInfo.playerInfo = this;
        } else {
            if (PlayerInfo.playerInfo != this) {
                PlayerInfo.playerInfo = this;
            }
        }
    }
    

    //Initialises and sets piece the player chose
    void Start() {

        if (PlayerPrefs.HasKey("SelectedPiece")) {
            myPiece = PlayerPrefs.GetInt("SelectedPiece");
        } else {
            myPiece = 0;
            PlayerPrefs.SetInt("SelectedPiece", myPiece);
        }

        if(PhotonNetwork.IsMasterClient)
        {
            for(int i = 0; i < PhotonNetwork.PlayerList.Length; i ++)
            {
                print(i);
            }
        }

    }
    
}
