using UnityEngine;

[CreateAssetMenu]
public class IntData : ScriptableObject
{
    public int value;

    public void AddValue(int num)
    {
        value += num;
    }

    public void OverrideValue(int num)
    {
        value = num;
    }
}
