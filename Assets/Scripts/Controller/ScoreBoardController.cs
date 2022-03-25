using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreBoardController : MonoBehaviour
{
    public List<FrameController> frameControllers = new List<FrameController>();
    public TMP_InputField inputFieldTotal;

    public void UpdateFrameScores(List<Frame> calculatedFrames)
    {
        for (int i = 0; i < calculatedFrames.Count; i++)
        {
            frameControllers[i].frameScore.text = calculatedFrames[i].FrameTotalPoints.ToString();
        }
    }
}
