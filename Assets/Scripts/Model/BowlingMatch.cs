using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BowlingMatch
{
    public BowlingMatch(int matchFrames) //Esto no es requerido
    {
        this.matchFrames = matchFrames;
    }

    public int matchFrames; //¿¿Que hacer si solo un TEST es el que lee este campo que debería ser privado??
    public int pinsAmount = 10; //Esto no es requerido
    public List<Frame> frameList = new List<Frame>();
    public bool isLastFrame;

    private List<int> lastFrameBonusRolls = new List<int>();

    public void CalculateMatchPoints(List<int> rollSequence)
    {
        Frame currentFrame = new Frame();

        foreach (var knockedPins in rollSequence)
        {
            currentFrame.Roll(knockedPins);

            if (!IsFirstFrame())
            {
                if (isLastFrame)
                {
                    lastFrameBonusRolls.Add(knockedPins);
                    continue;
                }

                if (IsRollASpareBonus(currentFrame))
                {
                    frameList.Last().AddFrameBonusPoint(knockedPins);
                }

                if (IsRollAFirstStrikeBonus(currentFrame))
                {
                    frameList.Last().AddFrameBonusPoint(knockedPins);
                }

                if (frameList.Count >= 2)
                {
                    if (IsRollASecondStrikeBonus(currentFrame))
                    {
                        frameList[frameList.Count - 2].AddFrameBonusPoint(knockedPins);
                    }
                }
            }

            if (currentFrame.IsFrameCompleted())
            {
                frameList.Add(currentFrame);
                currentFrame = new Frame();

                if (IsLastFrame())
                {
                    isLastFrame = true;
                }
            }
        }

        if (lastFrameBonusRolls.Count != 0)
            AddLastBonusRolls();
    }

    private bool IsRollASpareBonus(Frame currentFrame)
    {
        return frameList.Last().FrameIsSpare && frameList.Last().AddedBonusRolls < 1;
    }

    private bool IsRollAFirstStrikeBonus(Frame currentFrame)
    {
        return frameList.Last().FrameIsStrike && frameList.Last().AddedBonusRolls < 2;
    }

    private bool IsRollASecondStrikeBonus(Frame currentFrame)
    {
        return frameList[frameList.Count - 2].FrameIsStrike && frameList[frameList.Count - 2].AddedBonusRolls < 2;
    }

    private bool IsLastFrame()
    {
        return frameList.Count == matchFrames;
    }

    private bool IsFirstFrame()
    {
        return frameList.Count == 0;
    }

    private void AddLastBonusRolls()
    {
        if (!IsFirstFrame())
        {
            if (frameList.Last().FrameIsSpare)
            {
                frameList.Last().AddFrameBonusPoint(lastFrameBonusRolls.Sum());
            }

            if (frameList.Last().FrameIsStrike)
            {
                frameList.Last().AddFrameBonusPoint(lastFrameBonusRolls.Sum());
            }

            if (frameList.Count >= 2)
            {
                if (frameList[frameList.Count - 2].FrameIsStrike)
                {
                    frameList[frameList.Count - 2].AddFrameBonusPoint(lastFrameBonusRolls.First());
                }
            }
        }
    }

    public int GetTotalScore()
    {
        int totalScore = 0;

        foreach (var frame in frameList)
        {
            totalScore += frame.FrameTotalPoints;
        }

        return totalScore;
    }
}

