
using UnityEngine;

public class WeaponManager : GenericPoolManager<WeaponManager, Weapon>
{
    protected override string PrefabPath { get; set; } = ResourcesPath.Prefabs.Weapons.Path;

    protected override void OnEnable()
    {
        base.OnEnable();
        //PreloadAsync(eWeaponType.Jamming, 50, 50);
    }

    public void Create(Weapon type, GameObject owner)
    {
        var weapon = Rent(type);
        weapon.transform.parent = owner.transform;
        weapon.transform.position = new Vector3(owner.transform.position.x, owner.transform.position.y + weapon.Spd, 0);
    }

}
