using System;
using ChessChallenge.API;

public class MyBot : IChessBot
{
  public Move Think(Board board, Timer timer)
  {
    // Only works for black rn
    return GetBestMove(board, 100);

  }

  public Move GetBestMove(Board board, int depth)
  {
    Move[] moves = board.GetLegalMoves();
    float bestMoveEval = 0;
    int bestMoveIndex = 0;
    for (int i = 0; i < moves.Length; i++)
    {
      board.MakeMove(moves[i]);
      if (Evaluate(board) < bestMoveEval)
      {
        bestMoveIndex = i;
        bestMoveEval = Evaluate(board);
      }
      board.UndoMove(moves[i]);
    }
    return moves[bestMoveIndex];
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

    return white-black;
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