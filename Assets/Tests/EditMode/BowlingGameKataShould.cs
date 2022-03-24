using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using System.Linq;

public class BowlingGameKataShould
{
    // NOTAS//
    //Agregar al Readme del proyecto que significa cada cosa (Frame, Roll, Pin, etc.)

    //1° Requerimiento: Cada partida se compone de 10 turnos
    //Tiempo Estimado: 5min
    //Tiempo Real: 5min

    [UnityTest]
    public IEnumerator HaveOnly10Frames()
    {
        //Arrange 
        yield return null;
        BowlingMatch bowlingMatch = new BowlingMatch(10);

        //Act

        //Assert
        Assert.AreEqual(10, bowlingMatch.matchFrames);

    }

    //2° Requerimiento: Hay 10 bolos que se intentan tirar en cada turno
    //Tiempo Estimado: 10min
    //Tiempo Real: 7min

    [UnityTest]
    public IEnumerator HaveOnly10Pins()
    {
        //Arrange
        yield return null;
        BowlingMatch bowlingMatch = new BowlingMatch(10);
        //Act

        //Assert
        Assert.AreEqual(10, bowlingMatch.pinsAmount);
    }

    //3° Requerimiento: En cada turno el jugador hace 2 tiradas
    //Estimado: 15min
    //Real: 15min

    [UnityTest]
    public IEnumerator Have2RollsForEveryFrame()
    {
        //Arrange
        yield return null;
        Frame frame = new Frame();
        
        //Act

        //Each frame has two rolls
        frame.Roll();
        frame.Roll();

        //Assert
        Assert.AreEqual(2, frame.throwedRolls);
    }

    //4° Requerimiento: Si en un turno el jugador no tira los 10 bolos, la puntuacion del turno es el total de bolos tirados
    //Estimado: 20min
    //Real: 40min

    [UnityTest]

    public IEnumerator PointsGivenInFrameIsSumOfPinesKnockedIfLessThan10PinsKnocked()
    {
        //Arrange
        yield return null;
        Frame frame = new Frame();

        //Act
        frame.Roll(4);
        frame.Roll(2);

        //Assert
        Assert.AreEqual(6, frame.framePoints);

    }

    //5° Requerimiento: Si en un turno el jugador tira los 10 bolos (un "spare"),
    //la puntuacion es 10 mas el numero de bolos tirados en la siguiente tirada (Del siguiente turno).
    //Estimado: 30min
    //Real: 70min

    [UnityTest]

    public IEnumerator PointsGivenInFrameIs10PlusNextRollKnockedPinesIfSpare()
    {
        //Arrange
        yield return null;
        BowlingMatch bowlingMatch = new BowlingMatch(10);
        List<int> rollSequence = new List<int>{ 5,5,2 };

        //Act
        bowlingMatch.CalculateMatchPoints(rollSequence); 

        //Assert
        Assert.AreEqual(12, bowlingMatch.frameList.First().framePoints);

    }

    //6° Requerimiento: Si en la primer tirada del turno tira los 10 bolos (un strike)
    //el turno acaba y la puntuacion es 10 mas el numero de bolos de las dos tiradas siguientes.
    //Estimado: 30min
    //Real: 

    [UnityTest]

    public IEnumerator Points_Given_In_Frame_Is_10_Plus_Next_Two_Rolls_Knocked_Pines_If_Strike()
    {
        //Arrange
        yield return null;
        BowlingMatch bowlingMatch = new BowlingMatch(10);
        List<int> rollSequence = new List<int> { 10, 4, 2 };

        //Act
        bowlingMatch.CalculateMatchPoints(rollSequence);

        //Assert
        Assert.AreEqual(16, bowlingMatch.frameList.First().framePoints);

    }


}

public class BowlingMatch
{
    public BowlingMatch(int matchFrames)
    {
        this.matchFrames = matchFrames;
        this.frameList = new List<Frame>();
    }
    //Borrar todas las variables que no se usen
    public int matchFrames;
    public int pinsAmount = 10;
    public int throwedRolls;
    public int knockPins;
    public List<Frame> frameList = new List<Frame>();

    public void CalculateMatchPoints(List<int> rollSequence) //  11min  22max  10 
    {
        Frame currentFrame = new Frame();

        foreach (var knockedPins in rollSequence)
        {    
            currentFrame.Roll(knockedPins);
            
            if (currentFrame.IsFrameEnded())
            {
                frameList.Add(currentFrame);
                currentFrame = new Frame();
                continue;
            }
            if (frameList.Count == 0) continue;

            if (frameList.Last().isSpare)
            {
                frameList.Last().AddBonusPoint(knockedPins);
            }

            if (frameList.Last().isStrike)
            {
                frameList.Last().AddBonusPoint(knockedPins);
            }
        }
    }

}

public class Frame
{
    //puntos , rolls , pinos tirados 
    public int throwedRolls;
    public int frameKnockedPins;
    public int framePoints;
    public bool isSpare;
    public bool isStrike;

    public void Roll(int rollKnockedPins = 0)
    {
       
        if (IsFrameEnded()) return;
        throwedRolls++;
        
        frameKnockedPins += rollKnockedPins;
        framePoints += rollKnockedPins;

        isStrike = CheckStrike(rollKnockedPins);
        isSpare = CheckSpare();
    }

    public bool IsFrameEnded()
    {
        return (isStrike || throwedRolls == 2);
    }

    private bool CheckStrike(int rollKnockedPins)
    {
        return rollKnockedPins == 10 && throwedRolls == 1;
    }

    private bool CheckSpare()
    {
        return frameKnockedPins == 10 && isStrike == false;
    }

    public void AddBonusPoint(int bonusPoint)
    {
        framePoints += bonusPoint;
    }
}
//Arrange

//Act

//Assert