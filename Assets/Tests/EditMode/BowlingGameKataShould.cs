using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using System.Linq;

public class BowlingGameKataShould
{
    //1° Requerimiento: Cada partida se compone de 10 turnos
    //Tiempo Estimado: 5min
    //Tiempo Real: 5min

    [UnityTest]
    public IEnumerator Have_Only_10_Frames()
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
    public IEnumerator Have_Only_10_Pins()
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
    public IEnumerator Have_2_Rolls_For_Every_Frame()
    {
        //Arrange
        yield return null;
        Frame frame = new Frame();

        //Act

        //Each frame has two rolls
        frame.Roll();
        frame.Roll();

        //Assert
        Assert.AreEqual(2, frame.ThrowedRolls);
    }

    //4° Requerimiento: Si en un turno el jugador no tira los 10 bolos, la puntuacion del turno es el total de bolos tirados
    //Estimado: 20min
    //Real: 40min

    [UnityTest]

    public IEnumerator Points_Given_In_Frame_Is_Sum_Of_Pines_Knocked_If_Less_Than_10_Pins_Knocked()
    {
        //Arrange
        yield return null;
        Frame frame = new Frame();

        //Act
        frame.Roll(4);
        frame.Roll(2);

        //Assert
        Assert.AreEqual(6, frame.FrameTotalPoints);

    }

    //5° Requerimiento: Si en un turno el jugador tira los 10 bolos (un "spare"),
    //la puntuacion es 10 mas el numero de bolos tirados en la siguiente tirada (Del siguiente turno).
    //Estimado: 30min
    //Real: 70min

    [UnityTest]

    public IEnumerator Points_Given_In_Frame_Is_10_Plus_Next_Roll_Knocked_Pines_If_Spare()
    {
        //Arrange
        yield return null;
        BowlingMatch bowlingMatch = new BowlingMatch(10);
        List<int> rollSequence = new List<int> { 5, 5, 2 };

        //Act
        bowlingMatch.CalculateMatchPoints(rollSequence);

        //Assert
        Assert.AreEqual(12, bowlingMatch.frameList.First().FrameTotalPoints);

    }

