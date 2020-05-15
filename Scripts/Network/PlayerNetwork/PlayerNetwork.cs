using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerNetwork : MonoBehaviour
{
    public static PlayerNetwork instance;
    public string playerName;

    private int playersInGame;
    private PhotonView photonView;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        playerName = "Distule#" + Random.Range(0, 9999999);
        PhotonNetwork.player.NickName = playerName;

        PhotonNetwork.sendRate = 60;
        PhotonNetwork.sendRateOnSerialize = 30;

        photonView = GetComponent<PhotonView>();
        SceneManager.sceneLoaded += OnSceneFinishedLoaded;

    }
    
    private void OnSceneFinishedLoaded(Scene scene, LoadSceneMode mode)
    {
        
        if(scene.name == GameManager.instance.gameSceneName)
        {
            if (PhotonNetwork.isMasterClient)
            {
                MasterLoadedGame();
            }
            else
            {
                NonMasterLoadedGame();
            }
        }
        
    }

    
    private void MasterLoadedGame()
    {
        photonView.RPC("RPC_LoadedGameScene", PhotonTargets.MasterClient);
        photonView.RPC("RPC_LoadGameOthers", PhotonTargets.Others);
    }

    private void NonMasterLoadedGame()
    {
        photonView.RPC("RPC_LoadedGameScene", PhotonTargets.MasterClient);
    }
    
    [PunRPC]
    private void RPC_LoadGameOthers()
    {
        SceneManager.LoadScene(1);
    }

    [PunRPC]
    private void RPC_LoadedGameScene()
    {
        playersInGame++;
        if(playersInGame == PhotonNetwork.playerList.Length)
        {
            print("All players are in the game.");
            photonView.RPC("RPC_CreateNewPlayer", PhotonTargets.All);
        }
    }
    
    [PunRPC]
    private void RPC_CreateNewPlayer()
    {
        PhotonNetwork.Instantiate(Path.Combine("Prefabs", "NewPlayer"), Vector3.up * Random.Range(-5, 5), Quaternion.identity, 0);
    }
}
