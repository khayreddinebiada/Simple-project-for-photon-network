using UnityEngine;
using System.Collections.Generic;

public class PlayerLayoutGroup : MonoBehaviour
{
    public GameObject playerListingPrefab;
    public int playersNumber;
    private List<PlayerListing> playerListings = new List<PlayerListing>();
    
    void Update()
    {
        if(playersNumber != PhotonNetwork.playerList.Length)
        {

            foreach (PlayerListing pListing in playerListings)
            {
                Destroy(pListing.gameObject);
            }
            playerListings.Clear();

            PhotonPlayer[] photonPlayers = PhotonNetwork.playerList;
            for (int i = 0; i < photonPlayers.Length; i++)
            {
                PlayerJoinedRoom(photonPlayers[i]);
            }

            playersNumber = PhotonNetwork.playerList.Length;
        }
    }
    
    /*
    private void OnJoinedRoom()
    {
        foreach (PlayerListing pListing in playerListings)
        {
            Destroy(pListing.gameObject);
        }
        playerListings.Clear();

        PhotonPlayer[] photonPlayers = PhotonNetwork.playerList;
        for (int i = 0; i < photonPlayers.Length; i++)
        {
            PlayerJoinedRoom(photonPlayers[i]);
        }

    }
    
    private void OnPhotonPlayerConnected(PhotonPlayer photonPlayer)
    {
        PlayerJoinedRoom(photonPlayer);
    }
    */
    private void OnPhotonDisconnected(PhotonPlayer photonPlayer)
    {
        PlayerLeftRoom(photonPlayer);
        MainCanvasManager.instance.SetLobbyCanvasActivate();
    }

    private void PlayerJoinedRoom(PhotonPlayer photonPlayer)
    {

        if (photonPlayer == null)
            return;

        PlayerLeftRoom(photonPlayer);

        GameObject pListingObg = Instantiate(playerListingPrefab);
        pListingObg.transform.SetParent(transform, false);

        PlayerListing pListing = pListingObg.GetComponent<PlayerListing>();
        pListing.ApplyPhotonPlayer(photonPlayer);

        playerListings.Add(pListing );
    }

    private void PlayerLeftRoom(PhotonPlayer photonPlayer)
    {
        int index = playerListings.FindIndex(x => x.photonPlayer == photonPlayer);
        if (index != -1)
        {
            Destroy(playerListings[index].gameObject);
            playerListings.RemoveAt(index);
        }
    }

    public void OnClickLeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        MainCanvasManager.instance.SetLobbyCanvasActivate();
    }
}
