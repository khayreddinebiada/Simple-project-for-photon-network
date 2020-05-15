using UnityEngine;
using UnityEngine.UI;

public class PlayerListing : MonoBehaviour
{
    public PhotonPlayer photonPlayer;
    public Text playerName;

    public void ApplyPhotonPlayer(PhotonPlayer pPlayer)
    {
        playerName.text = pPlayer.NickName;
        photonPlayer = pPlayer;
    }
}
