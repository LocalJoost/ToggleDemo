
using UnityEngine;

namespace HoloToolkitExtensions
{
    public class FadeTogglable : Togglable
    {
        public bool IsActive = true;

        public float RunningTime = 1.5f;

        private bool _isBusy = false;

        private Material _gameObjectMaterial;

        public virtual void Start()
        {
            Animate(0.0f);
            _gameObjectMaterial = gameObject.GetComponent<Renderer>().material;
        }

        public override void Toggle()
        {
            IsActive = !IsActive;
            Animate(RunningTime);
        }

        public virtual void Update()
        {

            // This code to make sure the logic still works in someone
            // set the IsActive field directly
            if (_isBusy)
            {
                return;
            }
            if (IsActive != (_gameObjectMaterial.color.a == 1.0f))
            {
                Animate(RunningTime);
            }
        }

        private void Animate(float timeSpan)
        {
            _isBusy = true;
            LeanTween.alpha(gameObject, 
                IsActive ? 1f : 0f, timeSpan).setOnComplete(() => _isBusy = false);
        }
    }
}
