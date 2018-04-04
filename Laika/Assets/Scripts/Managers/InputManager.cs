
public partial class InputManager
{


    public static void Update()
    {
        Keyboard.Update();
        
    }
}

public enum eInputType
{
    MoveUpKeyDown,
    MoveUpKey,
    MoveUpKeyUp,
    MoveDownKeyDown,
    MoveDownKey,
    MoveDownKeyUp,
    MoveRightKeyDown,
    MoveRightKey,
    MoveRightKeyUp,
    MoveLeftKeyDown,
    MoveLeftKey,
    MoveLeftKeyUp,

    MoveRuKey,
    MoveRdKey,
    MoveLuKey,
    MoveLdKey,

    PauseKeyDown,
    PauseKey,
    PauseKeyUp,

    ShotAndDecideKeyDown,
    ShotAndDecideKey,
    ShotAndDecideKeyUp,

    BombAndCanselKeyDown,
    BombAndCanselKey,
    BombAndCanselKeyUp,

    SpeedChangeKeyDown,
    SpeedChangeKey,
    SpeedChangeKeyUp,
    

}
