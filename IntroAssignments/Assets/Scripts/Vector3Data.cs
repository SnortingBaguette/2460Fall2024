using UnityEngine;

[CreateAssetMenu]
public class Vector3Data : ScriptableObject
{
    public Vector3 vectorData;

    public void ResetPosition()
    {
        vectorData = new Vector3(0, 0, 0);
    }

    public void CopyVector(Vector3 acceptedVector)
    {
        vectorData = acceptedVector;
    }
}
