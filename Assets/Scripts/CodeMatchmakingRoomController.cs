using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
public class CodeMatchmakingRoomController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject joinButton;
    [SerializeField]
    private Text playerCount;
    [SerializeField]
    private Text playerCount2;

    public override void OnJoinedRoom()//called when the local player joins the room
    {
        joinButton.SetActive(false);
        playerCount.text = PhotonNetwork.PlayerList.Length + " Players" ;
        playerCount2.text = PhotonNetwork.PlayerList.Length + " Players";
    }
    public override void OnPlayerEnteredRoom(Player newPlayer) //called whenever a new player enter the room
    {
        playerCount.text = PhotonNetwork.PlayerList.Length + " Players";
        playerCount2.text = PhotonNetwork.PlayerList.Length + " Players";
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        playerCount.text = PhotonNetwork.PlayerList.Length + " Players";
        playerCount2.text = PhotonNetwork.PlayerList.Length + " Players";
    }
    public override void OnLeftRoom()
    {
        playerCount.text =  "0 Players";
        playerCount2.text =  "0 Players";
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log(message);
    }
    public void StartGameOnClick()
    {
        PhotonNetwork.LoadLevel(1);
    }
 
}