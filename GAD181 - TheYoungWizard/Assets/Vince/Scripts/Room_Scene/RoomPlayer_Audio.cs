using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPlayer_Audio : MonoBehaviour
{
    [SerializeField] RoomScene_Player player;

    public void PlayFootSteps()
    {
        player.PlayFootSteps();
    }
}
