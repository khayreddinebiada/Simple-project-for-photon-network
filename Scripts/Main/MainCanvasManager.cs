using UnityEngine;
using UnityEngine.UI;

public class MainCanvasManager : MonoBehaviour
{
    public static MainCanvasManager instance;
    public LobbyCanvas lobbyCanvas;
    public CurrentRoomCanvas currentRoomCanvas;
    
    public Text errorTextShow;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }
    
    public void SetLobbyCanvasActivate()
    {
        lobbyCanvas.gameObject.SetActive(true);
        currentRoomCanvas.gameObject.SetActive(false);
    }

    public void SetCurrentRoomCanvasActivate()
    {
        lobbyCanvas.gameObject.SetActive(false);
        currentRoomCanvas.JoinRoomAsNonMaster();
    }

    public void ShowErrorMassage(string massage)
    {
        errorTextShow.text = massage;
    }
}
