using UnityEngine;

public class LobbyNetwork : MonoBehaviour
{
    public static LobbyNetwork instance;
    public int maxPlayer = 2;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        print("Try to connect ...");
        PhotonNetwork.ConnectUsingSettings("1.0.0");
        
    }

    public void OnConnectedToMaster()
    {
        PhotonNetwork.automaticallySyncScene = true;
        PhotonNetwork.playerName = PlayerNetwork.instance.playerName;
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }
    
    private void OnJoinedLobby()
    {
        
    }

}
