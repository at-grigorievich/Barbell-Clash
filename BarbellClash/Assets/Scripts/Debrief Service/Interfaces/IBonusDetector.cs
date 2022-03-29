using ATG.LevelControl;

namespace Debrief
{
    public interface IBonusDetector
    {
        int FinishBlockIndex { get; }
        BonusBlock TargetPoint { get; }
    }
}