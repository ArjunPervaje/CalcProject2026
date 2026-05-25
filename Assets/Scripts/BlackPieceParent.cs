using UnityEngine;
[ExecuteAlways]
public class BlackPieceParent : MonoBehaviour
{
    void LateUpdate()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("BlackPiece");

        foreach (GameObject obj in objs)
        {
            if (obj.transform.parent == null)
            {
                obj.transform.SetParent(transform);
            }
        }
    }
}