using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private CuttingCounter cuttingCounter; // Reference to the CuttingCounter script
    [SerializeField] private Image barImage; // Reference to the progress bar UI element

    private void Start()
    {
        cuttingCounter.OnProgressChanged += CuttingCounter_OnProgressChanged; // Subscribe to the OnProgressChanged event

        barImage.fillAmount = 0f; // Initialize the progress bar to 0
    }

    private void CuttingCounter_OnProgressChanged(object sender, CuttingCounter.OnProgressChangedEventArgs e)
    {
        barImage.fillAmount = e.progressNormalized; // Update the progress bar fill amount based on the normalized progress
    }
}
