using STLExtensiton;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CharacterSelectCursol : MonoBehaviour
{

    int moveRequiredTime = 300;
    List<Image> characterIcons = new List<Image>();
    Subject<int> broadcastSelect = new Subject<int>();

    Loop<int> SelectIndex { get; set; } = new Loop<int>(0, 1);

    public Action<int> broadcastDecide;

    void OnMultipleInput(eInputType inputType, short playerIndex)
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
                //  TODO    :   
                broadcastDecide(SelectIndex);
                break;
        }

    }

    void OnNext(int person)
    {// TODO    :   implement add se

        //iTween.ColorTo(characterIcons[SelectIndex].gameObject, new Color(0.5f, 0.5f, 0.5f, 0.5f), moveRequiredTime);
        SelectIndex += person;
        //iTween.ColorTo(characterIcons[SelectIndex].gameObject, Color.white, moveRequiredTime);
        gameObject.transform.DOMoveX(characterIcons[SelectIndex].transform.position.x, moveRequiredTime);
    }

    void Start()
    {
        broadcastSelect
            .ThrottleFirst(TimeSpan.FromMilliseconds(moveRequiredTime))
            .Subscribe(p => OnNext(p));
    }

    void OnEnable()
    {
        EventManager.OnMultipleInput += OnMultipleInput;
    }
    void OnDisable()
    {
        EventManager.OnMultipleInput -= OnMultipleInput;
    }

}
