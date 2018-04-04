using System;

public static class EventManager
{
    public static event Action<eInputType, short> OnMultipleInput;
    public static void BroadcastMultipleInput(eInputType argInput, short playerIndex)
    {
        OnMultipleInput?.Invoke(argInput, playerIndex);
    }

    public static event Action OnApplicationQuit;
    public static void BroadcastApplicationQuit()
    {
        OnApplicationQuit?.Invoke();
        OnApplicationQuit = null;
    }

}
