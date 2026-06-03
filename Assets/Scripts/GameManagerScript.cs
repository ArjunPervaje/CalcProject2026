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

    private bool gotQuestionRight = false;
    
    // Camera (reference to the scene's main camera)
    private GameObject mainCamera;

    private bool acceptingPlayerInput = true;

    public bool isWhite;
    private Quaternion rotation;

    private GameObject selectedSquare = null;

    public QuestionManagerScript questionManager;

    
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
        
        GameRestart();
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
                    GameObject hitObject = hit.collider.gameObject;
                    SquareScript hitSquare = hitObject.GetComponent<SquareScript>();
                    string tagToCheck = isWhite ? "WhitePiece" : "BlackPiece";

                    // If nothing selected yet, try selecting this square's piece
                    if (selectedSquare == null && hitSquare.HasPiece())
                    {
                        if (hitSquare.GetPiece().CompareTag(tagToCheck))
                        {
                            selectedSquare = hitObject;
                            Debug.Log("Selected: " + selectedSquare.name + " Type: " + hitSquare.GetPieceType());
                            HighlightAvailableSquares(selectedSquare);
                        }
                    }

                    // If a piece is selected and we clicked a different square, handle movement/interaction
                    if (selectedSquare != null && selectedSquare != hitObject)
                    {
                        SquareScript selectedSquareScript = selectedSquare.GetComponent<SquareScript>();
                        int targetX = hitSquare.GetX();
                        int targetZ = hitSquare.GetZ();

                        bool canMoveToTarget = selectedSquareScript.CanMoveToSquare(targetX, targetZ);
                        bool targetHasPiece = hitSquare.HasPiece();

                        // Move to empty square
                        if (canMoveToTarget && !targetHasPiece)
                        {
                            bool tryingToPromote = ((isWhite && targetZ == 7) || (!isWhite && targetZ == 0));
                            if (selectedSquareScript.GetPieceType() == Piece.PieceType.Sigma && tryingToPromote)
                            {
                                // Promotion flow for white
                                if (isWhite && targetZ == 7)
                                {
                                    Quaternion rot = rotation;
                                    Vector3 pos = transform.position;
                                    GameObject oldSelected = selectedSquare;
                                    acceptingPlayerInput = false;
                                    StartCoroutine(RunQuiz(correct =>
                                    {
                                        if (correct)
                                        {
                                            Destroy(oldSelected.GetComponent<SquareScript>().GetPiece());
                                            hitSquare.AssignPiece(Instantiate(whiteInfiniteSumPrefab, pos, rot));
                                        }

                                        // cleanup & switch sides regardless of result
                                        selectedSquare = null;
                                        DisableAllHighlights();
                                        StartCoroutine(SwitchSides());
                                    }));
                                    return;
                                }

                                // Promotion flow for black
                                if (!isWhite && targetZ == 0)
                                {
                                    Quaternion rot = rotation;
                                    Vector3 pos = transform.position;
                                    GameObject oldSelected = selectedSquare;
                                    acceptingPlayerInput = false;
                                    StartCoroutine(RunQuiz(correct =>
                                    {
                                        if (correct)
                                        {
                                            Destroy(oldSelected.GetComponent<SquareScript>().GetPiece());
                                            hitSquare.AssignPiece(Instantiate(blackInfiniteSumPrefab, pos, rot));
                                        }

                                        // cleanup & switch sides regardless of result
                                        selectedSquare = null;
                                        DisableAllHighlights();
                                        StartCoroutine(SwitchSides());
                                    }));
                                    return;
                                }
                            }
                            else
                            {
                                hitSquare.AssignPiece(selectedSquareScript.GetPiece());
                                selectedSquareScript.UnassignPiece();
                                Debug.Log("Unassigned " + hitSquare.GetPiece() + " from " + selectedSquare.name + " and assigned it to " + hitObject.name);
                            }

                            selectedSquare = null;
                            DisableAllHighlights();
                            StartCoroutine(SwitchSides());
                        }
                        // Move to square that has another piece
                        else if (canMoveToTarget && targetHasPiece)
                        {
                            if (hitSquare.GetPiece().CompareTag(tagToCheck)) // combining
                            {
                                Piece.PieceType typeOfInitialPiece = selectedSquareScript.GetPieceType();
                                Destroy(selectedSquareScript.GetPiece());
                                if (typeOfInitialPiece != Piece.PieceType.Sigma)
                                {
                                    Destroy(hitSquare.GetPiece());
                                }
                                else
                                {
                                    selectedSquareScript.AssignPiece(hitSquare.GetPiece());
                                    hitSquare.UnassignPiece();
                                    hitSquare.AssignPiece(isWhite ? Instantiate(whiteSigmaPrefab, transform.position, rotation) : Instantiate(blackSigmaPrefab, transform.position, rotation));
                                }

                                switch (typeOfInitialPiece)
                                {
                                    case Piece.PieceType.X:
                                        hitSquare.AssignPiece(isWhite ? Instantiate(whiteFunctionPrefab, transform.position, rotation) : Instantiate(blackFunctionPrefab, transform.position, rotation));
                                        break;
                                    case Piece.PieceType.Y:
                                        hitSquare.AssignPiece(isWhite ? Instantiate(whiteFunctionPrefab, transform.position, rotation) : Instantiate(blackFunctionPrefab, transform.position, rotation));
                                        break;
                                    case Piece.PieceType.Delta:
                                        hitSquare.AssignPiece(isWhite ? Instantiate(whiteDeltaEpsilonPrefab, transform.position, rotation) : Instantiate(blackDeltaEpsilonPrefab, transform.position, rotation));
                                        break;
                                    case Piece.PieceType.Epsilon:
                                        hitSquare.AssignPiece(isWhite ? Instantiate(whiteDeltaEpsilonPrefab, transform.position, rotation) : Instantiate(blackDeltaEpsilonPrefab, transform.position, rotation));
                                        break;
                                }

                                selectedSquare = null;
                                if (isWhite)
                                {
                                    if (hitSquare.GetPieceType() == Piece.PieceType.Sigma && hitSquare.GetZ() == 7)
                                    {
                                        Destroy(hitSquare.GetPiece());
                                        hitSquare.AssignPiece(Instantiate(whiteInfiniteSumPrefab, transform.position, rotation));
                                    }
                                }
                                else
                                {
                                    if (hitSquare.GetPieceType() == Piece.PieceType.Sigma && hitSquare.GetZ() == 0)
                                    {
                                        Destroy(hitSquare.GetPiece());
                                        hitSquare.AssignPiece(Instantiate(blackInfiniteSumPrefab, transform.position, rotation));
                                    }
                                }

                                DisableAllHighlights();
                                StartCoroutine(SwitchSides());
                            }
                            else // capturing
                            {
                                // require quiz to complete capture
                                GameObject oldSelected = selectedSquare;
                                SquareScript oldSelectedScript = selectedSquareScript;
                                GameObject pieceToMove = selectedSquareScript.GetPiece();
                                acceptingPlayerInput = false;
                                StartCoroutine(RunQuiz(correct =>
                                {
                                    if (correct)
                                    {
                                        // perform capture
                                        if (hitSquare.GetPiece() != null)
                                            Destroy(hitSquare.GetPiece());

                                        hitSquare.AssignPiece(pieceToMove);
                                        oldSelectedScript.UnassignPiece();
                                        Debug.Log("Captured: moved " + hitSquare.GetPiece() + " to " + hitObject.name);

                                        // Promotion check only if move succeeded
                                        if (isWhite)
                                        {
                                            if (hitSquare.GetPieceType() == Piece.PieceType.Sigma && hitSquare.GetZ() == 7)
                                            {
                                                Destroy(hitSquare.GetPiece());
                                                hitSquare.AssignPiece(Instantiate(whiteInfiniteSumPrefab, transform.position, rotation));
                                            }
                                        }
                                        else
                                        {
                                            if (hitSquare.GetPieceType() == Piece.PieceType.Sigma && hitSquare.GetZ() == 0)
                                            {
                                                Destroy(hitSquare.GetPiece());
                                                hitSquare.AssignPiece(Instantiate(blackInfiniteSumPrefab, transform.position, rotation));
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Debug.Log("Capture quiz failed - capture not performed");
                                    }

                                    // cleanup & switch sides regardless of result
                                    selectedSquare = null;
                                    DisableAllHighlights();
                                    StartCoroutine(SwitchSides());
                                }));
                                return;
                            }
                        }
                        // Select a different friendly piece
                        else if (!selectedSquareScript.CanMoveToSquare(targetX, targetZ) && targetHasPiece && hitSquare.GetPiece().CompareTag(tagToCheck))
                        {
                            selectedSquare = hitObject;
                            Debug.Log("Selected: " + selectedSquare.name + " Type: " + hitSquare.GetPieceType());
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
    
    private IEnumerator AskQuestion()
    {
        switch (selectedSquare.gameObject.GetComponent<SquareScript>().GetPieceType())
        {
            case Piece.PieceType.Sigma:
                questionManager.DisplayQuestion(QuestionManagerScript.QuestionToGet.Series);
                break;
            case Piece.PieceType.InfiniteSum:
                questionManager.DisplayQuestion(QuestionManagerScript.QuestionToGet.Series);
                break;
            case Piece.PieceType.DeltaEpsilon:
                questionManager.DisplayQuestion(QuestionManagerScript.QuestionToGet.Medium);
                break;
            case Piece.PieceType.Function:
                questionManager.DisplayQuestion(QuestionManagerScript.QuestionToGet.Medium);
                break;
            default:
                questionManager.DisplayQuestion(QuestionManagerScript.QuestionToGet.Any);
                break;
        }

        char choice = '\0';
        while (choice == '\0')
        {
            if (Keyboard.current != null && Keyboard.current.aKey.wasPressedThisFrame) choice = 'A';
            else if (Keyboard.current != null && Keyboard.current.bKey.wasPressedThisFrame) choice = 'B';
            else if (Keyboard.current != null && Keyboard.current.cKey.wasPressedThisFrame) choice = 'C';
            else if (Keyboard.current != null && Keyboard.current.dKey.wasPressedThisFrame) choice = 'D';

            yield return null;
        }


        gotQuestionRight = questionManager.InputAnswer(choice);
    }
    
    private IEnumerator RunQuiz(Action<bool> onComplete)
    {
        yield return StartCoroutine(AskQuestion());
        // pass the result produced by AskQuestion to the caller
        onComplete?.Invoke(gotQuestionRight);
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
