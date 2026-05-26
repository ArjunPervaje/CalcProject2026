using System;
using System.Collections;
using UnityEditor.PackageManager;
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
    
    // Black Piece Prefabs
    public GameObject blackXPrefab;
    public GameObject blackEpsilonPrefab;
    public GameObject blackPiPrefab;
    public GameObject blackYPrefab;
    public GameObject blackSigmaPrefab;
    public GameObject blackDeltaPrefab;
    
    private GameObject[,] board;
    
    // Camera
    private GameObject camera;

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
            }
        }

        /* By Now, the coordinates should all correspond to a square, and you can get the square's
        location by doing board[x, y].transform.position.x or .z for x and z respectfully */

        camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void FixedUpdate()
    {
        if (!isWhite)
        {
            rotation = Quaternion.Euler(Quaternion.identity.x, 180, Quaternion.identity.z);
            camera.transform.rotation = Quaternion.Euler(90f, 180f, 0f);
        }
        else
        {
            rotation = Quaternion.identity;
            camera.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
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
                Ray ray = camera.GetComponent<Camera>().ScreenPointToRay(mousePos);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
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
                            Debug.Log("Selected: " + selectedSquare.name);
                        }
                    }

                    if (selectedSquare != null && selectedSquare != hit.collider.gameObject)
                    {
                        if (!hit.collider.gameObject.GetComponent<SquareScript>().HasPiece())
                        {
                            hit.collider.gameObject.GetComponent<SquareScript>()
                                .AssignPiece(selectedSquare.GetComponent<SquareScript>().GetPiece());
                            selectedSquare.GetComponent<SquareScript>().UnassignPiece();
                            Debug.Log("Unassigned " + hit.collider.gameObject.GetComponent<SquareScript>().GetPiece() +
                                      " from " + selectedSquare.name + " and assigned it to " +
                                      hit.collider.gameObject.name);
                            selectedSquare = null;
                            StartCoroutine(SwitchSides());
                        }
                        else if (!hit.collider.gameObject.GetComponent<SquareScript>().GetPiece().CompareTag(tagToCheck))
                        {
                            Destroy(hit.collider.gameObject.GetComponent<SquareScript>().GetPiece());
                            hit.collider.gameObject.GetComponent<SquareScript>()
                                .AssignPiece(selectedSquare.GetComponent<SquareScript>().GetPiece());
                            selectedSquare.GetComponent<SquareScript>().UnassignPiece();
                            Debug.Log("Unassigned " + hit.collider.gameObject.GetComponent<SquareScript>().GetPiece() +
                                      " from " + selectedSquare.name + " and assigned it to " +
                                      hit.collider.gameObject.name);
                            selectedSquare = null;
                            StartCoroutine(SwitchSides());
                        }
                        else
                        {
                            selectedSquare = hit.collider.gameObject;
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
        
        board[7, 7].GetComponent<SquareScript>().AssignPiece(Instantiate(blackXPrefab, transform.position, rotation));
        board[6, 7].GetComponent<SquareScript>().AssignPiece(Instantiate(blackDeltaPrefab, transform.position, rotation));
        board[5, 7].GetComponent<SquareScript>().AssignPiece(Instantiate(blackPiPrefab, transform.position, rotation));
        board[4, 7].GetComponent<SquareScript>().AssignPiece(Instantiate(blackSigmaPrefab, transform.position, rotation));
        board[3, 7].GetComponent<SquareScript>().AssignPiece(Instantiate(blackYPrefab, transform.position, rotation));
        board[2, 7].GetComponent<SquareScript>().AssignPiece(Instantiate(blackPiPrefab, transform.position, rotation));
        board[1, 7].GetComponent<SquareScript>().AssignPiece(Instantiate(blackEpsilonPrefab, transform.position, rotation));
        board[0, 7].GetComponent<SquareScript>().AssignPiece(Instantiate(blackXPrefab, transform.position, rotation));
    }
}
