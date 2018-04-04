
using UnityEngine;

public abstract class Actor : MonoBehaviour
{
    public float Spd { get; set; }
    protected static Vector2 StageRangeMin { get; set; }
    protected static Vector2 StageRangeMax { get; set; }
    private const float RangeAdjust = 20;

    public abstract void OutStageRange();
    public abstract void Initialize();

    public static void SetStageRange(Vector2 min, Vector2 max)
    {
        StageRangeMin = min;
        StageRangeMax = max;
    }

    protected bool IsInStageRange()
    {
        return ((StageRangeMin.x - RangeAdjust < transform.localPosition.x && transform.localPosition.x < StageRangeMax.x + RangeAdjust) &&
            (StageRangeMin.y - RangeAdjust < transform.localPosition.y && transform.localPosition.y < StageRangeMax.y + RangeAdjust));
    }

}
