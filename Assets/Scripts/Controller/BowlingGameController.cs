using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingGameController : MonoBehaviour
{
    [SerializeField] private ScoreBoardController scoreBoardController;

    BowlingMatch bowlingMatch;
    private List<int> rollSequence = new List<int>();

    public void CalculateMatchPoints() 
    {
        bowlingMatch = new BowlingMatch(10);
        rollSequence.Clear();
        foreach (var frameController in scoreBoardController.frameControllers)
        {
            if (frameController.firstRoll.text != "-")
            {
                rollSequence.Add(int.Parse(frameController.firstRoll.text));
            }
            if (frameController.secondRoll.text != "-")
            {
                rollSequence.Add(int.Parse(frameController.secondRoll.text));
            }          
        }
        
        bowlingMatch.CalculateFramesPoints(rollSequence);
        scoreBoardController.inputFieldTotal.text = bowlingMatch.GetTotalScore().ToString();
        scoreBoardController.UpdateFrameScores(bowlingMatch.frameList);
    }

    public void ClearValues()
    {
        foreach (var frameController in scoreBoardController.frameControllers)
        {
            frameController.firstRoll.text = "-";
            frameController.secondRoll.text = "-";
            frameController.frameScore.text = "-";
        }
        scoreBoardController.inputFieldTotal.text = "-";
    }
}
