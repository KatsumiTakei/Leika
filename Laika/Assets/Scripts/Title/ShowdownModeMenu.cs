using System.Linq;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using STLExtensiton;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShowdownModeMenu : MenuBase, ISelectHandler, IDeselectHandler
{
    struct RectTransformCopy
    {
        public Vector3 position;
        public Vector3 localScale;

        public RectTransformCopy(RectTransform rectTransform)
        {
            position = rectTransform.position;
            localScale = rectTransform.localScale;
        }
    }

    #region     variable

    [SerializeField]
    TextMeshProUGUI discription;

    protected override Loop<int> SelectIndex { get; set; } = new Loop<int>(0, 1);
    protected override int PreIndex { get; set; } = new Loop<int>(0, 1);

    readonly string[] discriptionTexts = new string[] {
        "生きている人間同士で争います",
        "見えない人との闘いです",
    };

    List<RectTransformCopy> initTransforms = new List<RectTransformCopy>();
    Image corsol;

    #endregion  variable

    #region     Event

    private void Awake()
    {
        if (initTransforms.Any())
            return;

        corsol = GetComponentInChildren<Image>();
        var childrenRectTransform = GetComponentsInChildren<RectTransform>();
        childrenRectTransform.ForEach(p =>
        {
            initTransforms.Add(new RectTransformCopy(p));
        });

    }

    protected override void OnEnable()
    {
        base.OnEnable();
        var childrenRectTransform = GetComponentsInChildren<RectTransform>();
        childrenRectTransform.Indexed().ForEach(p =>
        {
            var element = p.Element;
            element.DOScale(initTransforms[p.Index].localScale, MoveRequiredTime);
            element.DOMove(initTransforms[p.Index].position, MoveRequiredTime);

        });

    }

    protected override void OnDisable()
    {
        base.OnDisable();
        var childrenRectTransform = GetComponentsInChildren<RectTransform>();
        childrenRectTransform.ForEach(p =>
        {
            p.DOScale(new Vector3(0f, 0f), MoveRequiredTime);
            p.DOMoveY(-450f, MoveRequiredTime);

        });

    }

    protected override void Start()
    {
        base.Start();
        discription.text = discriptionTexts[SelectIndex];
        buttons[SelectIndex].GetComponentInChildren<TextMeshProUGUI>().alpha = 1f;
        corsol.transform.DOMoveY(buttons[SelectIndex].transform.position.y, 0f);

        transform.parent.GetComponent<Canvas>().gameObject.SetActive(false);
    }

    protected override void OnNext(int nextIndex)
    {
        base.OnNext(nextIndex);
        discription.text = discriptionTexts[SelectIndex];
        buttons[PreIndex].GetComponentInChildren<TextMeshProUGUI>().alpha = buttons[SelectIndex].GetComponentInChildren<TextMeshProUGUI>().alpha;
        buttons[SelectIndex].GetComponentInChildren<TextMeshProUGUI>().alpha = 1f;

        corsol.transform.DOMoveY(buttons[SelectIndex].transform.position.y, MoveRequiredTime);

    }

    public void OnSelect(BaseEventData eventData)
    {
        
    }

    public void OnDeselect(BaseEventData eventData)
    {
        
    }
    #endregion  Event

}
