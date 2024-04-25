using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Visualizer : MonoBehaviour
{
    [SerializeField]
    TMP_Text labelElement;

    public void SetLabel(string label)
    {
        labelElement.text = label;
    }
}
