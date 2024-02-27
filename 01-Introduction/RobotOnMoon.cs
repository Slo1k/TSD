using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;


public class RobotOnMoon
{
public string isSafeCommand(string[] board, string S)
{
    bool isAlive = true;
    int robotX = -1, robotY = -1;
    
    for (int i = 0; i < board.Length && !((robotX >= 0) && (robotY >= 0)); i++)
    {
        for (int j = 0; j < board[i].Length; j++)
        {
            if (board[i][j] == 'S')
            {
                robotX = i;
                robotY = j;
                break;
            }
        }
    }
	
    foreach (char command in S)
    {
        switch (command)
        {
            case 'U':
                if ((robotX > 0) && (board[robotX - 1][robotY] != '#'))
                    robotX--;
				else if (robotX <= 0)
					isAlive = false;
                break;
            case 'D':
                if ((robotX + 1 < board.Length) && (board[robotX + 1][robotY] != '#'))
                    robotX++;
				else if (robotX + 1 >= board.Length)
					isAlive = false;
                break;
            case 'L':
                if ((robotY > 0) && (board[robotX][robotY - 1] != '#'))
                    robotY--;
				else if (robotY <= 0)
					isAlive = false;
                break;
            case 'R':
                if ((robotY + 1 < board[robotX].Length) && (board[robotX][robotY + 1] != '#'))
                    robotY++;
				else if (robotY + 1 >= board[robotX].Length)
					isAlive = false;
                break;
            default:
                throw new ArgumentException("Invalid command.");
        }
        
        if (!((robotX >= 0) && (robotX < board.Length) && (robotY >= 0) && (robotY < board[robotX].Length)))
        {
            isAlive = false;
            break;
        }
    }

    if (isAlive)
        return "Alive";
    else
        return "Dead";
	}

    #region Testing code

    [STAThread]
    private static Boolean KawigiEdit_RunTest(int testNum, string[] p0, string p1, Boolean hasAnswer, string p2)
    {
        Console.Write("Test " + testNum + ": [" + "{");
        for (int i = 0; p0.Length > i; ++i)
        {
            if (i > 0)
            {
                Console.Write(",");
            }
            Console.Write("\"" + p0[i] + "\"");
        }
        Console.Write("}" + "," + "\"" + p1 + "\"");
        Console.WriteLine("]");
        RobotOnMoon obj;
        string answer;
        obj = new RobotOnMoon();
        DateTime startTime = DateTime.Now;
        answer = obj.isSafeCommand(p0, p1);
        DateTime endTime = DateTime.Now;
        Boolean res;
        res = true;
        Console.WriteLine("Time: " + (endTime - startTime).TotalSeconds + " seconds");
        if (hasAnswer)
        {
            Console.WriteLine("Desired answer:");
            Console.WriteLine("\t" + "\"" + p2 + "\"");
        }
        Console.WriteLine("Your answer:");
        Console.WriteLine("\t" + "\"" + answer + "\"");
        if (hasAnswer)
        {
            res = answer == p2;
        }
        if (!res)
        {
            Console.WriteLine("DOESN'T MATCH!!!!");
        }
        else if ((endTime - startTime).TotalSeconds >= 2)
        {
            Console.WriteLine("FAIL the timeout");
            res = false;
        }
        else if (hasAnswer)
        {
            Console.WriteLine("Match :-)");
        }
        else
        {
            Console.WriteLine("OK, but is it right?");
        }
        Console.WriteLine("");
        return res;
    }

    public static void Main(string[] args)
    {
        Boolean all_right;
        all_right = true;

        string[] p0;
        string p1;
        string p2;

        // ----- test 0 -----
        p0 = new string[] {".....", ".###.", "..S#.", "...#."};
        p1 = "URURURURUR";
        p2 = "Alive";
        all_right = KawigiEdit_RunTest(0, p0, p1, true, p2) && all_right;
        // ------------------

        // ----- test 1 -----
        p0 = new string[] {".....", ".###.", "..S..", "...#."};
        p1 = "URURURURUR";
        p2 = "Dead";
        all_right = KawigiEdit_RunTest(1, p0, p1, true, p2) && all_right;
        // ------------------

        // ----- test 2 -----
        p0 = new string[] {".....", ".###.", "..S..", "...#."};
        p1 = "URURU";
        p2 = "Alive";
        all_right = KawigiEdit_RunTest(2, p0, p1, true, p2) && all_right;
        // ------------------

        // ----- test 3 -----
        p0 = new string[] {"#####", "#...#", "#.S.#", "#...#", "#####"};
        p1 = "DRULURLDRULRUDLRULDLRULDRLURLUUUURRRRDDLLDD";
        p2 = "Alive";
        all_right = KawigiEdit_RunTest(3, p0, p1, true, p2) && all_right;
        // ------------------

        // ----- test 4 -----
        p0 = new string[] {"#####", "#...#", "#.S.#", "#...#", "#.###"};
        p1 = "DRULURLDRULRUDLRULDLRULDRLURLUUUURRRRDDLLDD";
        p2 = "Dead";
        all_right = KawigiEdit_RunTest(4, p0, p1, true, p2) && all_right;
        // ------------------

        // ----- test 5 -----
        p0 = new string[] {"S"};
        p1 = "R";
        p2 = "Dead";
        all_right = KawigiEdit_RunTest(5, p0, p1, true, p2) && all_right;
        // ------------------

        if (all_right)
        {
            Console.WriteLine("You're a stud (at least on the example cases)!");
        }
        else
        {
            Console.WriteLine("Some of the test cases had errors.");
        }
    }

    #endregion
}
