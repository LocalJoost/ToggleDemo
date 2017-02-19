using System;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

namespace HoloToolkitExtensions
{
    public class Toggler : MonoBehaviour, IInputClickHandler
    {
        private AudioSource _selectSound;

        public List<Togglable> Toggles = new List<Togglable>();

        public virtual void Start()
        {
            _selectSound = GetComponent<AudioSource>();
        }

        public virtual void OnInputClicked(InputClickedEventData eventData)
        {
            foreach (var toggle in Toggles)
            {
                toggle.Toggle();
            }
            if (_selectSound != null)
            {
                _selectSound.Play();
            }
        }
    }
}
