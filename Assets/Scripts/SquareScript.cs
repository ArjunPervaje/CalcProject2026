using UnityEngine;

public class SquareScript : MonoBehaviour
{
    private GameObject piece = null;
    // private bool isSelected = false;

    private int x;
    private int z;

    private Transform cylinder;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        x = (int)transform.position.x;
        z = (int)transform.position.z;
        cylinder = transform.Find("Cylinder");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AssignPiece(GameObject newPiece)
    {
        piece = newPiece;
        piece.transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
    }

    public bool HasPiece()
    {
        return piece != null;
    }

    public GameObject GetPiece()
    {
        return piece;
    }

    public void UnassignPiece()
    {
        piece = null;
    }

    public Piece.PieceType GetPieceType()
    {
        return piece.GetComponent<Piece>().GetPieceType();
    }

    public bool CanMoveToSquare(int desX, int desZ)
    {
        return piece.GetComponent<Piece>().CheckIfCanMove(desX, desZ, this.x, this.z);
    }
    
    public int GetX()
    {
        return x;
    }
    public int GetZ()
    {
        return z;
    }

    public void EnableHighlight()
    {
        cylinder.gameObject.SetActive(true);
    }
    
    public void DisableHighlight()
    {
        cylinder.gameObject.SetActive(false);
    }
}
