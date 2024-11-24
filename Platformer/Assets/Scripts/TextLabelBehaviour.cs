using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(TextMeshProUGUI))]

public class TextLabelBehaviour : MonoBehaviour
{
    private TextMeshProUGUI label;
    public UnityEvent startEvent;

    private void Start()
    {
        label = GetComponent<TextMeshProUGUI>();
        startEvent.Invoke();
    }

    public void UpdateLabel(FloatData obj)
    {
        label.text = obj.value.ToString();

    }

    public void UpdateLabel(IntData obj)
    {
        label.text = obj.value.ToString();
    }

}
