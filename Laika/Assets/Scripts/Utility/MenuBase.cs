using STLExtensiton;
using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public abstract class MenuBase : MonoBehaviour
{
    protected const float MoveRequiredTime = 0.3f;
    protected List<Button> buttons;
    protected Subject<int> broadcastSelect = new Subject<int>();

    protected abstract Loop<int> SelectIndex { get; set; }
    protected abstract int PreIndex { get; set; }

    public void OnActivateMenu()
    {
        buttons[PreIndex]?.Select();
    }

    protected virtual void OnMultipleInput(eInputType inputType, short playerIndex)
    {
        switch (inputType)
        {
            case eInputType.MoveDownKey:
                broadcastSelect.OnNext(1);
                break;
            case eInputType.MoveUpKey:
                broadcastSelect.OnNext(-1);
                break;
            case eInputType.ShotAndDecideKeyDown:
                AudioManager.PlaySE(ResourcesPath.Audio.SE._Decide);
                buttons[SelectIndex].onClick?.Invoke();
                break;
        }

    }

    protected virtual void OnEnable()
    {
        EventManager.OnMultipleInput += OnMultipleInput;
    }

    protected virtual void OnDisable()
    {
        EventManager.OnMultipleInput -= OnMultipleInput;
    }

    protected virtual void Start()
    {
        buttons = GetComponentsInChildren<Button>().ToList();
        buttons[0].Select();
        broadcastSelect
            .ThrottleFirst(TimeSpan.FromSeconds(MoveRequiredTime))
            .Subscribe(p => OnNext(p));
        PreIndex = SelectIndex;
    }

    protected virtual void OnNext(int nextIndex)
    {
        AudioManager.PlaySE(ResourcesPath.Audio.SE._CursolMove);

        PreIndex = SelectIndex;
        SelectIndex += nextIndex;
        buttons[SelectIndex].Select();
    }
}
