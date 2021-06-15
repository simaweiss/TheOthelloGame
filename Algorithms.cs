using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B19_Ex05_Othelo_SimaWeiss_309823011
{
    class Algorithms
    {
        private readonly int m_RivalLetter = 0;

        public MatrixTile[,] m_Matrix = null;
        private readonly int m_CurrentSoldier;

        public int m_RightDirectionColumn;
        public int m_RightDirectionRow;
        public int m_LeftDirectionColumn;
        public int m_LeftDirectionRow;
        public int m_UpDirectionColumn;
        public int m_UpDirectionRow;
        public int m_DownDirectionColumn;
        public int m_DiagonalLeftUpDirectionColumn;
        public int m_DiagonalLeftUpDirectionRow;
        public int m_DownDirectionRow;
        public int m_DiagonalRightUpDirectionColumn;
        public int m_DiagonalRightUpDirectionRow;
        public int m_DiagonalLeftDownDirectionColumn;
        public int m_DiagonalLeftDownDirectionRow;
        public int m_DiagonalRightDownDirectionColumn;
        public int m_DiagonalRightDownDirectionRow;

        private readonly int m_RowNumber = 0;
        private readonly int m_ColumnNumber = 0;
        public List<string> m_PathDirections = null;

        public Algorithms(MatrixTile[,] i_Matrix, int i_CurrentSoldier, int i_RowNumber, int i_ColumnNumber)
        {
            m_Matrix = i_Matrix;
            m_PathDirections = new List<string>();
            m_CurrentSoldier = i_CurrentSoldier;
            m_RivalLetter = 167 - m_CurrentSoldier;
            m_RowNumber = i_RowNumber;
            m_ColumnNumber = i_ColumnNumber;
        }

        public bool CheckIfValidMove(int i_RowNew, int i_ColNew)
        {
            int typeTile = checkTypeOfTile(i_RowNew, i_ColNew);
            switch (typeTile)
            {
                case 0:
                    checkAllInternalTilesAround(i_RowNew, i_ColNew);
                    break;
                case 1:
                    checkCasesOfBoardCorners(i_RowNew, i_ColNew);
                    break;
                case 2:
                    checkCasesOfBoarderLines(i_RowNew, i_ColNew);
                    break;
            }

            return m_PathDirections.Count != 0;
        }

        private int checkTypeOfTile(int i_RowNew, int i_ColNew)
        {
            int type = 0;
            int columns = m_Matrix.GetLength(1) - 1, rows = m_Matrix.GetLength(0) - 1;
            if ((i_RowNew == 1 && i_ColNew == 1) || (i_RowNew == 1 && i_ColNew == columns) || (i_RowNew == rows && i_ColNew == 1) || (i_RowNew == rows && i_ColNew == columns))
            {
                type = 1;
            }
            else
            {
                // tiles on the edges that are not corners - this option was checked above: 
                if (i_RowNew == 1 || i_RowNew == rows || i_ColNew == 1 || i_ColNew == columns)
                {
                    type = 2;
                }
            }

            return type;
        }

        private void doUp(MatrixTile i_Tile)
        {
            bool taken = i_Tile.Taken;
            int rival = i_Tile.CurrentValue;
            if (rival == m_RivalLetter && taken)
            {
                checkIfPathIsLegalUp();
            }
        }

        private void doDown(MatrixTile i_Tile)
        {
            bool taken = i_Tile.Taken;
            int rival = i_Tile.CurrentValue;
            if (rival == m_RivalLetter && taken)
            {
                checkIfPathIsLegalDown();
            }
        }

        private void doRight(MatrixTile i_Tile)
        {
            bool taken = i_Tile.Taken;
            int rival = i_Tile.CurrentValue;
            if (rival == m_RivalLetter && taken)
            {
                checkIfPathIsLegalRight();
            }
        }

        private void doLeft(MatrixTile i_Tile)
        {
            bool taken = i_Tile.Taken;
            int rival = i_Tile.CurrentValue;
            if (rival == m_RivalLetter && taken)
            {
                checkIfPathIsLegalLeft();
            }
        }

        private void doLeftUp(MatrixTile i_Tile)
        {
            bool taken = i_Tile.Taken;
            int rival = i_Tile.CurrentValue;
            if (rival == m_RivalLetter && taken)
            {
                checkIfPathIsLegalDiagonallyToTheLeftUp();
            }
        }

        private void doLeftDown(MatrixTile i_Tile)
        {
            bool taken = i_Tile.Taken;
            int rival = i_Tile.CurrentValue;
            if (rival == m_RivalLetter && taken)
            {
                checkIfPathIsLegalDiagonallyToTheLeftDown();
            }
        }

        private void doRightUp(MatrixTile i_Tile)
        {
            bool taken = i_Tile.Taken;
            int rival = i_Tile.CurrentValue;
            if (rival == m_RivalLetter && taken)
            {
                checkIfPathIsLegalDiagonallyToTheRightUp();
            }
        }

        private void doRightDown(MatrixTile i_Tile)
        {
            bool taken = i_Tile.Taken;
            int rival = i_Tile.CurrentValue;
            if (rival == m_RivalLetter && taken)
            {
                checkIfPathIsLegalDiagonallyToTheRightDown();
            }
        }






        private void checkCasesOfBoardCorners(int i_RowNew, int i_ColNew)
        {
            int row = i_RowNew;
            int column = i_ColNew;
            if (row == 1 && column == 1)
            {
                doRight(m_Matrix[1, 2]);
                doDown(m_Matrix[2, 1]);
                doRightDown(m_Matrix[2, 2]);
            }
            else if (row == 1 && column == m_Matrix.GetLength(1) - 1)
            {
                doLeft(m_Matrix[1, column - 1]);
                doDown(m_Matrix[2, column]);
                doLeftDown(m_Matrix[2, column - 1]);
            }
            else if (row == m_Matrix.GetLength(0) - 1 && column == 1)
            {
                doUp(m_Matrix[row - 1, 1]);
                doRight(m_Matrix[row, 2]);
                doRightUp(m_Matrix[row - 1, 2]);
            }
            else if (row == m_Matrix.GetLength(0) - 1 && column == m_Matrix.GetLength(1) - 1)
            {
                doLeft(m_Matrix[row, column - 1]);
                doUp(m_Matrix[row - 1, column]);
                doLeftUp(m_Matrix[row - 1, column - 1]);
            }
        }

        // does not include the 4 corner tiles of board, which were checked above.
        private void checkCasesOfBoarderLines(int i_RowNew, int i_ColNew)
        {
            int row = i_RowNew;
            int column = i_ColNew;
            if (row == 1)
            {
                doDown(m_Matrix[row + 1, column]);
                doLeft(m_Matrix[row, column - 1]);
                doRight(m_Matrix[row, column + 1]);
                doLeftDown(m_Matrix[row + 1, column - 1]);
                doRightDown(m_Matrix[row + 1, column + 1]);
            }
            else if (row == m_Matrix.GetLength(0) - 1)
            {
                doUp(m_Matrix[row - 1, column]);
                doLeft(m_Matrix[row, column - 1]);
                doRight(m_Matrix[row, column + 1]);
                doLeftUp(m_Matrix[row - 1, column - 1]);
                doRightUp(m_Matrix[row - 1, column + 1]);
            }
            else if (column == m_Matrix.GetLength(1) - 1)
            {
                doUp(m_Matrix[row - 1, column]);
                doLeft(m_Matrix[row, column - 1]);
                doDown(m_Matrix[row + 1, column]);
                doLeftUp(m_Matrix[row - 1, column - 1]);
                doLeftDown(m_Matrix[row + 1, column - 1]);
            }
            else if (column == 1)
            {
                doUp(m_Matrix[row - 1, column]);
                doRight(m_Matrix[row, column + 1]);
                doDown(m_Matrix[row + 1, column]);
                doRightUp(m_Matrix[row - 1, column + 1]);
                doRightDown(m_Matrix[row + 1, column + 1]);
            }
        }

        private void checkAllInternalTilesAround(int i_RowNew, int i_ColNew)
        {
            int row = i_RowNew;
            int column = i_ColNew;

            // inside check all 8 Directions with the functions used in cases above. 
            doUp(m_Matrix[row - 1, column]);
            doRight(m_Matrix[row, column + 1]);
            doDown(m_Matrix[row + 1, column]);
            doLeft(m_Matrix[row, column - 1]);
            doLeftDown(m_Matrix[row + 1, column - 1]);
            doRightDown(m_Matrix[row + 1, column + 1]);
            doLeftUp(m_Matrix[row - 1, column - 1]);
            doRightUp(m_Matrix[row - 1, column + 1]);
        }

        private void checkIfPathIsLegalRight()
        {
            int row = m_RowNumber, column = m_ColumnNumber + 1;
            while (column < m_Matrix.GetLength(1) - 1 && m_Matrix[row, column].CurrentValue != m_CurrentSoldier && m_Matrix[row, column].Taken)
            {
                column++;
            }

            if (m_Matrix[row, column].CurrentValue == m_CurrentSoldier)
            {
                m_RightDirectionColumn = column;
                m_RightDirectionRow = row;
                m_PathDirections.Add("Right");
            }
        }

        private void checkIfPathIsLegalLeft()
        {
            int row = m_RowNumber, column = m_ColumnNumber - 1;
            while (column > 0 && m_Matrix[row, column].CurrentValue != m_CurrentSoldier && m_Matrix[row, column].Taken)
            {
                column--;
            }
            //// if the loop stopped because the tile was empty, it will not go into the if block!
            if (m_Matrix[row, column].CurrentValue == m_CurrentSoldier)
            {
                m_LeftDirectionColumn = column;
                m_LeftDirectionRow = row;
                m_PathDirections.Add("Left");
            }
        }

        private void checkIfPathIsLegalUp()
        {
            int row = m_RowNumber - 1, column = m_ColumnNumber;
            while (row > 0 && m_Matrix[row, column].CurrentValue != m_CurrentSoldier && m_Matrix[row, column].Taken)
            {
                row--;
            }

            if (m_Matrix[row, column].CurrentValue == m_CurrentSoldier)
            {
                m_UpDirectionColumn = column;
                m_UpDirectionRow = row;
                m_PathDirections.Add("Up");
            }
        }

        private void checkIfPathIsLegalDown()
        {
            int row = m_RowNumber + 1, column = m_ColumnNumber;
            while (row < m_Matrix.GetLength(0) - 1 && m_Matrix[row, column].CurrentValue != m_CurrentSoldier && m_Matrix[row, column].Taken)
            {
                row++;
            }

            if (m_Matrix[row, column].CurrentValue == m_CurrentSoldier)
            {
                m_DownDirectionColumn = column;
                m_DownDirectionRow = row;
                m_PathDirections.Add("Down");
            }
        }

        private void checkIfPathIsLegalDiagonallyToTheLeftUp()
        {
            int row = m_RowNumber - 1, column = m_ColumnNumber - 1;
            while (row > 0 && column > 0 && m_Matrix[row, column].CurrentValue != m_CurrentSoldier && m_Matrix[row, column].Taken)
            {
                row--;
                column--;
            }

            if (m_Matrix[row, column].CurrentValue == m_CurrentSoldier)
            {
                m_DiagonalLeftUpDirectionColumn = column;
                m_DiagonalLeftUpDirectionRow = row;
                m_PathDirections.Add("LeftUp");
            }
        }

        private void checkIfPathIsLegalDiagonallyToTheRightUp()
        {
            int row = m_RowNumber - 1, column = m_ColumnNumber + 1;
            while (row > 0 && column < m_Matrix.GetLength(1) - 1 && m_Matrix[row, column].CurrentValue != m_CurrentSoldier && m_Matrix[row, column].Taken)
            {
                row--;
                column++;
            }

            if (m_Matrix[row, column].CurrentValue == m_CurrentSoldier)
            {
                m_DiagonalRightUpDirectionColumn = column;
                m_DiagonalRightUpDirectionRow = row;
                m_PathDirections.Add("RightUp");
            }
        }

        private void checkIfPathIsLegalDiagonallyToTheLeftDown()
        {
            int row = m_RowNumber + 1, column = m_ColumnNumber - 1;
            while (row < m_Matrix.GetLength(0) - 1 && column > 0 && m_Matrix[row, column].CurrentValue != m_CurrentSoldier && m_Matrix[row, column].Taken)
            {
                row++;
                column--;
            }

            if (m_Matrix[row, column].CurrentValue == m_CurrentSoldier)
            {
                m_DiagonalLeftDownDirectionColumn = column;
                m_DiagonalLeftDownDirectionRow = row;
                m_PathDirections.Add("LeftDown");
            }
        }

        private void checkIfPathIsLegalDiagonallyToTheRightDown()
        {
            int row = m_RowNumber + 1, column = m_ColumnNumber + 1;
            while (row < m_Matrix.GetLength(0) - 1 && column < m_Matrix.GetLength(1) - 1 && m_Matrix[row, column].CurrentValue != m_CurrentSoldier && m_Matrix[row, column].Taken)
            {
                row++;
                column++;
            }

            if (m_Matrix[row, column].CurrentValue == m_CurrentSoldier)
            {
                m_DiagonalRightDownDirectionColumn = column;
                m_DiagonalRightDownDirectionRow = row;
                m_PathDirections.Add("RightDown");
            }
        }
    }
}
