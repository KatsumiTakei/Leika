using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerDataManager
{
    static LeftPlayerData leftPlayerData;
    static RightPlayerData rightPlayerData;

    static readonly ePlayerType[] playerTypes = new ePlayerType[2]{
        ePlayerType.Leika,
        ePlayerType.Max
    };

    public static ePlayerType GetPlayerType(int selectIndex)
    {
        Debug.Assert(playerTypes.Length > selectIndex, "length over");
        return playerTypes[selectIndex];
    }

}
