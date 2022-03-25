using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingGameController : MonoBehaviour
{
    BowlingMatch bowlingMatch;
    [SerializeField] private ScoreBoardController scoreBoardController;
    private List<int> entryRolls = new List<int>();

    public void CalculateMatchPoints() 
    {
        bowlingMatch = new BowlingMatch(10);
        entryRolls.Clear();
        foreach (var frameController in scoreBoardController.framesControllers)
        {

            if (frameController.firstRoll.text != "-")
            {
                entryRolls.Add(int.Parse(frameController.firstRoll.text));
            }
            if (frameController.secondRoll.text != "-")
            {
                entryRolls.Add(int.Parse(frameController.secondRoll.text));
            }
            
        }
        
        bowlingMatch.CalculateMatchPoints(entryRolls);

        scoreBoardController.inputFieldTotal.text = bowlingMatch.GetTotalScore().ToString();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
