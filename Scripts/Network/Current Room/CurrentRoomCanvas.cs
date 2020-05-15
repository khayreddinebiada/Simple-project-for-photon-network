using UnityEngine;
using UnityEngine.UI;

public class CurrentRoomCanvas : MonoBehaviour
{
    public GameObject masterPanel;
    public GameObject nonMasterPanel;

    public Toggle toggleVisible;
    
    public void OnClickRoomState()
    {
        PhotonNetwork.room.IsVisible = toggleVisible.isOn;
    }

    public void OnClickStartSceneSync(int indexScene)
    {
        /*
        If game is we want to close the room after the game start we will remove the coments.
        PhotonNetwork.room.IsOpen = false;
        PhotonNetwork.room.IsVisible = false;
        */
        PhotonNetwork.LoadLevel(indexScene);
    }
    public void JoinRoomAsNonMaster()
    {
        gameObject.SetActive(true);
        masterPanel.SetActive(false);
        nonMasterPanel.SetActive(true);
    }

    public void CreateRoomAsMaster()
    {
        gameObject.SetActive(true);
        masterPanel.SetActive(true);
        nonMasterPanel.SetActive(false);
    }

}
