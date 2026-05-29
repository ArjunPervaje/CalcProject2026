using UnityEngine;

public class Piece : MonoBehaviour
{
    public enum PieceType
    {
        X,
        Delta,
        Pi,
        Sigma,
        Y,
        Epsilon,
        Function,
        InfiniteSum,
        DeltaEpsilon,
        // standard chess for potential future projects:
        Rook,
        Knight,
        Bishop,
        King,
        Queen,
        Pawn,
    }
    
    private GameObject[,] board;
    
    private bool isWhite = false;
    private string myTag = "";
    
    public PieceType pieceType;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isWhite = gameObject.CompareTag("WhitePiece");
        board = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>().GetBoardInfo();
        if (isWhite)
        {
            myTag = "WhitePiece";
        }
        else
        {
            myTag = "BlackPiece";
        }
    }

    // Update is called once per frame
    void Update()
    {
        // board = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>().GetBoardInfo();
    }

    public PieceType GetPieceType()
    {
        return pieceType;
    }
    
    public bool CheckIfCanMove(int desX, int desZ, int currX, int currZ)
    {
        switch (pieceType)
        {
            case  PieceType.X:
                return CheckIfXCanMove(desX, desZ, currX, currZ);
                //break;
            case PieceType.Delta:
                return CheckIfDeltaCanMove(desX, desZ, currX, currZ);
                //break;
            case PieceType.Pi:
                return CheckIfPiCanMove(desX, desZ, currX, currZ);
                //break;
            case PieceType.Sigma:
                return CheckIfSigmaCanMove(desX, desZ, currX, currZ);
                //break;
            case PieceType.Y:
                return CheckIfYCanMove(desX, desZ, currX, currZ);
                //break;
            case PieceType.Epsilon:
                return CheckIfEpsilonCanMove(desX, desZ, currX, currZ);
                //break;
            case PieceType.Function:
                return CheckIfFunctionCanMove(desX, desZ, currX, currZ);
                //break;
            case PieceType.InfiniteSum:
                return CheckIfInfiniteSumCanMove(desX, desZ, currX, currZ);
                //break;
            case PieceType.DeltaEpsilon:
                return CheckDeltaEpsilonCanMove(desX, desZ, currX, currZ);
            default:
                return false;
                //break;
        }
    }

    private bool CheckIfXCanMove(int desX, int desZ, int currX, int currZ)
    {
        if (Mathf.Abs(desX - currX) <= 1 && Mathf.Abs(desZ - currZ) <= 1 && (Mathf.Abs(desX - currX) + Mathf.Abs(desZ - currZ) != 0)) // all default available squares
        {
            if (!board[desX, desZ].gameObject.GetComponent<SquareScript>().HasPiece()) // is it empty?
            {
                return true;
            }
            else if (!board[desX, desZ].gameObject.GetComponent<SquareScript>().GetPiece().CompareTag(myTag)) // is it an opposing piece?
            {
                return true;
            }
            else if (board[desX, desZ].gameObject.GetComponent<SquareScript>().GetPieceType() == PieceType.Y) // can I combine with it?
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    
    private bool CheckIfYCanMove(int desX, int desZ, int currX, int currZ)
    {
        if (Mathf.Abs(desX - currX) <= 1 && Mathf.Abs(desZ - currZ) <= 1 && (Mathf.Abs(desX - currX) + Mathf.Abs(desZ - currZ) != 0)) // all default available squares
        {
            if (!board[desX, desZ].gameObject.GetComponent<SquareScript>().HasPiece()) // is it empty?
            {
                return true;
            }
            else if (!board[desX, desZ].gameObject.GetComponent<SquareScript>().GetPiece().CompareTag(myTag)) // is it an opposing piece?
            {
                return true;
            }
            else if (board[desX, desZ].gameObject.GetComponent<SquareScript>().GetPieceType() == PieceType.X) // can I combine with it?
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    
    private bool CheckIfPiCanMove(int desX, int desZ, int currX, int currZ)
    {
        if ((Mathf.Abs(desX - currX) == 3 && desZ - currZ == 0) || (Mathf.Abs(desZ - currZ) == 3 && desX - currX == 0) && (Mathf.Abs(desX - currX) + Mathf.Abs(desZ - currZ) != 0)) // all default available squares
        {
            if (!board[desX, desZ].gameObject.GetComponent<SquareScript>().HasPiece()) // is it empty?
            {
                return true;
            }
            else if (!board[desX, desZ].gameObject.GetComponent<SquareScript>().GetPiece().CompareTag(myTag)) // is it an opposing piece?
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    
    private bool CheckIfDeltaCanMove(int desX, int desZ, int currX, int currZ)
    {
        if (Mathf.Abs(desX - currX) <= 1 && Mathf.Abs(desZ - currZ) <= 1 && (Mathf.Abs(desX - currX) + Mathf.Abs(desZ - currZ) != 0)) // combining only squares
        {
            if (board[desX, desZ].gameObject.GetComponent<SquareScript>().HasPiece() && board[desX, desZ].gameObject.GetComponent<SquareScript>().GetPiece().CompareTag(myTag) && board[desX, desZ].gameObject.GetComponent<SquareScript>().GetPieceType() == PieceType.Epsilon) // can I combine with it?
            {
                return true;
            }
        }
        
        if ((Mathf.Abs(desX - currX) <= 4 && Mathf.Abs(desZ - currZ) <= 0) || (Mathf.Abs(desX - currX) <= 0 && Mathf.Abs(desZ - currZ) <= 1) && (Mathf.Abs(desX - currX) + Mathf.Abs(desZ - currZ) != 0)) // all default available squares
        {
            if (!board[desX, desZ].gameObject.GetComponent<SquareScript>().HasPiece()) // is it empty?
            {
                return true;
            }
            else if (!board[desX, desZ].gameObject.GetComponent<SquareScript>().GetPiece().CompareTag(myTag)) // is it an opposing piece?
            {
                return true;
            }
            else if (board[desX, desZ].gameObject.GetComponent<SquareScript>().GetPieceType() == PieceType.Epsilon) // can I combine with it?
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    
    private bool CheckIfEpsilonCanMove(int desX, int desZ, int currX, int currZ)
    {
        if (Mathf.Abs(desX - currX) <= 1 && Mathf.Abs(desZ - currZ) <= 1 && (Mathf.Abs(desX - currX) + Mathf.Abs(desZ - currZ) != 0)) // combining only squares
        {
            if (board[desX, desZ].gameObject.GetComponent<SquareScript>().HasPiece() && board[desX, desZ].gameObject.GetComponent<SquareScript>().GetPiece().CompareTag(myTag) && board[desX, desZ].gameObject.GetComponent<SquareScript>().GetPieceType() == PieceType.Delta) // can I combine with it?
            {
                return true;
            }
        }
        
        if ((Mathf.Abs(desX - currX) <= 0 && Mathf.Abs(desZ - currZ) <= 4) || (Mathf.Abs(desX - currX) <= 1 && Mathf.Abs(desZ - currZ) <= 0) && (Mathf.Abs(desX - currX) + Mathf.Abs(desZ - currZ) != 0)) // all default available squares
        {
            if (!board[desX, desZ].gameObject.GetComponent<SquareScript>().HasPiece()) // is it empty?
            {
                return true;
            }
            else if (!board[desX, desZ].gameObject.GetComponent<SquareScript>().GetPiece().CompareTag(myTag)) // is it an opposing piece?
            {
                return true;
            }
            else if (board[desX, desZ].gameObject.GetComponent<SquareScript>().GetPieceType() == PieceType.Delta) // can I combine with it?
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    
    private bool CheckIfSigmaCanMove(int desX, int desZ, int currX, int currZ)
    {
        if (board[desX, desZ].gameObject.GetComponent<SquareScript>().HasPiece() && board[desX, desZ].gameObject.GetComponent<SquareScript>().GetPiece().CompareTag(myTag)) // can I swap with it?
        {
            return true;
        }
        
        if ((Mathf.Abs(desX - currX) <= 2 && Mathf.Abs(desZ - currZ) <= 2) && (Mathf.Abs(desX - currX) + Mathf.Abs(desZ - currZ) != 0)) // all default available squares
        {
            if (!board[desX, desZ].gameObject.GetComponent<SquareScript>().HasPiece()) // is it empty?
            {
                return true;
            }
            else if (!board[desX, desZ].gameObject.GetComponent<SquareScript>().GetPiece().CompareTag(myTag)) // is it an opposing piece?
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    
    private bool CheckIfFunctionCanMove(int desX, int desZ, int currX, int currZ)
    {
        if (((Mathf.Abs(desX - currX) <= 2 && Mathf.Abs(desZ - currZ) <= 0) || (Mathf.Abs(desX - currX) <= 0 && Mathf.Abs(desZ - currZ) <= 2)) && (Mathf.Abs(desX - currX) + Mathf.Abs(desZ - currZ) != 0)) // all default available squares
        {
            if (!board[desX, desZ].gameObject.GetComponent<SquareScript>().HasPiece()) // is it empty?
            {
                return true;
            }
            else if (!board[desX, desZ].gameObject.GetComponent<SquareScript>().GetPiece().CompareTag(myTag)) // is it an opposing piece?
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if ((desX - currX != 0 && desZ - currZ != 0) && ((float)Mathf.Abs(desX - currX) / (float)Mathf.Abs(desZ - currZ) == 1) && (Mathf.Abs(desX - currX) + Mathf.Abs(desZ - currZ) != 0)) // all default available squares
        {
            if (!board[desX, desZ].gameObject.GetComponent<SquareScript>().HasPiece()) // is it empty?
            {
                return true;
            }
            else if (!board[desX, desZ].gameObject.GetComponent<SquareScript>().GetPiece().CompareTag(myTag)) // is it an opposing piece?
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    
    private bool CheckIfInfiniteSumCanMove(int desX, int desZ, int currX, int currZ)
    {
        if (Mathf.Abs(desX - currX) + Mathf.Abs(desZ - currZ) != 0) // all default available squares
        {
            if (!board[desX, desZ].gameObject.GetComponent<SquareScript>().HasPiece()) // is it empty?
            {
                return true;
            }
            else if (!board[desX, desZ].gameObject.GetComponent<SquareScript>().GetPiece().CompareTag(myTag)) // is it an opposing piece?
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    
    private bool CheckDeltaEpsilonCanMove(int desX, int desZ, int currX, int currZ)
    {
        if (((Mathf.Abs(desX - currX) <= 7 && Mathf.Abs(desZ - currZ) <= 0) || (Mathf.Abs(desX - currX) <= 0 && Mathf.Abs(desZ - currZ) <= 7)) && (Mathf.Abs(desX - currX) + Mathf.Abs(desZ - currZ) != 0)) // all default available squares
        {
            if (!board[desX, desZ].gameObject.GetComponent<SquareScript>().HasPiece()) // is it empty?
            {
                return true;
            }
            else if (!board[desX, desZ].gameObject.GetComponent<SquareScript>().GetPiece().CompareTag(myTag)) // is it an opposing piece?
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}
