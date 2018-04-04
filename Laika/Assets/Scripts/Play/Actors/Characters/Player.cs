using STLExtensiton;
using UnityDLL;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

public enum eWeaponType
{
    Normal,
    Jamming,
    Charge,
    Max,
}

public abstract class Player : Character
{
    private ManipulatorBase manipulator;
    private SpriteRenderer spriteRenderer;

    const float AlphaStrong = 0.5f;
    const float AlphaWeak = 0.25f;

    short chargeLevel = 0;

    protected bool isInvincible = false;


    public float DiagonalSpd { get; set; }
    public bool IsSpeedChange { get; set; } = false;
    public Weapon[] Weapons { get; set; } = new Weapon[(int)eWeaponType.Max];
    public Subject<eWeaponType> BroadcastShot{get;set;} = new Subject<eWeaponType>();


    #region private

    private void LoadWeapon(string characterName)
    {
        var resourcePath = ResourcesPath.Prefabs.Weapons.Path + characterName;
        var lWeapons = Resources.LoadAll<Weapon>(resourcePath);
        lWeapons.Indexed().ForEach(p => { Weapons[p.Index] = p.Element; });
    }

    public void Move(int x, int y)
    {
        var spd = (x != 0 && y != 0) ? DiagonalSpd : Spd;
        var val = new Vector2(x, y) * spd * ((IsSpeedChange) ? 0.5f : 1f);
        transform.AddPositionXY(val);
        OutStageRange();
    }

    #endregion private

    public override void OutStageRange()
    {
        transform.SetLocalPositionX(Mathf.Clamp(transform.localPosition.x, StageRangeMin.x, StageRangeMax.x));
        transform.SetLocalPositionY(Mathf.Clamp(transform.localPosition.y, StageRangeMin.y, StageRangeMax.y));
    }

    protected override void Destory()
    {
        //  TODO    :   implement explosion particle
        //ParticleManager.Instance.Play();
        GameObject.Destroy(gameObject);
    }

    protected override void TakeDamage()
    {
        //  TODO    :   implement hit se
        //AudioManager.PlaySE(ResourcesPath.Audio.SE);

        if (--Life < 0)
        {
            Destory();
            return;
        }
        isInvincible = true;

        Observable.Interval(TimeSpan.FromSeconds(0.1))
            .TakeUntil(Observable.Timer(TimeSpan.FromSeconds(3)))
            .Subscribe(p =>
        {
            float alpha = (spriteRenderer.color.a > AlphaWeak) ? AlphaWeak : AlphaStrong;
            spriteRenderer.color = new Color(1, 1, 1, alpha);
        });

        Observable.Timer(TimeSpan.FromSeconds(3))
            .Subscribe(p => 
        {
            spriteRenderer.color = new Color(1, 1, 1, 1);
            isInvincible = false;
        });
    }

    protected virtual void Initialize(string characterName)
    {
        Initialize();
        LoadWeapon(characterName);
    }

    public override void Initialize()
    {
        Spd = 4;
        DiagonalSpd = Spd / Mathf.Sqrt(2);
        Life = 3;
    }

    #region Event

    protected virtual void Start()
    {// TODO    :   モードによってManualかaiか決める
        manipulator = new ManualManipulator(this, 0);
        spriteRenderer = GetComponent<SpriteRenderer>();

        BroadcastShot
            .ThrottleFirst(TimeSpan.FromMilliseconds(4))
            .Subscribe(p => { WeaponManager.Instance.Create(Weapons[(int)eWeaponType.Normal], gameObject); });
    }

    protected virtual void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isInvincible)
            return;

        if (!collision.gameObject.GetComponent<Enemy>())// || collision.gameObject.GetComponent<Enemy>())
            return;

        TakeDamage();
    }

    #endregion Event
}