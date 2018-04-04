using STLExtensiton;
using UnityDLL;
using UnityEngine.EventSystems;
using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UniRx;
using System;

public class TitleMenu : MenuBase, ISelectHandler, IDeselectHandler
{
    #region Variable
    [SerializeField]
    TextMeshProUGUI discription;

    readonly string[] discriptionTexts = new string[4] {
        "事件を解決だ！",
        "ニクいあいつをぶっとばせ！",
        "操作方法を変えるよ",
        "おかえりになりま～す"
    };


    protected override Loop<int> SelectIndex { get; set; } = new Loop<int>(0, 3);
    protected override int PreIndex { get; set; } = new Loop<int>(0, 3);

    List<float> initButtonsPos = new List<float>();

    const float AnimTargetPos = 100;
    const float SelectedPos = 200;
    const float EaseDuration = 1.5f;
    #endregion Variable

    protected override void Start()
    {
        base.Start();
        buttons.Indexed().ForEach(p =>
        {
            initButtonsPos.Add(p.Element.GetComponent<RectTransform>().anchoredPosition.x);
            var element = p.Element.GetComponent<AnimatedProgressbar>();
            element.IsAnimationBreak = true;
            if(p.Index == 0)
                element.IsAnimationBreak = false;
        });
        discription.text = discriptionTexts[SelectIndex];
    }

    protected override void OnNext(int nextIndex)
    {
        base.OnNext(nextIndex);
        discription.text = discriptionTexts[SelectIndex];
    }

    #region ButtonEvent

    public void OnSelect(BaseEventData eventData)
    {
        var rectTrans = buttons[SelectIndex].GetComponent<RectTransform>();
        rectTrans.DOLocalMoveX(SelectedPos, MoveRequiredTime);
        rectTrans.DOScale(new Vector3(1.05f, 1.05f), MoveRequiredTime);
        buttons[SelectIndex].GetComponent<AnimatedProgressbar>().IsAnimationBreak = false;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        var rectTrans = buttons[PreIndex].GetComponent<RectTransform>();
        rectTrans.DOLocalMoveX(initButtonsPos[PreIndex], MoveRequiredTime);
        rectTrans.DOScale(new Vector3(1.0f, 1.0f), MoveRequiredTime);
        buttons[PreIndex].GetComponent<AnimatedProgressbar>().IsAnimationBreak = true;

    }

    public void StoryButton()
    {
        //Camera.main.transform.DOMoveX(16f, EaseDuration).SetEase(Ease.InOutBack).onComplete = () => 
        //{
        //    TitleScene.Instance.ChangeOperationCancas(eTitleCanvas.CharacterModeMenu);
        //};
        foreach(Transform child in transform)
        {
            child.DOMoveX(1600f, EaseDuration * 0.95f).SetEase(Ease.InOutBack);
        }

        Observable.Timer(TimeSpan.FromSeconds(EaseDuration))
            .Subscribe(p =>
        {
            TitleScene.Instance.ChangeOperationCancas(eTitleCanvas.CharacterModeMenu);
        });

        print("StoryButton");
    }

    public void ShowdownButton()
    {
        Camera.main.transform.DOMoveX(16f, EaseDuration).SetEase(Ease.InOutBack);
        foreach (Transform child in transform)
        {
            child.DOMoveX(1600f, EaseDuration * 0.95f).SetEase(Ease.InOutBack);
        }

        print("ShowdownButton");
    }

    public void ConfigButton()
    {
        print("ConfigButton");
    }

    public void QuitButton()
    {

#if UNITY_EDITOR
        print("Quit");
#else
        Application.Quit();
#endif
    }

    #endregion ButtonEvent
}