    //6° Requerimiento: Si en la primer tirada del turno tira los 10 bolos (un strike)
    //el turno acaba y la puntuacion es 10 mas el numero de bolos de las dos tiradas siguientes.
    //Estimado: 30min
    //Real: 25min

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
        Assert.AreEqual(16, bowlingMatch.frameList.First().FrameTotalPoints);

    }

    //7° Verificar si estamos en el ultimo Frame
    //Estimado: 30min
    //Real: 

    [UnityTest]

    public IEnumerator Verify_If_Arrived_To_Last_Frame()
    {
        //Arrange
        yield return null;
        BowlingMatch bowlingMatch = new BowlingMatch(10);
        List<int> rollSequence = new List<int> { 0, 1, 2, 3, 4, 5, 0, 1, 2, 3, 4, 5, 0, 1, 2, 3, 0, 1, 10, 3, 5 };

        //Act
        bowlingMatch.CalculateMatchPoints(rollSequence);

        //Assert
        Assert.IsTrue(bowlingMatch.isLastFrame);
    }

    //7° Requerimiento: Si el jugador logra un strike en el ultimo turno,
    //obtiene dos tiradas mas de bonificacion. Esas Tiradas cuentan como parte del mismo turno (el decimo).
    //Estimado: 30min
    //Real: 21min

    [UnityTest]

    public IEnumerator Have_Two_Additional_Rolls_If_Strike_In_Last_Frame()
    {
        //Arrange
        yield return null;
        BowlingMatch bowlingMatch = new BowlingMatch(10);
        List<int> rollSequence = new List<int> { 0, 1, 2, 3, 4, 5, 0, 1, 2, 3, 4, 5, 0, 1, 2, 3, 0, 1, 10, 3, 5 };

        //Act
        bowlingMatch.CalculateMatchPoints(rollSequence);

        //Assert
        Assert.AreEqual(18, bowlingMatch.frameList.Last().FrameTotalPoints);

    }

    //7° Requerimiento: Si el jugador logra un spareen el ultimo turno,
    //obtiene una tirada mas de bonificacion. Esa Tirada cuenta como parte del mismo turno (el decimo).
    //Estimado: 10min
    //Real: 5min

    [UnityTest]

    public IEnumerator Have_One_Additional_Rolls_If_Spare_In_Last_Frame()
    {
        //Arrange
        yield return null;
        BowlingMatch bowlingMatch = new BowlingMatch(10);
        List<int> rollSequence = new List<int> { 0, 1, 2, 3, 4, 5, 0, 1, 2, 3, 4, 5, 0, 1, 2, 3, 0, 1, 6, 4, 3 };

        //Act
        bowlingMatch.CalculateMatchPoints(rollSequence);

        //Assert
        Assert.AreEqual(13, bowlingMatch.frameList.Last().FrameTotalPoints);

    }

    //8° Requerimiento: Si en las tiradas de bonificación el jugador derriba todos los bolos
    //el proceso no se repite, no se vuelven a generar mas lanzamientos de bonificación.
    //Estimado: 5min
    //Real: 5min

    [UnityTest]

    public IEnumerator Not_Provide_Bonus_Rolls_If_All_Pins_Knocked_In_Bonus_Rolls()
    {
        //Arrange
        yield return null;
        BowlingMatch bowlingMatch = new BowlingMatch(10);
        List<int> rollSequence = new List<int> { 0, 1, 2, 3, 4, 5, 0, 1, 2, 3, 4, 5, 0, 1, 2, 3, 0, 1, 6, 4, 10 };

        //Act
        bowlingMatch.CalculateMatchPoints(rollSequence);

        //Assert
        Assert.AreEqual(20, bowlingMatch.frameList.Last().FrameTotalPoints);
    }

    // Escenarios de Test Completos:

    [UnityTest]

    public IEnumerator Return_Correct_Value_If_All_Zeroes()
    {
        yield return null;
        BowlingMatch bowlingMatch = new BowlingMatch(10);
        List<int> rollSequence = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        //Act
        bowlingMatch.CalculateMatchPoints(rollSequence);

        //Assert
        Assert.AreEqual(0, bowlingMatch.GetTotalScore());
    }

    [UnityTest]

    public IEnumerator Return_Correct_Value_If_All_Strikes()
    {
        //Arrange
        yield return null;
        BowlingMatch bowlingMatch = new BowlingMatch(10);
        List<int> rollSequence = new List<int> { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };

        //Act
        bowlingMatch.CalculateMatchPoints(rollSequence);

        //Assert
        Assert.AreEqual(300, bowlingMatch.GetTotalScore());
    }

    [UnityTest]

    public IEnumerator Return_Correct_Value_If_All_Spares()
    {
        //Arrange
        yield return null;
        BowlingMatch bowlingMatch = new BowlingMatch(10);
        List<int> rollSequence = new List<int> { 5, 5, 6, 4, 3, 7, 1, 9, 2, 8, 3, 7, 4, 6, 2, 8, 8, 2, 1, 9, 5 };

        //Act
        bowlingMatch.CalculateMatchPoints(rollSequence);

        //Assert
        Assert.AreEqual(135, bowlingMatch.GetTotalScore());
    }

    [UnityTest]

    public IEnumerator Return_Correct_Value_If_No_Strikes_Or_Spares()
    {
        //Arrange
        yield return null;
        BowlingMatch bowlingMatch = new BowlingMatch(10);
        List<int> rollSequence = new List<int> { 3, 2, 5, 1, 4, 2, 7, 1, 4, 2, 0, 8, 1, 7, 1, 0, 0, 1, 3, 2 };

        //Act
        bowlingMatch.CalculateMatchPoints(rollSequence);

        //Assert
        Assert.AreEqual(54, bowlingMatch.GetTotalScore());
    }

    [UnityTest]

    public IEnumerator Return_Correct_Value_If_Mixed_Strikes_And_Spares()
    {
        //Arrange
        yield return null;
        BowlingMatch bowlingMatch = new BowlingMatch(10);
        List<int> rollSequence = new List<int> { 5, 5, 10, 10, 3, 7, 1, 9, 10, 8, 2, 10, 10, 10, 5, 5 };

        //Act
        bowlingMatch.CalculateMatchPoints(rollSequence);

        //Assert
        Assert.AreEqual(209, bowlingMatch.GetTotalScore());
    }

    [UnityTest]
    public IEnumerator Return_Correct_Value_For_Mixed_Test_Suite()
    {
        //Arrange
        yield return null;

        foreach (var rollSequence in mixedTestSuiteDataSource)
        {
            BowlingMatch bowlingMatch = new BowlingMatch(10);

            //Act
            bowlingMatch.CalculateMatchPoints(rollSequence.Item1);

            //Assert
            Assert.AreEqual(rollSequence.Item2, bowlingMatch.GetTotalScore());
        }
    }

    List<(List<int>, int)> mixedTestSuiteDataSource = new List<(List<int>, int)>
    {
        (new List<int> {5,5,4,5,8,2,10,0,10,10,6,2,10,4,6,10,10,0}, 169),
        (new List<int> {5,5,4,0,8,1,10,0,10,10,10,10,4,6,10,10,5}, 186),
        (new List<int> {10,10,10,10,10,10,10,10,10,10,10,2}, 292),
        (new List<int> {8,2,10,10,10,10,10,10,10,10,10,10,7}, 287),
        (new List<int> {8,0,7,0,5,3,9,1,9,1,10,8,0,5,1,3,7,9,0}, 122),
        (new List<int> {8,2,9,0,4,4,7,2,9,0,10,10,8,0,3,5,9,1,7}, 133),
        (new List<int> {7,0,6,3,8,2,6,4,10,10,7,3,7,2,8,2,7,3,9}, 161),
        (new List<int> {9,1,8,1,9,1,8,2,10,10,10,9,1,10,10,10,9}, 223),
        (new List<int> {10,10,9,0,9,1,10,7,3,8,1,10,10,8,1}, 180),
    };
}
