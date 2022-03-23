using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

public class BowlingGameKataShould
{

    [UnityTest]

    public IEnumerator InitialTest()
    {
        yield return null;
        Assert.AreEqual(true, true);
    }

    //Cada partida se compone de 10 turnos

    [UnityTest]

    public IEnumerator HaveOnly10Turns()
    {
        //Arrange 
        yield return null;
        BowlingMatch bowlingMatch = new BowlingMatch(10);

        //Act

        //Assert
        Assert.AreEqual(bowlingMatch.matchFrames, 10);

    }

    //Hay 10 bolos que se intentan tirar en cada turno
    //Estimado 10m, real 7m

    [UnityTest]

    public IEnumerator HaveOnly10Pins()
    {
        //Arrange
        yield return null;
        BowlingMatch bowlingMatch = new BowlingMatch(10);
        //Act

        //Assert
        Assert.AreEqual(bowlingMatch.pinsAmount, 10);
    }

    //En cada turno el jugador hace 2 tiradas
    //Estimado 15 ,

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
        Assert.AreEqual(frame.throwedRolls, 2);
    }

    //Si en un turno el jugador no tira los 10 bolos, la puntuacion del turno es el total de bolos tirados
    //Estimado 20

    [UnityTest]

    public IEnumerator PointsGivenIsSumOfPinesKnockedIfLessThan10PinsKnocked()
    {
        //Arrange
        yield return null;
        Frame frame = new Frame();
        int framePoints;

        //Act
        frame.knockedPins = 5;
        framePoints = frame.CalculatePoints();

        //Assert
        Assert.AreEqual(framePoints, 5);

    }

    //PointsGivenSumOfPinesKnockedIfLessThan10PinsKnocked


}

public class BowlingMatch
{
    public BowlingMatch(int matchFrames)
    {
        this.matchFrames = matchFrames;
        this.frameList = new List<Frame>();
    }

    public int matchFrames;
    public int pinsAmount = 10;
    public int throwedRolls;
    public int knockPins;
    public List<Frame> frameList;

}

public class Frame
{
    //puntos , rolls , pinos tirados 
    public int throwedRolls;
    public int knockedPins;
    public int framePoints;


    public void Roll()
    {

        throwedRolls++;

    }

    public int CalculatePoints()
    {
        if(knockedPins < 10)
        {
            return knockedPins;
        }
        return 0;
    }
}
//Arrange

//Act

//Assert