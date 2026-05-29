using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManagerScript : MonoBehaviour
{
    // White Piece Prefabs
    public GameObject whiteXPrefab;
    public GameObject whiteEpsilonPrefab;
    public GameObject whitePiPrefab;
    public GameObject whiteYPrefab;
    public GameObject whiteSigmaPrefab;
    public GameObject whiteDeltaPrefab;
    public GameObject whiteFunctionPrefab;
    public GameObject whiteInfiniteSumPrefab;
    public GameObject whiteDeltaEpsilonPrefab;
    
    // Black Piece Prefabs
    public GameObject blackXPrefab;
    public GameObject blackEpsilonPrefab;
    public GameObject blackPiPrefab;
    public GameObject blackYPrefab;
    public GameObject blackSigmaPrefab;
    public GameObject blackDeltaPrefab;
    public GameObject blackFunctionPrefab;
    public GameObject blackInfiniteSumPrefab;
    public GameObject blackDeltaEpsilonPrefab;
    
    private GameObject[,] board;
    
    // Camera (reference to the scene's main camera)
    private GameObject mainCamera;

    private bool acceptingPlayerInput = true;

    public bool isWhite;
    private Quaternion rotation;

    private GameObject selectedSquare = null;
    void Start()
    {
        int rows = 8;
        int cols = 8;
        board = new GameObject[cols, rows];
        
        for (int x = 0; x < cols; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                string findingName = ((char)('a' + x)).ToString() + (y + 1);
                board[x, y] = GameObject.Find(findingName);
                Debug.Log(board[x, y]);
            }
        }

        /* By Now, the coordinates should all correspond to a square, and you can get the square's
        location by doing board[x, y].transform.position.x or .z for x and z respectfully */

        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void FixedUpdate()
    {
        if (!isWhite)
        {
            rotation = Quaternion.Euler(Quaternion.identity.x, 180, Quaternion.identity.z);
            mainCamera.transform.rotation = Quaternion.Euler(90f, 180f, 0f);
        }
        else
        {
            rotation = Quaternion.identity;
            mainCamera.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        }
        
        if (acceptingPlayerInput)
        {
            if (Keyboard.current != null && Keyboard.current.rKey.wasPressedThisFrame)
            {
                GameRestart();
            }
            
            if (Mouse.current.leftButton.wasReleasedThisFrame)
            {
                Vector2 mousePos = Mouse.current.position.ReadValue();
                Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(mousePos);
                RaycastHit hit;
                
                if (Physics.Raycast(ray, out hit))
                {
                    //if (hit.collider.gameObject.GetComponent<SquareScript>().HasPiece())
                    //{
                        
                    //}
                    Debug.Log("Hit: " + hit.collider.gameObject.name);
                    String tagToCheck;
                    if (isWhite)
                    {
                        tagToCheck = "WhitePiece";
                    }
                    else
                    {
                        tagToCheck = "BlackPiece";
                    }
                
                    if (selectedSquare == null && hit.collider.gameObject.GetComponent<SquareScript>().HasPiece())
                    {
                        if (hit.collider.gameObject.GetComponent<SquareScript>().GetPiece().CompareTag(tagToCheck))
                        {
                            selectedSquare = hit.collider.gameObject;
                            Debug.Log("Selected: " + selectedSquare.name + " Type: " + hit.collider.gameObject.GetComponent<SquareScript>().GetPieceType());
                            HighlightAvailableSquares(selectedSquare);
                        }
                    }
                    /*
                    if (selectedSquare != null && selectedSquare != hit.collider.gameObject && selectedSquare.gameObject.GetComponent<SquareScript>().CanMoveToSquare(hit.collider.gameObject.GetComponent<SquareScript>().GetX(), hit.collider.gameObject.GetComponent<SquareScript>().GetZ()))
                    {
                        if (!hit.collider.gameObject.GetComponent<SquareScript>().HasPiece()) // Moves to Empty Space
                        {
                            hit.collider.gameObject.GetComponent<SquareScript>()
                                .AssignPiece(selectedSquare.GetComponent<SquareScript>().GetPiece());
                            selectedSquare.GetComponent<SquareScript>().UnassignPiece();
                            Debug.Log("Unassigned " + hit.collider.gameObject.GetComponent<SquareScript>().GetPiece() +
                                      " from " + selectedSquare.name + " and assigned it to " +
                                      hit.collider.gameObject.name);
                            selectedSquare = null;
                            DisableAllHighlights();
                            StartCoroutine(SwitchSides());
                        }
                        else if (!hit.collider.gameObject.GetComponent<SquareScript>().GetPiece().CompareTag(tagToCheck)) // Takes opponent Piece
                        {
                            Destroy(hit.collider.gameObject.GetComponent<SquareScript>().GetPiece());
                            hit.collider.gameObject.GetComponent<SquareScript>()
                                .AssignPiece(selectedSquare.GetComponent<SquareScript>().GetPiece());
                            selectedSquare.GetComponent<SquareScript>().UnassignPiece();
                            Debug.Log("Unassigned " + hit.collider.gameObject.GetComponent<SquareScript>().GetPiece() +
                                      " from " + selectedSquare.name + " and assigned it to " +
                                      hit.collider.gameObject.name);
                            selectedSquare = null;
                            DisableAllHighlights();
                            StartCoroutine(SwitchSides());
                        }
                        else // Selects another Piece
                        {
                            selectedSquare = hit.collider.gameObject;
                            Debug.Log("Selected: " + selectedSquare.name + " Type: " + hit.collider.gameObject.GetComponent<SquareScript>().GetPieceType());
                            HighlightAvailableSquares(selectedSquare);
                        }
                    } */
                    // above is legacy selection mode

                    if (selectedSquare != null && selectedSquare != hit.collider.gameObject) // has a selected piece and clicks another square
                    {
                        if (selectedSquare.gameObject.GetComponent<SquareScript>().CanMoveToSquare(
                                hit.collider.gameObject.GetComponent<SquareScript>().GetX(),
                                hit.collider.gameObject.GetComponent<SquareScript>().GetZ())
                            && !hit.collider.gameObject.GetComponent<SquareScript>().HasPiece()) // moves to possible empty square
                        {
                            hit.collider.gameObject.GetComponent<SquareScript>()
                                .AssignPiece(selectedSquare.GetComponent<SquareScript>().GetPiece());
                            selectedSquare.GetComponent<SquareScript>().UnassignPiece();
                            Debug.Log("Unassigned " + hit.collider.gameObject.GetComponent<SquareScript>().GetPiece() +
                                      " from " + selectedSquare.name + " and assigned it to " +
                                      hit.collider.gameObject.name);
                            selectedSquare = null;
                            DisableAllHighlights();
                            if (isWhite)
                            {
                                if (hit.collider.gameObject.GetComponent<SquareScript>().GetPieceType() == Piece.PieceType.Sigma &&
                                    hit.collider.gameObject.GetComponent<SquareScript>().GetZ() == 7)
                                {
                                    Destroy(hit.collider.gameObject.GetComponent<SquareScript>().GetPiece());
                                    hit.collider.gameObject.GetComponent<SquareScript>().AssignPiece(Instantiate(whiteInfiniteSumPrefab, transform.position, rotation));
                                }
                            }
                            else
                            {
                                if (hit.collider.gameObject.GetComponent<SquareScript>().GetPieceType() == Piece.PieceType.Sigma &&
                                    hit.collider.gameObject.GetComponent<SquareScript>().GetZ() == 7)
                                {
                                    Destroy(hit.collider.gameObject.GetComponent<SquareScript>().GetPiece());
                                    hit.collider.gameObject.GetComponent<SquareScript>().AssignPiece(Instantiate(blackInfiniteSumPrefab, transform.position, rotation));
                                }
                            }
                            StartCoroutine(SwitchSides());
                        }
                        else if (selectedSquare.gameObject.GetComponent<SquareScript>().CanMoveToSquare(
                                     hit.collider.gameObject.GetComponent<SquareScript>().GetX(),
                                     hit.collider.gameObject.GetComponent<SquareScript>().GetZ())
                                 && hit.collider.gameObject.GetComponent<SquareScript>().HasPiece()) // if it moves to a possible square that has another piece on it
                        {
                            if (hit.collider.gameObject.GetComponent<SquareScript>().GetPiece().CompareTag(tagToCheck)) // if it's trying to combine
                            {
                                Piece.PieceType typeOfInitialPiece = selectedSquare.GetComponent<SquareScript>().GetPieceType();
                                Destroy(selectedSquare.gameObject.GetComponent<SquareScript>().GetPiece());
                                if (typeOfInitialPiece != Piece.PieceType.Sigma)
                                {
                                    Destroy(hit.collider.gameObject.GetComponent<SquareScript>().GetPiece());
                                }
                                else
                                {
                                    selectedSquare.gameObject.GetComponent<SquareScript>().AssignPiece(hit.collider.gameObject.GetComponent<SquareScript>().GetPiece());
                                    hit.collider.gameObject.GetComponent<SquareScript>().UnassignPiece();
                                    if (isWhite)
                                    {
                                        hit.collider.gameObject.GetComponent<SquareScript>().AssignPiece(Instantiate(whiteSigmaPrefab, transform.position, rotation));
                                    }
                                    else
                                    {
                                        hit.collider.gameObject.GetComponent<SquareScript>().AssignPiece(Instantiate(blackSigmaPrefab, transform.position, rotation));
                                    }
                                }
                                switch (typeOfInitialPiece)
                                {
                                    case Piece.PieceType.X:
                                        if (isWhite)
                                        {
                                            hit.collider.gameObject.GetComponent<SquareScript>()
                                                .AssignPiece(Instantiate(whiteFunctionPrefab, transform.position, rotation));
                                        }
                                        else
                                        {
                                            hit.collider.gameObject.GetComponent<SquareScript>()
                                                .AssignPiece(Instantiate(blackFunctionPrefab, transform.position, rotation));
                                        }

                                        break;
                                    case Piece.PieceType.Y:
                                        if (isWhite)
                                        {
                                            hit.collider.gameObject.GetComponent<SquareScript>()
                                                .AssignPiece(Instantiate(whiteFunctionPrefab, transform.position, rotation));
                                        }
                                        else
                                        {
                                            hit.collider.gameObject.GetComponent<SquareScript>()
                                                .AssignPiece(Instantiate(blackFunctionPrefab, transform.position, rotation));
                                        }

                                        break;
                                    case Piece.PieceType.Delta:
                                        if (isWhite)
                                        {
                                            hit.collider.gameObject.GetComponent<SquareScript>()
                                                .AssignPiece(Instantiate(whiteDeltaEpsilonPrefab, transform.position, rotation));
                                        }
                                        else
                                        {
                                            hit.collider.gameObject.GetComponent<SquareScript>()
                                                .AssignPiece(Instantiate(blackDeltaEpsilonPrefab, transform.position, rotation));
                                        }

                                        break;
                                    case Piece.PieceType.Epsilon:
                                        if (isWhite)
                                        {
                                            hit.collider.gameObject.GetComponent<SquareScript>()
                                                .AssignPiece(Instantiate(whiteDeltaEpsilonPrefab, transform.position, rotation));
                                        }
                                        else
                                        {
                                            hit.collider.gameObject.GetComponent<SquareScript>()
                                                .AssignPiece(Instantiate(blackDeltaEpsilonPrefab, transform.position, rotation));
                                        }

                                        break;
                                }
                                selectedSquare = null;
                                if (isWhite)
                                {
                                    if (hit.collider.gameObject.GetComponent<SquareScript>().GetPieceType() == Piece.PieceType.Sigma &&
                                        hit.collider.gameObject.GetComponent<SquareScript>().GetZ() == 7)
                                    {
                                        Destroy(hit.collider.gameObject.GetComponent<SquareScript>().GetPiece());
                                        hit.collider.gameObject.GetComponent<SquareScript>().AssignPiece(Instantiate(whiteInfiniteSumPrefab, transform.position, rotation));
                                    }
                                }
                                else
                                {
                                    if (hit.collider.gameObject.GetComponent<SquareScript>().GetPieceType() == Piece.PieceType.Sigma &&
                                        hit.collider.gameObject.GetComponent<SquareScript>().GetZ() == 7)
                                    {
                                        Destroy(hit.collider.gameObject.GetComponent<SquareScript>().GetPiece());
                                        hit.collider.gameObject.GetComponent<SquareScript>().AssignPiece(Instantiate(blackInfiniteSumPrefab, transform.position, rotation));
                                    }
                                }
                                DisableAllHighlights();
                                StartCoroutine(SwitchSides());
                            }
                            else // capturing
                            {
                                Destroy(hit.collider.gameObject.GetComponent<SquareScript>().GetPiece());
                                hit.collider.gameObject.GetComponent<SquareScript>()
                                    .AssignPiece(selectedSquare.GetComponent<SquareScript>().GetPiece());
                                selectedSquare.GetComponent<SquareScript>().UnassignPiece();
                                Debug.Log("Unassigned " + hit.collider.gameObject.GetComponent<SquareScript>().GetPiece() +
                                          " from " + selectedSquare.name + " and assigned it to " +
                                          hit.collider.gameObject.name);
                                selectedSquare = null;
                                if (isWhite)
                                {
                                    if (hit.collider.gameObject.GetComponent<SquareScript>().GetPieceType() == Piece.PieceType.Sigma &&
                                        hit.collider.gameObject.GetComponent<SquareScript>().GetZ() == 7)
                                    {
                                        Destroy(hit.collider.gameObject.GetComponent<SquareScript>().GetPiece());
                                        hit.collider.gameObject.GetComponent<SquareScript>().AssignPiece(Instantiate(whiteInfiniteSumPrefab, transform.position, rotation));
                                    }
                                }
                                else
                                {
                                    if (hit.collider.gameObject.GetComponent<SquareScript>().GetPieceType() == Piece.PieceType.Sigma &&
                                        hit.collider.gameObject.GetComponent<SquareScript>().GetZ() == 7)
                                    {
                                        Destroy(hit.collider.gameObject.GetComponent<SquareScript>().GetPiece());
                                        hit.collider.gameObject.GetComponent<SquareScript>().AssignPiece(Instantiate(blackInfiniteSumPrefab, transform.position, rotation));
                                    }
                                }
                                DisableAllHighlights();
                                StartCoroutine(SwitchSides());
                            }
                        }
                        else if (!selectedSquare.gameObject.GetComponent<SquareScript>().CanMoveToSquare(
                                     hit.collider.gameObject.GetComponent<SquareScript>().GetX(),
                                     hit.collider.gameObject.GetComponent<SquareScript>().GetZ())
                                 && hit.collider.gameObject.GetComponent<SquareScript>().HasPiece()
                                 && hit.collider.gameObject.GetComponent<SquareScript>().GetPiece().CompareTag(tagToCheck)) // select new square
                        {
                            selectedSquare = hit.collider.gameObject;
                            Debug.Log("Selected: " + selectedSquare.name + " Type: " + hit.collider.gameObject.GetComponent<SquareScript>().GetPieceType());
                            HighlightAvailableSquares(selectedSquare);
                        }
                        else
                        {
                            selectedSquare = null;
                            Debug.Log("Deselected Square");
                            DisableAllHighlights();
                        }
                    }
                }
            }
        }
        
        
    }

    public void GameRestart()
    {
        StartCoroutine(GameRestartRoutine());
    }

    private IEnumerator GameRestartRoutine()
    {
        acceptingPlayerInput = false;
        if (!isWhite)
        {
            yield return StartCoroutine(SwitchSides());
        }
        yield return StartCoroutine(RemoveAllPieces());
        PlaceAllPeices();
        acceptingPlayerInput = true;
    }
    
    private IEnumerator SwitchSides()
    {
        acceptingPlayerInput = false;
        yield return new WaitForSeconds(0.5f);
        isWhite = !isWhite;
        acceptingPlayerInput = true;
    }

    private IEnumerator RemoveAllPieces()
    {
        while (true)
        {
            GameObject[] blacks = GameObject.FindGameObjectsWithTag("BlackPiece");
            GameObject[] whites = GameObject.FindGameObjectsWithTag("WhitePiece");

            if ((blacks == null || blacks.Length == 0) && (whites == null || whites.Length == 0))
                break;

            foreach (GameObject b in blacks)
            {
                if (b != null)
                    Destroy(b);
            }

            foreach (GameObject w in whites)
            {
                if (w != null)
                    Destroy(w);
            }
            
            yield return null;
        }
    }
    
    private void PlaceAllPeices()
    {
        // White Piece Setup
        
        board[0, 0].GetComponent<SquareScript>().AssignPiece(Instantiate(whiteXPrefab, transform.position, rotation));
        board[1, 0].GetComponent<SquareScript>().AssignPiece(Instantiate(whiteDeltaPrefab, transform.position, rotation));
        board[2, 0].GetComponent<SquareScript>().AssignPiece(Instantiate(whitePiPrefab, transform.position, rotation));
        board[3, 0].GetComponent<SquareScript>().AssignPiece(Instantiate(whiteSigmaPrefab, transform.position, rotation));
        board[4, 0].GetComponent<SquareScript>().AssignPiece(Instantiate(whiteYPrefab, transform.position, rotation));
        board[5, 0].GetComponent<SquareScript>().AssignPiece(Instantiate(whitePiPrefab, transform.position, rotation));
        board[6, 0].GetComponent<SquareScript>().AssignPiece(Instantiate(whiteEpsilonPrefab, transform.position, rotation));
        board[7, 0].GetComponent<SquareScript>().AssignPiece(Instantiate(whiteXPrefab, transform.position, rotation));
        
        // Black Piece Setup
        
        board[7, 7].GetComponent<SquareScript>().AssignPiece(Instantiate(blackXPrefab, transform.position, Quaternion.Euler(Quaternion.identity.x, 180, Quaternion.identity.z)));
        board[6, 7].GetComponent<SquareScript>().AssignPiece(Instantiate(blackDeltaPrefab, transform.position, Quaternion.Euler(Quaternion.identity.x, 180, Quaternion.identity.z)));
        board[5, 7].GetComponent<SquareScript>().AssignPiece(Instantiate(blackPiPrefab, transform.position, Quaternion.Euler(Quaternion.identity.x, 180, Quaternion.identity.z)));
        board[4, 7].GetComponent<SquareScript>().AssignPiece(Instantiate(blackSigmaPrefab, transform.position, Quaternion.Euler(Quaternion.identity.x, 180, Quaternion.identity.z)));
        board[3, 7].GetComponent<SquareScript>().AssignPiece(Instantiate(blackYPrefab, transform.position, Quaternion.Euler(Quaternion.identity.x, 180, Quaternion.identity.z)));
        board[2, 7].GetComponent<SquareScript>().AssignPiece(Instantiate(blackPiPrefab, transform.position, Quaternion.Euler(Quaternion.identity.x, 180, Quaternion.identity.z)));
        board[1, 7].GetComponent<SquareScript>().AssignPiece(Instantiate(blackEpsilonPrefab, transform.position, Quaternion.Euler(Quaternion.identity.x, 180, Quaternion.identity.z)));
        board[0, 7].GetComponent<SquareScript>().AssignPiece(Instantiate(blackXPrefab, transform.position, Quaternion.Euler(Quaternion.identity.x, 180, Quaternion.identity.z)));
    }

    public GameObject[,] GetBoardInfo()
    {
        return board;
    }

    private void HighlightAvailableSquares(GameObject selectedSquare)
    {
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                if (selectedSquare.gameObject.GetComponent<SquareScript>().CanMoveToSquare(x, y))
                {
                    board[x, y].GetComponent<SquareScript>().EnableHighlight();
                }
                else
                {
                    board[x, y].GetComponent<SquareScript>().DisableHighlight();
                }
            }
        }
    }

    private void DisableAllHighlights()
    {
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                board[x, y].GetComponent<SquareScript>().DisableHighlight();
            }
        }
    }
}
