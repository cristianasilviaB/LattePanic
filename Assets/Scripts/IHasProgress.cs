using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasProgress 
{
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged; // Event to notify when cutting progress changes
    public class OnProgressChangedEventArgs : EventArgs
    {
        public float progressNormalized; // Variable to hold the current cutting progress
    }
}
