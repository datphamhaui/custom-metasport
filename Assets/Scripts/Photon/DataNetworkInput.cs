using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DataNetworkInput : INetworkInput
{
    public Vector2 move;
    public Vector2 look;
    public bool jump;
    public bool sprint;
}