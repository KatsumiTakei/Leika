
using UnityEngine;

public class EnemyManager : GenericPoolManager<EnemyManager, Enemy>
{
    protected override string PrefabPath { get; set; } = ResourcesPath.Prefabs.Enemies.Path;

    protected override void OnEnable()
    {
        base.OnEnable();
        //PreloadAsync(eEnemyType.Small, 10, 10);
    }

    public void Create(Enemy type, GameObject owner)
    {
        var enemy = Rent(type);
        enemy.transform.parent = owner.transform;
        enemy.Initialize();

    }
}
