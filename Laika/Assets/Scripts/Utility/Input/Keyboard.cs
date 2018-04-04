using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class InputManager
{
    static class Keyboard
    {
        #region variable
        static KeyCode shotAndDecide = KeyCode.Z;
        static KeyCode bombAndCansel = KeyCode.X;
        static KeyCode moveRight = KeyCode.RightArrow;
        static KeyCode moveLeft = KeyCode.LeftArrow;
        static KeyCode moveUp = KeyCode.UpArrow;
        static KeyCode moveDown = KeyCode.DownArrow;
        static KeyCode pause = KeyCode.Escape;
        static KeyCode speedChange = KeyCode.LeftShift;
        #endregion variable

        public static short PlayerIndex { get; set; } = 0;//    HACK


        #region private
        static void KeyDown(eInputType inputType, params KeyCode[] keys)
        {
            bool release = keys.Any(p => !Input.GetKeyDown(p));
            if (release)
                return;
            EventManager.BroadcastMultipleInput(inputType, PlayerIndex);
        }

        static void KeyUp(eInputType inputType, params KeyCode[] keys)
        {
            bool release = keys.Any(p => !Input.GetKeyUp(p));
            if (release)
                return;
            EventManager.BroadcastMultipleInput(inputType, PlayerIndex);

        }
        static void Key(eInputType inputType, params KeyCode[] keys)
        {
            bool release = keys.Any(p => !Input.GetKey(p));
            if (release)
                return;
            EventManager.BroadcastMultipleInput(inputType, PlayerIndex);
        }
        #endregion private
        #region public

        public static void Update()
        {
            Key(eInputType.ShotAndDecideKey, shotAndDecide);
            KeyDown(eInputType.ShotAndDecideKeyDown, shotAndDecide);
            KeyUp(eInputType.ShotAndDecideKeyUp, shotAndDecide);

            KeyDown(eInputType.SpeedChangeKeyDown, speedChange);
            KeyUp(eInputType.SpeedChangeKeyUp, speedChange);

            MoveKeys();

            KeyDown(eInputType.PauseKeyDown, pause);
            KeyDown(eInputType.BombAndCanselKeyDown, bombAndCansel);
        }
        public static void ConfigKey()
        {

        }

        static void MoveKeys()
        {
            Key(eInputType.MoveRuKey, moveRight, moveUp);
            Key(eInputType.MoveRdKey, moveRight, moveDown);
            Key(eInputType.MoveLuKey, moveLeft, moveUp);
            Key(eInputType.MoveLdKey, moveLeft, moveDown);

            Key(eInputType.MoveRightKey, moveRight);
            Key(eInputType.MoveLeftKey, moveLeft);
            Key(eInputType.MoveUpKey, moveUp);
            Key(eInputType.MoveDownKey, moveDown);
        }

        #endregion public

    }
}