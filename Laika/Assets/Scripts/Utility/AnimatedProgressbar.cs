using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityDLL;
using STLExtensiton;

[DisallowMultipleComponent]
public class AnimatedProgressbar : MonoBehaviour
{
    [SerializeField] private RawImage m_barUI = null;
    [SerializeField] private float animationFillRepeatSpeed = 0;
    [SerializeField] private float m_fillAmount = 0;
    [SerializeField] private float progressSpeed = 0;
    [SerializeField] bool isAnimationBreak = false;
    [SerializeField] bool isProgressBreak = false;

    private float aninmationProgressAmount = 0;

    private float m_initUvRectWidth;
    private bool m_isInit;

    public bool IsAnimationBreak
    {
        get { return isAnimationBreak; }
        set { isAnimationBreak = value; }
    }
    public bool IsProgressBreak
    {
        get { return isProgressBreak; }
        set { isProgressBreak = value; }
    }



    public float FillAmount
    {
        get { return m_fillAmount; }
        set
        {
            Init();

            m_fillAmount = Mathf.Clamp01(value);

            var localScale = m_barUI.rectTransform.localScale;
            localScale.x = m_fillAmount;
            m_barUI.rectTransform.localScale = localScale;

            var rect = m_barUI.uvRect;
            rect.width = m_initUvRectWidth * m_fillAmount;
            m_barUI.uvRect = rect;
        }
    }

    private void Init()
    {
        if (m_isInit)
            return;
        m_isInit = true;
        //if (!m_barUI)
        //    m_barUI = GetComponent<RawImage>();
        m_initUvRectWidth = m_barUI.uvRect.width;

        FillAmount = m_fillAmount;
    }

    private void Awake()
    {
        Init();
    }

    private IEnumerator Start()
    {
        while (aninmationProgressAmount < 1)
        {
            if (isProgressBreak)
                yield break;

            aninmationProgressAmount += progressSpeed;
            FillAmount = aninmationProgressAmount;
            yield return null;
        }
    }

    private void Update()
    {
        if (isAnimationBreak)
            return;

        var rect = m_barUI.uvRect;
        rect.x -= animationFillRepeatSpeed;
        m_barUI.uvRect = rect;
    }
}
