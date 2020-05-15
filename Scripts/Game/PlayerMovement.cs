using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Photon.MonoBehaviour
{
    public float movingSpeed;
    public Vector3 targetPosition;
    public Quaternion targetRotation;

    private PhotonView photonView;
    // Start is called before the first frame update
    void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }


    // Update is called once per frame
    void Update()
    {

        if (photonView.isMine)
            MovingLeftRight();
        else
            SmoothMove();

    }

    private void SmoothMove()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, 0.25f);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 500);
    }

    private void OnPhotonSerializeView(PhotonStream steam, PhotonMessageInfo info)
    {
        if (steam.isWriting)
        {
            steam.SendNext(transform.position);
            steam.SendNext(transform.rotation);
        }
        else
        {
            targetPosition = (Vector3)steam.ReceiveNext();
            targetRotation = (Quaternion)steam.ReceiveNext();
        }
    }

    void MovingLeftRight()
    {
        float horizontal = Input.GetAxis("Horizontal");

        transform.position += transform.right * horizontal * movingSpeed * Time.fixedDeltaTime;
    }
}
