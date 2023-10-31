using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;



public class PiecePicker : MonoBehaviour
{

    public TMP_Text myPlayer;

    public static bool clicked = false;

  
    // Used to set which piece the player chooses
    public void onClickPiecePick(int selectedPiece) {
        PlayerSetup.pieceValue = selectedPiece;
        
        if (PlayerInfo.playerInfo != null) {
            PlayerInfo.playerInfo.myPiece = selectedPiece;           
            
        }

        switch (selectedPiece)
            {
                case 1:
                    myPlayer.text = "My Player: DOG";
                    break;
                case 2:
                    myPlayer.text = "My Player: CAR";
                    break;
                case 3:
                    myPlayer.text = "My Player: BIN";
                    break;
                case 4:
                    myPlayer.text = "My Player: BOAT";
                    break;
            }

        print(selectedPiece);

    }


    

    


    

    

}
