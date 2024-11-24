using UnityEngine;

[CreateAssetMenu]
public class EnumData : ScriptableObject
{
    public enum State
    {
        Standing,
        Walking,
        Running
    }
    public State characterState;


    public void Walking()
    {
        characterState = State.Walking;
    }

    public void Running()
    {
        characterState = State.Running;
    }

    public void Standing()
    {
        characterState = State.Standing;
    }
}
