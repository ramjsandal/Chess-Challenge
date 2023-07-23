using System;
using ChessChallenge.API;

public class MyBot : IChessBot
{
/*
right now bot doesn't consider white playing a good move, considers worst possible move
*/
  public Move Think(Board board, Timer timer)
  {
    // Only works for black rn
    return GetBestMove(board, 100);
  }

  public Move GetBestMove(Board board, int depth)
  {
    Move[] blackMoves = board.GetLegalMoves();
    float bestMoveEval = 999;
    int bestMoveIndex = 0;
    for (int i = 0; i < blackMoves.Length; i++)
    {
      board.MakeMove(blackMoves[i]);
      Move[] whiteMoves = board.GetLegalMoves();
      float bestWhiteMoveEval = -999;
      int bestWhiteMoveIndex = 0;
      for (int j = 0; j < whiteMoves.Length; j++)
      {
        board.MakeMove(whiteMoves[j]);
        if (Evaluate(board) > bestWhiteMoveEval)
        {
          bestWhiteMoveEval = Evaluate(board);
          bestWhiteMoveIndex = j;
        }
        board.UndoMove(whiteMoves[j]);
      }

      if (bestWhiteMoveEval < bestMoveEval)
      {
        bestMoveEval = bestWhiteMoveEval;
        bestMoveIndex = i;
      }

      board.UndoMove(blackMoves[i]);
    }

    return blackMoves[bestMoveIndex];
  }


  public float Evaluate(Board board)
  {
    PieceList[] pieces = board.GetAllPieceLists();
    float white = 0, black = 0;
    for (int i = 0; i < pieces.Length; i++)
    {
      if (i < 6)
      {
        white += GetPieceVal(pieces[i].TypeOfPieceInList) * pieces[i].Count;
      }
      else
      {
        black += GetPieceVal(pieces[i].TypeOfPieceInList) * pieces[i].Count;
      }
    }

    return white - black;
  }

  public float GetPieceVal(PieceType pt)
  {
    switch (pt)
    {
      case PieceType.Pawn:
        return 1;
      case PieceType.Bishop:
      case PieceType.Knight:
        return 3;
      case PieceType.Rook:
        return 5;
      case PieceType.Queen:
        return 9;
      case PieceType.King:
        return 500;
    }

    return 0;
  }
}