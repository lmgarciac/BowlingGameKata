using System.Collections;
using System.Collections.Generic;

public class Frame
{
    private int frameKnockedPins;
    private int throwedRolls;
    private int frameTotalPoints;
    private bool frameIsSpare;
    private bool frameIsStrike;
    private int addedBonusRolls;

    public int ThrowedRolls { get => throwedRolls; }
    public int FrameTotalPoints { get => frameTotalPoints; }
    public bool FrameIsSpare { get => frameIsSpare; }
    public bool FrameIsStrike { get => frameIsStrike; }
    public int AddedBonusRolls { get => addedBonusRolls; }

    public void Roll(int rollKnockedPins = 0)
    {
        if (IsFrameCompleted()) return;
        throwedRolls++;

        frameKnockedPins += rollKnockedPins;
        frameTotalPoints += rollKnockedPins;

        frameIsStrike = CheckStrikeInFrame(rollKnockedPins);
        frameIsSpare = CheckSpareInFrame();
    }

    private bool CheckSpareInFrame()
    {
        return frameKnockedPins == 10 && frameIsStrike == false;
    }

    private bool CheckStrikeInFrame(int rollKnockedPins)
    {
        return rollKnockedPins == 10 && throwedRolls == 1;
    }

    public void AddFrameBonusPoint(int bonusPoint)
    {
        frameTotalPoints += bonusPoint;
        addedBonusRolls++;
    }
    public bool IsFrameCompleted()
    {
        return (frameIsStrike || throwedRolls == 2);
    }
}
