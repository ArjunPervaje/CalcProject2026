using UnityEngine;
[ExecuteAlways]
public class WhitePieceParent : MonoBehaviour
{
    void LateUpdate()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("WhitePiece");

        foreach (GameObject obj in objs)
        {
            if (obj.transform.parent == null)
            {
                obj.transform.SetParent(transform);
            }
        }
    }
}