using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ePlayerType
{
	Leika,
	Max
}

public enum eHIDType
{
    Keyboard,
    Pad1,
    Pad2,
}


public interface IPlayerData
{
    //  decide by config
    eHIDType HidType { get; set; }
    short PlayerIndex { get; set; }
    short Remain { get; set; }
    //  decide by character select
    ePlayerType PlayerType { get; set; }
    short InitialLife { get; set; }
}

public class LeftPlayerData : IPlayerData
{
    //  decide by config
    public eHIDType HidType { get; set; } = eHIDType.Keyboard;
    public short PlayerIndex { get; set; } = 1;
    public short Remain { get; set; } = 2;
    //  decide by character select
    public ePlayerType PlayerType { get; set; } = ePlayerType.Leika;
    public short InitialLife { get; set; } = 5;

}

public class RightPlayerData : IPlayerData
{
    //  decide by config
    public eHIDType HidType { get; set; } = eHIDType.Pad1;
    public short PlayerIndex { get; set; } = 2;
    public short Remain { get; set; } = 2;
    //  decide by character select
    public ePlayerType PlayerType { get; set; } = ePlayerType.Leika;
    public short InitialLife { get; set; } = 5;

}