using UnityEngine;

public class DiceFaceReader : MonoBehaviour
{
    public int GetTopFaceValue()
    {
        Vector3 up = transform.up;

        // How the dice faces work
        Vector3[] directions = {
            -transform.right,     // 1 (left)
            -transform.forward,   // 2 (back)
            transform.right,      // 3 (right)
            transform.forward,    // 4 (front)
            -transform.up,        // 5 (bottom)
            transform.up          // 6 (top)
        };

        float maxDot = -1f;
        int faceValue = 1;

        //How the face value of the dice is found
        for (int i = 0; i < directions.Length; i++)
        {
            float dot = Vector3.Dot(directions[i], Vector3.up);
            if (dot > maxDot)
            {
                maxDot = dot;
                faceValue = i + 1; 
            }
        }

        return faceValue;
    }
}
