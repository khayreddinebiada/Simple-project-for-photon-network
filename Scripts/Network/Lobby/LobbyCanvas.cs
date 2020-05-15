using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyCanvas : MonoBehaviour
{
    public RoomLayoutGroup roomLayoutGroup;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnClickJoinRoom(string roomName)
    {
        if (PhotonNetwork.JoinRoom(roomName))
        {
            MainCanvasManager.instance.SetCurrentRoomCanvasActivate();
        }
        else
        {
            MainCanvasManager.instance.ShowErrorMassage("You can't join this room...");
            print("You can't join this room...");
        }
    }
    
}
