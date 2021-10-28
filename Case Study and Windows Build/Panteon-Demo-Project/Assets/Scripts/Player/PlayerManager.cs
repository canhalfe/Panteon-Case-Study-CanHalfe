using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerState playerState;
    public enum PlayerState
    {
        Stop,
        Move
    }
}
