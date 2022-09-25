using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace JGM.GameTests
{
    public class Board
    {
        public int[,] Grid => m_grid;
        public readonly int TotalFields;

        private const int m_rows = 3;
        private const int m_columns = 3;

        private int[,] m_grid;

        public Board()
        {
            m_grid = new int[3, 3];
            for (int i = 0; i < m_rows; i++)
            {
                for (int j = 0; j < m_columns; j++)
                {
                    m_grid[i, j] = (int)PlayerId.Empty;
                    TotalFields++;
                }
            }
        }

        public bool Place(int playerId, Vector2Int position)
        {
            if (PositionIsValid(position))
            {
                m_grid[position.x, position.y] = playerId;
                return true;
            }

            return false;
        }

        private bool PositionIsValid(Vector2Int position)
        {
            return position.x < m_grid.GetLength(0) && position.y < m_grid.GetLength(1) && m_grid[position.x, position.y] == (int)PlayerId.Empty;
        }
    }

    public class BoardController
    {
        private const int m_maxUsedTurns = 3;

        private readonly Board m_board;

        private int m_turn;
        private int[] m_playersUsedTurns;

        public BoardController(PlayerId startingPlayer)
        {
            m_board = new Board();
            m_turn = (int)startingPlayer;
            m_playersUsedTurns = new int[2];
            for (int i = 0; i < m_playersUsedTurns.Length; i++)
            {
                m_playersUsedTurns[i] = 0;
            }
        }

        public bool Place(int playerId, Vector2Int position)
        {
            if (m_turn == playerId && m_board.Place(playerId, position))
            {
                m_playersUsedTurns[playerId]++;
                m_turn = 1 - m_turn;

                if (m_playersUsedTurns[(int)PlayerId.One] == m_maxUsedTurns && m_playersUsedTurns[(int)PlayerId.Two] == m_maxUsedTurns)
                {
                    Debug.Log("Game Over");
                    CheckForWinner();
                }

                return true;
            }

            return false;
        }

        private void CheckForWinner()
        {
            if (!CheckForRows())
            {
                if (!CheckForColumns())
                {
                    CheckForDiagonals();
                }
            }
        }

        private bool CheckForRows()
        {
            for (int i = 0; i < 3; i++)
            {
                int rowScore = -1;
                int rowScoreCount = 0;

                for (int j = 0; j < 3; j++)
                {
                    if (rowScore == -1)
                    {
                        rowScore = m_board.Grid[i, j];
                        rowScoreCount++;
                    }
                    else
                    {
                        if (rowScore == m_board.Grid[i, j])
                        {
                            rowScoreCount++;
                            if (rowScoreCount == 3)
                            {
                                Debug.Log("Winner Is Player " + ((PlayerId)rowScore).ToString());
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        private bool CheckForColumns()
        {
            for (int i = 0; i < 3; i++)
            {
                int columnScore = -1;
                int columnScoreCount = 0;

                for (int j = 0; j < 3; j++)
                {
                    if (columnScore == -1)
                    {
                        columnScore = m_board.Grid[j, i];
                        columnScoreCount++;
                    }
                    else
                    {
                        if (columnScore == m_board.Grid[j, i])
                        {
                            columnScoreCount++;
                            if (columnScoreCount == 3)
                            {
                                Debug.Log("Winner Is Player " + ((PlayerId)columnScore).ToString());
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        private bool CheckForDiagonals()
        {
            // (0,0) / (1,1) / (2,2)
            // (0,2) / (1,1) / (2,0)
            if (m_board.Grid[0, 0] == m_board.Grid[1,1] && m_board.Grid[1,1] == m_board.Grid[2, 2])
            {
                Debug.Log("Winner Is Player " + ((PlayerId)m_board.Grid[0, 0]).ToString());
                return true;
            }
            else if (m_board.Grid[0, 2] == m_board.Grid[1, 1] && m_board.Grid[1, 1] == m_board.Grid[2, 0])
            {
                Debug.Log("Winner Is Player " + ((PlayerId)m_board.Grid[0, 2]).ToString());
                return true;
            }

            return false;
        }
    }

    public enum PlayerId
    {
        Empty = -1,
        One = 0,
        Two = 1
    }

    public class NewBehaviourScript
    {
        private Board m_board;
        private BoardController m_boardController;

        [SetUp]
        public void Setup()
        {
            m_board = new Board();
            m_boardController = new BoardController(PlayerId.One);
        }

        [Test]
        public void when_creating_new_game_grid_is_3x3()
        {
            Assert.AreEqual(9, m_board.TotalFields);
        }

        [TestCase(0, 0)]
        [TestCase(0, 1)]
        [TestCase(0, 2)]
        [TestCase(1, 0)]
        [TestCase(1, 1)]
        [TestCase(1, 2)]
        [TestCase(2, 0)]
        [TestCase(2, 1)]
        [TestCase(2, 2)]
        public void when_creating_new_game_player_can_place_anywhere_in_the_grid(int xCoord, int yCoord)
        {
            int playerId = (int)PlayerId.One;
            Vector2Int position = new Vector2Int(xCoord, yCoord);
            Assert.IsTrue(m_board.Place(playerId, position));
        }

        [TestCase(PlayerId.One, PlayerId.Two, true)]
        [TestCase(PlayerId.Two, PlayerId.One, true)]
        [TestCase(PlayerId.One, PlayerId.One, false)]
        [TestCase(PlayerId.Two, PlayerId.Two, false)]
        public void when_player_places_his_token_its_next_player_turn(PlayerId firstPlayer, PlayerId secondsPlayer, bool result)
        {
            m_boardController = new BoardController(firstPlayer);
            Vector2Int firstPosition = new Vector2Int(1, 1);
            Assert.IsTrue(m_boardController.Place((int)firstPlayer, firstPosition));
            Vector2Int secondPosition = new Vector2Int(2, 1);
            Assert.AreEqual(result, m_boardController.Place((int)secondsPlayer, secondPosition));
        }

        [TestCase(0, 0, 0, 0, false)]
        [TestCase(0, 1, 1, 2, true)]
        [TestCase(0, 2, 1, 1, true)]
        [TestCase(1, 0, 1, 0, false)]
        [TestCase(1, 1, 0, 0, true)]
        [TestCase(1, 2, 1, 2, false)]
        public void when_player_places_his_token_if_its_taken_he_decides_again(int xCoordTaken, int yCoordTaken, int xCoord, int yCoord, bool result)
        {
            int playerId = (int)PlayerId.One;
            Vector2Int positionTaken = new Vector2Int(xCoordTaken, yCoordTaken);
            Assert.IsTrue(m_board.Place(playerId, positionTaken));
            Vector2Int position = new Vector2Int(xCoord, yCoord);
            Assert.AreEqual(result, m_board.Place(playerId, position));
        }

        [Test]
        public void when_each_player_has_placed_tokens_for_3_turns_the_game_ends()
        {
            m_boardController.Place((int)PlayerId.One, new Vector2Int(0, 0));
            m_boardController.Place((int)PlayerId.Two, new Vector2Int(0, 1));
            m_boardController.Place((int)PlayerId.One, new Vector2Int(0, 2));
            m_boardController.Place((int)PlayerId.Two, new Vector2Int(1, 0));
            m_boardController.Place((int)PlayerId.One, new Vector2Int(1, 1));
            m_boardController.Place((int)PlayerId.Two, new Vector2Int(1, 2));
            LogAssert.Expect(LogType.Log, "Game Over");
        }

        [Test]
        public void player_cannot_place_his_token_twice_in_a_row()
        {
            m_boardController.Place((int)PlayerId.One, new Vector2Int(0, 0));
            Assert.IsFalse(m_boardController.Place((int)PlayerId.One, new Vector2Int(1, 0)));
        }

        [Test]
        public void when_3_fields_in_a_row_are_of_same_player_he_wins()
        {
            m_boardController.Place((int)PlayerId.One, new Vector2Int(0, 0));
            m_boardController.Place((int)PlayerId.Two, new Vector2Int(2, 2));
            m_boardController.Place((int)PlayerId.One, new Vector2Int(0, 1));
            m_boardController.Place((int)PlayerId.Two, new Vector2Int(1, 1));
            m_boardController.Place((int)PlayerId.One, new Vector2Int(0, 2));
            m_boardController.Place((int)PlayerId.Two, new Vector2Int(1, 2));
            LogAssert.Expect(LogType.Log, "Winner Is Player One");
        }

        [Test]
        public void when_3_fields_in_a_column_are_of_same_player_he_wins()
        {
            m_boardController.Place((int)PlayerId.One, new Vector2Int(0, 1));
            m_boardController.Place((int)PlayerId.Two, new Vector2Int(0, 0));
            m_boardController.Place((int)PlayerId.One, new Vector2Int(1, 1));
            m_boardController.Place((int)PlayerId.Two, new Vector2Int(0, 2));
            m_boardController.Place((int)PlayerId.One, new Vector2Int(2, 1));
            m_boardController.Place((int)PlayerId.Two, new Vector2Int(2, 2));
            LogAssert.Expect(LogType.Log, "Winner Is Player One");
        }

        [Test]
        public void when_3_fields_in_a_diagonal_are_of_same_player_he_wins()
        {
            // (0,2) / (1,1) / (2,0)
            m_boardController.Place((int)PlayerId.One, new Vector2Int(0, 0));
            m_boardController.Place((int)PlayerId.Two, new Vector2Int(0, 2));
            m_boardController.Place((int)PlayerId.One, new Vector2Int(1, 2));
            m_boardController.Place((int)PlayerId.Two, new Vector2Int(1, 1));
            m_boardController.Place((int)PlayerId.One, new Vector2Int(2, 2));
            m_boardController.Place((int)PlayerId.Two, new Vector2Int(2, 0));
            LogAssert.Expect(LogType.Log, "Winner Is Player Two");
        }
    }
}