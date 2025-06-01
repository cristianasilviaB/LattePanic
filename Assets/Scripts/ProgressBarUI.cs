using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private CuttingCounter cuttingCounter; // Reference to the CuttingCounter script
    [SerializeField] private Image barImage; // Reference to the progress bar UI element
}
