using System.Collections;
using UnityEngine;

public class TranslateObject : MonoBehaviour
{
    public float maxHeight = 15f;
    private WaitForFixedUpdate wffu = new WaitForFixedUpdate();
    public bool x = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void StartTranslation()
    {
        StartCoroutine(Translate());
    }

    public IEnumerator Translate()
    {
        yield return wffu;
        transform.Translate(Vector3.up * Time.deltaTime);
        if(transform.localPosition.y < maxHeight)
        {
            StartCoroutine(Translate());
        }
    }

    public void TranslateObj()
    {
        while (x == true)
        {
            transform.Translate(Vector3.up * Time.deltaTime);
            if (transform.localPosition.y >= maxHeight)
            {
                x = false;
            }
        }
    }
}
