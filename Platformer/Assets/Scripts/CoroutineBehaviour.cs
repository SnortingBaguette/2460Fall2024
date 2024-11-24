using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CoroutineBehaviour : MonoBehaviour
{
    public UnityEvent startEvent, startCountEvent, repeatCountEvent, endCountEvent, repeatUntilFalseEvent;

    public bool canRun;
    private WaitForSeconds wfsObj;
    public float seconds = 3.0f;
    private WaitForFixedUpdate wffuObj;
    public int counterNum = 0;

    private void Start()
    {
        startEvent.Invoke();
        wfsObj = new WaitForSeconds(seconds);
        wffuObj = new WaitForFixedUpdate();
    }

    private IEnumerator Counting()
    {


        startCountEvent.Invoke();
        yield return wfsObj;
        while (counterNum > 0)
        {
            repeatCountEvent.Invoke();
            counterNum--;
            yield return wfsObj;
        }
        endCountEvent.Invoke();
    }

    private IEnumerator RepeatUntilFalse()
    {
        while (canRun)
        {
            yield return wfsObj;
            repeatUntilFalseEvent.Invoke();
        }
    }

    public void StartRepeatUntilFalse()
    {
        canRun = true;
        StartCoroutine(RepeatUntilFalse());
    }

    public void StartCounting()
    {
        StartCoroutine(Counting());
    }

    public bool CanRun
    {
        get => canRun;
        set => canRun = value;
    }
}
