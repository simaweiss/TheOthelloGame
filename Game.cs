using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace B19_Ex05_Othelo_SimaWeiss_309823011
{
    public delegate void ButtonGameDelegate(int i_RowIndex, int i_ColumnIndex, char i_Player);
    public delegate void ValidButtonGameDelegate(int i_RowIndex, int i_ColumnIndex, bool i_Enable);
    public delegate void WindowGameDelegate(bool i_Flag, char i_CurrentPlayer);
    public delegate void MessageGameDelegate(char i_Winner, int i_OCounter, int i_XCounter, int i_ORoundsWinnigCount, int i_XRoundsWinnigCount);
    public delegate void IntializeButtonGameDelegate(int i_RowIndex, int i_ColumnIndex, char i_Player);

    public class Game
    {
        private int m_BoardSize;
        public int m_XCounter = 0;
        public int m_OCounter = 0;
        private int m_ORoundsWinnigCount;
        private int m_XRoundsWinnigCount;
        public int m_CurrentPlayer = 0;

        private Algorithms m_Algorithms = null;
        public bool m_MovesLeft = false;
        public bool m_MoveMade = false;
        public int m_Winner = 0;
        public eTypeGame m_TypeGame;
        public event ButtonGameDelegate m_GameButtonEventHandler;
        public event WindowGameDelegate m_GameWindowEventHandler;
        public event MessageGameDelegate m_GameMessageEventHandler;
        public event IntializeButtonGameDelegate m_GameIntializeButtonGameEventHandler;
        public event ValidButtonGameDelegate m_ValidButtonGameEventHandler;

        private MatrixTile[,] m_Matrix;

        public Game(int i_BoardSize, eTypeGame i_TypeGame)
        {
            m_BoardSize = i_BoardSize;
            m_TypeGame = i_TypeGame;
            m_Matrix = new MatrixTile[m_BoardSize, m_BoardSize];
            m_CurrentPlayer = 88; // X begins
        }

        public MatrixTile[,] Matrix
        {
            get { return m_Matrix; }
        }

        private void eraceLegalPlacesPreviousTurn()
        {
            for (int i = 1; i < m_BoardSize; i++)
            {
                for (int j = 1; j < m_BoardSize; j++)
                {
                    if (m_Matrix[i, j].LegalMove == true)
                    {
                        m_Matrix[i, j].LegalMove = false;
                        m_ValidButtonGameEventHandler.Invoke(i, j, false);

                    }
                }
            }
        }

        public void Initialize_Matrix()
        {
            int i = m_BoardSize / 2;
            int j = m_BoardSize / 2;
            char converted;
            bool flagMovesLeftForCurrentPlayer = false;

            // 79 ascci for o 
            // 88 ascci for x 
            m_Matrix[i, j].CurrentValue = 79;
            m_Matrix[i, j].Taken = true;
            converted = Convert.ToChar(m_Matrix[i, j].CurrentValue);
            m_GameIntializeButtonGameEventHandler.Invoke(i, j, converted);

            m_Matrix[i, j + 1].CurrentValue = 88;
            m_Matrix[i, j + 1].Taken = true;
            converted = Convert.ToChar(m_Matrix[i, j + 1].CurrentValue);
            m_GameIntializeButtonGameEventHandler.Invoke(i, j + 1, converted);

            m_Matrix[i + 1, j].CurrentValue = 88;
            m_Matrix[i + 1, j].Taken = true;
            converted = Convert.ToChar(m_Matrix[i + 1, j].CurrentValue);
            m_GameIntializeButtonGameEventHandler.Invoke(i + 1, j, converted);

            m_Matrix[i + 1, j + 1].CurrentValue = 79;
            m_Matrix[i + 1, j + 1].Taken = true;
            converted = Convert.ToChar(m_Matrix[i + 1, j + 1].CurrentValue);
            m_GameIntializeButtonGameEventHandler.Invoke(i + 1, j + 1, converted);
            flagMovesLeftForCurrentPlayer = checkAndUpdateIfValidMovesLeftInBoard();
        }

        private void makeComputerMove()
        {
            m_GameWindowEventHandler.Invoke(true, Convert.ToChar(m_CurrentPlayer));
            //// Implementing greedy algorithm - searching for the move that gives
            //// maximal points, and making this move.

            int maxCount = 0, iMax = 0, jMax = 0, tempCount = 0;

            for (int i = 1; i < m_Matrix.GetLength(0); i++)
            {
                for (int j = 1; j < m_Matrix.GetLength(1); j++)
                {
                    Algorithms algorithm = new Algorithms(m_Matrix, m_CurrentPlayer, i, j);
                    if (!m_Matrix[i, j].Taken && algorithm.CheckIfValidMove(i, j))
                    {
                        tempCount = countStepsForValidMove(i, j, algorithm);
                        if (tempCount > maxCount)
                        {
                            maxCount = tempCount;
                            iMax = i;
                            jMax = j;
                        }
                    }
                }
            }

            Thread.Sleep(1500);
            makeMove(iMax, jMax);
        }

        private void humanPlayer(int i_RowNumber, int i_ColumnNumber)
        {
            bool flagMovesLeftForPlayer = false;

            makeMove(i_RowNumber, i_ColumnNumber);
            eraceLegalPlacesPreviousTurn();
            bool fullBoard = checkIfBoardIsFull();
            if (fullBoard)
            {
                gameOver(); 
            }
            else // ! fullBoard
            {
                m_CurrentPlayer = 167 - m_CurrentPlayer; // changing player!
                flagMovesLeftForPlayer = checkAndUpdateIfValidMovesLeftInBoard();
                if (flagMovesLeftForPlayer)
                {
                    // turn goes to next player 
                    m_GameWindowEventHandler.Invoke(true, Convert.ToChar(m_CurrentPlayer));
                }
                else
                {


                    m_CurrentPlayer = 167 - m_CurrentPlayer; // changing back player!
                    m_GameWindowEventHandler.Invoke(false, Convert.ToChar(m_CurrentPlayer));
                    Thread.Sleep(1500);

                    flagMovesLeftForPlayer = checkAndUpdateIfValidMovesLeftInBoard();

                    if (flagMovesLeftForPlayer)
                    {

                        m_GameWindowEventHandler.Invoke(true, Convert.ToChar(m_CurrentPlayer));
                    }
                    else
                    {
                        // Game is over. 
                        gameOver();
                    }
                }
            }
        }

        public void PlayGame(int i_RowNumber, int i_ColumnNumber)
        {
            bool flagMovesLeftForPlayer = false;
            bool fullBoard = false;

            if (m_TypeGame == eTypeGame.AgainstPlayer)
            {
                humanPlayer(i_RowNumber, i_ColumnNumber);
            }
            else if (m_TypeGame == eTypeGame.AgainstComputer)
            {
                makeMove(i_RowNumber, i_ColumnNumber);
                eraceLegalPlacesPreviousTurn();
                fullBoard = checkIfBoardIsFull();
                if (fullBoard)
                {
                    gameOver();
                }
                else
                {
                    m_CurrentPlayer = 167 - m_CurrentPlayer; // changing player!
                    flagMovesLeftForPlayer = checkAndUpdateIfValidMovesLeftInBoard();
                    if (flagMovesLeftForPlayer)
                    {
                        computerMove();
                    }
                    else
                    {
                        m_CurrentPlayer = 167 - m_CurrentPlayer; // changing back player!
                        m_GameWindowEventHandler.Invoke(false, Convert.ToChar(m_CurrentPlayer));
                        Thread.Sleep(1500);
                        flagMovesLeftForPlayer = checkAndUpdateIfValidMovesLeftInBoard();

                        if (flagMovesLeftForPlayer)
                        {
                            // Go Back turn. 
                            m_GameWindowEventHandler.Invoke(true, Convert.ToChar(m_CurrentPlayer));
                        }
                        else
                        {
                            // Game is over. 
                            gameOver();
                        }
                    }
                }
            }
        }

        private void computerMove()
        {
            bool fullBoard = false;
            bool flagMovesLeftForPlayer = false;
            bool ScipToCopmuterTurn = false;

            do
            {
                m_GameWindowEventHandler.Invoke(true, Convert.ToChar(m_CurrentPlayer));

                makeComputerMove();
                ScipToCopmuterTurn = false;
                eraceLegalPlacesPreviousTurn();
                fullBoard = checkIfBoardIsFull();
                if (fullBoard)
                {
                    gameOver();
                    fullBoard = true;
                }

                m_CurrentPlayer = 167 - m_CurrentPlayer; // changing back player!
                flagMovesLeftForPlayer = checkAndUpdateIfValidMovesLeftInBoard();

                if (flagMovesLeftForPlayer)
                {
                    m_GameWindowEventHandler.Invoke(true, Convert.ToChar(m_CurrentPlayer));
                }
                else
                {
                    m_CurrentPlayer = 167 - m_CurrentPlayer; // changing back to computer!
                    m_GameWindowEventHandler.Invoke(false, Convert.ToChar(m_CurrentPlayer));
                    Thread.Sleep(1500);

                    flagMovesLeftForPlayer = checkAndUpdateIfValidMovesLeftInBoard();
                    if (flagMovesLeftForPlayer)
                    {

                        ScipToCopmuterTurn = true;
                    }
                    else
                    {
                        // Game is over. 
                        ScipToCopmuterTurn = false;
                        gameOver(); 
                    }
                }
            } while (ScipToCopmuterTurn);
        }

        private void gameOver()
        {
            countScores();
            char converted = Convert.ToChar(m_Winner);
            m_GameMessageEventHandler.Invoke(converted, m_OCounter, m_XCounter, m_ORoundsWinnigCount, m_XRoundsWinnigCount);
        }

        private void makeMove(int i_RowNumber, int i_ColumnNumber)
        {
            char converted;
            m_Algorithms = new Algorithms(m_Matrix, m_CurrentPlayer, i_RowNumber, i_ColumnNumber);
            bool condition1 = !m_Matrix[i_RowNumber, i_ColumnNumber].Taken;
            bool condition2 = m_Algorithms.CheckIfValidMove(i_RowNumber, i_ColumnNumber);
            bool condition = condition1 && condition2;
            if (condition)
            {
                m_Matrix[i_RowNumber, i_ColumnNumber].CurrentValue = m_CurrentPlayer;
                m_Matrix[i_RowNumber, i_ColumnNumber].Taken = true;
                m_Matrix[i_RowNumber, i_ColumnNumber].LegalMove = false;
                converted = Convert.ToChar(m_Matrix[i_RowNumber, i_ColumnNumber].CurrentValue);
                m_GameButtonEventHandler.Invoke(i_RowNumber, i_ColumnNumber, converted);
                updateMatrix(i_RowNumber, i_ColumnNumber, converted);
            }
        }

        private void updateMatrix(int i_RowNumber, int i_ColumnNumber, char i_CurrentPlayer)
        {
            foreach (string direction in m_Algorithms.m_PathDirections)
            {
                switch (direction)
                {
                    case "Up":
                        for (int i = i_RowNumber - 1; i > m_Algorithms.m_UpDirectionRow; i--)
                        {
                            // converted = Convert.ToChar(m_Matrix[i_RowNumber, i_ColumnNumber].CurrentValue);
                            m_Matrix[i, i_ColumnNumber].CurrentValue = m_CurrentPlayer;
                            m_GameButtonEventHandler.Invoke(i, i_ColumnNumber, i_CurrentPlayer);
                        }

                        break;
                    case "Down":
                        for (int i = i_RowNumber + 1; i < m_Algorithms.m_DownDirectionRow; i++)
                        {
                            m_Matrix[i, i_ColumnNumber].CurrentValue = m_CurrentPlayer;
                            m_GameButtonEventHandler.Invoke(i, i_ColumnNumber, i_CurrentPlayer);
                        }

                        break;
                    case "Right":
                        for (int i = i_ColumnNumber + 1; i < m_Algorithms.m_RightDirectionColumn; i++)
                        {
                            m_Matrix[i_RowNumber, i].CurrentValue = m_CurrentPlayer;
                            m_GameButtonEventHandler.Invoke(i_RowNumber, i, i_CurrentPlayer);
                        }

                        break;
                    case "Left":
                        for (int i = i_ColumnNumber - 1; i > m_Algorithms.m_LeftDirectionColumn; i--)
                        {
                            m_Matrix[i_RowNumber, i].CurrentValue = m_CurrentPlayer;
                            m_GameButtonEventHandler.Invoke(i_RowNumber, i, i_CurrentPlayer);
                        }

                        break;
                    case "RightUp":
                        int j = i_ColumnNumber + 1;
                        for (int i = i_RowNumber - 1; i > m_Algorithms.m_DiagonalRightUpDirectionRow && j < m_Algorithms.m_DiagonalRightUpDirectionColumn; i--, j++)
                        {
                            m_Matrix[i, j].CurrentValue = m_CurrentPlayer;
                            m_GameButtonEventHandler.Invoke(i, j, i_CurrentPlayer);
                        }

                        break;
                    case "LeftUp":
                        j = i_ColumnNumber - 1;
                        for (int i = i_RowNumber - 1; i > m_Algorithms.m_DiagonalLeftUpDirectionRow && j > m_Algorithms.m_DiagonalLeftUpDirectionColumn; i--, j--)
                        {
                            m_Matrix[i, j].CurrentValue = m_CurrentPlayer;
                            m_GameButtonEventHandler.Invoke(i, j, i_CurrentPlayer);
                        }

                        break;
                    case "LeftDown":
                        j = i_ColumnNumber - 1;
                        for (int i = i_RowNumber + 1; i < m_Algorithms.m_DiagonalLeftDownDirectionRow && j > m_Algorithms.m_DiagonalLeftDownDirectionColumn; i++, j--)
                        {
                            m_Matrix[i, j].CurrentValue = m_CurrentPlayer;
                            m_GameButtonEventHandler.Invoke(i, j, i_CurrentPlayer);
                        }

                        break;
                    case "RightDown":
                        j = i_ColumnNumber + 1;
                        for (int i = i_RowNumber + 1; i < m_Algorithms.m_DiagonalRightDownDirectionRow && j < m_Algorithms.m_DiagonalRightDownDirectionColumn; i++, j++)
                        {
                            m_Matrix[i, j].CurrentValue = m_CurrentPlayer;
                            m_GameButtonEventHandler.Invoke(i, j, i_CurrentPlayer);
                        }

                        break;
                }
            }
        }

        private int countStepsForValidMove(int i_RowNumber, int i_ColumnNumber, Algorithms i_Algorithm)
        {
            int countSteps = 0;
            foreach (string direction in i_Algorithm.m_PathDirections)
            {
                switch (direction)
                {
                    case "Up":
                        for (int i = i_RowNumber - 1; i > i_Algorithm.m_UpDirectionRow; i--)
                        {
                            countSteps++;
                        }

                        break;
                    case "Down":
                        for (int i = i_RowNumber + 1; i < i_Algorithm.m_DownDirectionRow; i++)
                        {
                            countSteps++;
                        }

                        break;
                    case "Right":
                        for (int i = i_ColumnNumber + 1; i < i_Algorithm.m_RightDirectionColumn; i++)
                        {
                            countSteps++;
                        }

                        break;
                    case "Left":
                        for (int i = i_ColumnNumber - 1; i > i_Algorithm.m_LeftDirectionColumn; i--)
                        {
                            countSteps++;
                        }

                        break;
                    case "RightUp":
                        int j = i_ColumnNumber + 1;
                        for (int i = i_RowNumber - 1; i > i_Algorithm.m_DiagonalRightUpDirectionRow && j < i_Algorithm.m_DiagonalRightUpDirectionColumn; i--, j++)
                        {
                            countSteps++;
                        }

                        break;
                    case "LeftUp":
                        j = i_ColumnNumber - 1;
                        for (int i = i_RowNumber - 1; i > i_Algorithm.m_DiagonalLeftUpDirectionRow && j > i_Algorithm.m_DiagonalLeftUpDirectionColumn; i--, j--)
                        {
                            countSteps++;
                        }

                        break;
                    case "LeftDown":
                        j = i_ColumnNumber - 1;
                        for (int i = i_RowNumber + 1; i < i_Algorithm.m_DiagonalLeftDownDirectionRow && j > i_Algorithm.m_DiagonalLeftDownDirectionColumn; i++, j--)
                        {
                            countSteps++;
                        }

                        break;
                    case "RightDown":
                        j = i_ColumnNumber + 1;
                        for (int i = i_RowNumber + 1; i < i_Algorithm.m_DiagonalRightDownDirectionRow && j < i_Algorithm.m_DiagonalRightDownDirectionColumn; i++, j++)
                        {
                            countSteps++;
                        }

                        break;
                }
            }

            return countSteps;
        }

        private bool checkIfBoardIsFull()
        {
            bool isFull = true;
            for (int i = 1; i < m_BoardSize; i++)
            {
                for (int j = 1; j < m_BoardSize; j++)
                {
                    if (!m_Matrix[i, j].Taken)
                    {
                        isFull = false;
                    }
                }
            }

            return isFull;
        }

        private bool checkAndUpdateIfValidMovesLeftInBoard()
        {
            bool validMove = false;
            bool flagMovesLeftForCurrentPlayer = false; 
            for (int i = 1; i < m_BoardSize; i++)
            {
                for (int j = 1; j < m_BoardSize; j++)
                {
                    if (!m_Matrix[i, j].Taken)
                    {
                        m_Algorithms = new Algorithms(m_Matrix, m_CurrentPlayer, i, j);
                        validMove = m_Algorithms.CheckIfValidMove(i, j);
                        if (validMove)
                        {
                            m_Matrix[i, j].LegalMove = true;
                            m_ValidButtonGameEventHandler.Invoke(i, j, true);
                            flagMovesLeftForCurrentPlayer = true;
                        }
                    }
                }
            }

            return flagMovesLeftForCurrentPlayer;
        }

        private void countScores()
        {
            // updates m_OCounter, m_XCounter & m_Winner values.
            m_XCounter = 0;
            m_OCounter = 0;
            for (int i = 1; i < m_BoardSize; i++)
            {
                for (int j = 1; j < m_BoardSize; j++)
                {
                    if (m_Matrix[i, j].CurrentValue == 79 && m_Matrix[i, j].Taken)
                    {
                        m_OCounter++;
                    }
                    else
                    {
                        if (m_Matrix[i, j].CurrentValue == 88 && m_Matrix[i, j].Taken)
                        {
                            m_XCounter++;
                        }
                    }
                }
            }

            if (m_OCounter > m_XCounter)
            {
                m_Winner = 79;
                m_ORoundsWinnigCount++;

            }
            else
            {
                if (m_XCounter > m_OCounter)
                {
                    m_Winner = 88;
                    m_XRoundsWinnigCount++;
                }
                else
                {
                    // its a Tie 84= 'T'
                    m_Winner = 84;
                }
            }
        }
    }
}