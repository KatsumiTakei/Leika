using System;
using UniRx;
using UnityDLL;
using UnityEngine;

public class ManualManipulator : ManipulatorBase
{
    short playerIndex = 0;

    public ManualManipulator(Player player, short playerIndex) : base(player)
    {
        EventManager.OnMultipleInput += OnMultipleInput;
        this.playerIndex = playerIndex;
    }

    ~ManualManipulator()
    {
        EventManager.OnMultipleInput -= OnMultipleInput;
    }

    void OnInput(eInputType inputType)
    {
        Attack(inputType);
        Move(inputType);
    }

    void OnMultipleInput(eInputType inputType, short playerIndex)
    {
        if (this.playerIndex != playerIndex)
            return;
        OnInput(inputType);
    }


    void Move(eInputType inputType)
    {
        switch (inputType)
        {
            case eInputType.SpeedChangeKeyDown:
                player.IsSpeedChange = true;
                break;
            case eInputType.SpeedChangeKeyUp:
                player.IsSpeedChange = false;
                break;
            case eInputType.MoveDownKey:
                player.Move(0, -1);
                break;
            case eInputType.MoveUpKey:
                player.Move(0, 1);
                break;
            case eInputType.MoveLeftKey:
                player.Move(-1, 0);
                break;
            case eInputType.MoveRightKey:
                player.Move(1, 0);
                break;
            case eInputType.MoveRuKey:
                player.Move(1, 1);
                break;
            case eInputType.MoveRdKey:
                player.Move(1, -1);
                break;
            case eInputType.MoveLuKey:
                player.Move(-1, 1);
                break;
            case eInputType.MoveLdKey:
                player.Move(-1, -1);
                break;
        }
    }


    void Attack(eInputType inputType)
    {
        switch (inputType)
        {
            case eInputType.BombAndCanselKeyUp:
                player.BroadcastShot.OnNext(eWeaponType.Charge);
                break;
            case eInputType.BombAndCanselKeyDown:
                Debug.Log("charge ...");
                break;
            case eInputType.ShotAndDecideKey:
                player.BroadcastShot.OnNext(eWeaponType.Normal);
                break;
        }
    }
}
