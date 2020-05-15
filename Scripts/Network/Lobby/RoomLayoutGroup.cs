using System.Collections.Generic;
using UnityEngine;
public class RoomLayoutGroup : MonoBehaviour
{
    public static List<RoomListing> roomListingButtons = new List<RoomListing>();
    public GameObject RoomListingPrefab;
    // Start is called before the first frame update
    void Awake()
    {
        roomListingButtons = new List<RoomListing>();
    }

    // Update is called once update on the rooms list.
    void OnReceivedRoomListUpdate()
    {
        RoomInfo[] roomsinfo = PhotonNetwork.GetRoomList();

        foreach (RoomInfo roominfo in roomsinfo)
        {
            RoomReceived(roominfo);
        }

        RemoveOldRoom();
    }

    private void RoomReceived(RoomInfo room)
    {
        int index = roomListingButtons.FindIndex(x => x.roomName == room.Name);
        if (index == -1)
        {
            if (room.IsVisible && room.PlayerCount < room.MaxPlayers) // For check if the room is Visible and Player is not on the Max number.
            {
                GameObject roomListingObject = Instantiate(RoomListingPrefab);
                roomListingObject.transform.SetParent(transform, false);

                RoomListing roomListing = roomListingObject.GetComponent<RoomListing>();
                roomListingButtons.Add(roomListing);

                index = roomListingButtons.Count - 1;
            }
        }
        if(index != -1)
        {
            RoomListing roomListing = roomListingButtons[index];
            roomListing.ApplyForRoom(room.Name, room.PlayerCount);
            roomListing.isUpdated = true;
        }
    }

    private void RemoveOldRoom()
    {
        List<RoomListing> removeListing = new List<RoomListing>();

        foreach (RoomListing room in roomListingButtons)
        {
            if (!room.isUpdated || LobbyNetwork.instance.maxPlayer <= room.playerNumber)
            {
                removeListing.Add(room);
            }
            else
            {
                room.isUpdated = false;
            }
        }

        foreach(RoomListing room in removeListing)
        {
            GameObject roomListingObj = room.gameObject;
            roomListingButtons.Remove(room);
            Destroy(roomListingObj);
        }
    }
}
