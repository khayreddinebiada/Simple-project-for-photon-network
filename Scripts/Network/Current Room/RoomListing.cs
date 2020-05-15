using UnityEngine;
using UnityEngine.UI;

public class RoomListing : MonoBehaviour
{
    public Button joinButton;
    public string roomName;
    public Text textRoomName;
    public int playerNumber;
    public bool isUpdated = false;

    // Start is called before the first frame update
    void Start()
    {
        LobbyCanvas lobbyCanvas = MainCanvasManager.instance.lobbyCanvas.gameObject.GetComponent<LobbyCanvas>();

        if (lobbyCanvas == null)
            return;

        joinButton.onClick.AddListener(() => lobbyCanvas.OnClickJoinRoom(textRoomName.text));
    }
    
    public void ApplyForRoom(string text, int pNumber)
    {
        textRoomName.text = roomName = text;
        playerNumber = pNumber;
    }
}
