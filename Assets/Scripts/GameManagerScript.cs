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
    
    // Black Piece Prefabs
    public GameObject blackXPrefab;
    public GameObject blackEpsilonPrefab;
    public GameObject blackPiPrefab;
    public GameObject blackYPrefab;
    public GameObject blackSigmaPrefab;
    public GameObject blackDeltaPrefab;
    
    private GameObject[,] board;

    private bool acceptingPlayerInput = true;
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
        
        
    }

    void Update()
    {
        if (acceptingPlayerInput)
        {
            if (Keyboard.current != null && Keyboard.current.rKey.wasPressedThisFrame)
            {
                GameRestart();
            }
        }
    }

    public void GameRestart()
    {
        acceptingPlayerInput = false;
        StartCoroutine(RemoveAllPieces());
        PlaceAllPeices();
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
        Instantiate(whiteXPrefab, new Vector3(board[0, 0].transform.position.x, 0.5f, board[0, 0].transform.position.z), Quaternion.identity);
        Instantiate(whiteDeltaPrefab, new Vector3(board[1, 0].transform.position.x, 0.5f, board[1, 0].transform.position.z), Quaternion.identity);
        Instantiate(whitePiPrefab, new Vector3(board[2, 0].transform.position.x, 0.5f, board[2, 0].transform.position.z), Quaternion.identity);
        Instantiate(whiteSigmaPrefab, new Vector3(board[3, 0].transform.position.x, 0.5f, board[3, 0].transform.position.z), Quaternion.identity);
        Instantiate(whiteYPrefab, new Vector3(board[4, 0].transform.position.x, 0.5f, board[4, 0].transform.position.z), Quaternion.identity);
        Instantiate(whitePiPrefab, new Vector3(board[5, 0].transform.position.x, 0.5f, board[5, 0].transform.position.z), Quaternion.identity);
        Instantiate(whiteEpsilonPrefab, new Vector3(board[6, 0].transform.position.x, 0.5f, board[6, 0].transform.position.z), Quaternion.identity);
        Instantiate(whiteXPrefab, new Vector3(board[7, 0].transform.position.x, 0.5f, board[7, 0].transform.position.z), Quaternion.identity);
        
        // Black Piece Setup
        Instantiate(blackXPrefab, new Vector3(board[7, 7].transform.position.x, 0.5f, board[7, 7].transform.position.z), Quaternion.identity);
        Instantiate(blackDeltaPrefab, new Vector3(board[6, 7].transform.position.x, 0.5f, board[6, 7].transform.position.z), Quaternion.identity);
        Instantiate(blackPiPrefab, new Vector3(board[5, 7].transform.position.x, 0.5f, board[5, 7].transform.position.z), Quaternion.identity);
        Instantiate(blackSigmaPrefab, new Vector3(board[4, 7].transform.position.x, 0.5f, board[4, 7].transform.position.z), Quaternion.identity);
        Instantiate(blackYPrefab, new Vector3(board[3, 7].transform.position.x, 0.5f, board[3, 7].transform.position.z), Quaternion.identity);
        Instantiate(blackPiPrefab, new Vector3(board[2, 7].transform.position.x, 0.5f, board[2, 7].transform.position.z), Quaternion.identity);
        Instantiate(blackEpsilonPrefab, new Vector3(board[1, 7].transform.position.x, 0.5f, board[1, 7].transform.position.z), Quaternion.identity);
        Instantiate(blackXPrefab, new Vector3(board[0, 7].transform.position.x, 0.5f, board[0, 7].transform.position.z), Quaternion.identity);
    }
}
