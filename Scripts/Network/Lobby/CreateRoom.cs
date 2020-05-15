using UnityEngine;
using UnityEngine.UI;

public class CreateRoom : MonoBehaviour
{
    public InputField inputField;
    public Toggle isOpen;
    public Dropdown playerNumber;

    public void OnClickCreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions()
        {
            IsVisible = isOpen.isOn,
            IsOpen = isOpen.isOn,
            MaxPlayers = (byte) (playerNumber.value + 2)
        };

        if (PhotonNetwork.CreateRoom(inputField.text, roomOptions, TypedLobby.Default))
        {
            print("Create room successfully sent.");
            MainCanvasManager.instance.currentRoomCanvas.CreateRoomAsMaster();
            MainCanvasManager.instance.lobbyCanvas.gameObject.SetActive(false);
        }
        else
        {
            MainCanvasManager.instance.ShowErrorMassage("Create room failed to send");
        }
    }

    private void OnPhotonCreateRoomFailed(object[] codeAndMessage)
    {
        // MainCanvasManager.instance.ShowErrorMassage("create room failed: " + codeAndMessage[1]);
        print("create room failed: " + codeAndMessage[1]);
    }

    private void OnCreatedRoom()
    {
        print("The room was created successfully");
    }
}
