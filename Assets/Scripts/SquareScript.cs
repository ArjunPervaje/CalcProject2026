using UnityEngine;

public class SquareScript : MonoBehaviour
{
    private GameObject piece = null;
    private bool isSelected = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
}
