using HoloToolkit.Unity.InputModule;
using UnityEngine;

namespace HoloToolkitExtensions
{
    public class RollerBlindAnimatior : Togglable, IInputClickHandler
    {
        public float PlayTime = 1.5f;

        private AudioSource _selectSound;

        private float _foldedOutScale;
        private float _size;

        private bool _isBusy = false;

        public bool IsOpen { get; private set; }

        public void Start()
        {
            _selectSound = GetComponent<AudioSource>();
            _foldedOutScale = gameObject.transform.localScale.z;
            var startBounds = gameObject.GetComponent<Renderer>().bounds;
            _size = startBounds.size.y;
            AnimateObject(gameObject, 0, 0);
        }

        public void OnInputClicked(InputClickedEventData eventData)
        {
            Toggle();
        }

        public override void Toggle()
        {
            if (_isBusy)
            {
                return;
            }

            AnimateObject(gameObject, !IsOpen ? _foldedOutScale : 0, PlayTime);

            if (_selectSound != null)
            {
                _selectSound.Play();
            }

            IsOpen = !IsOpen;
        }

        private void AnimateObject(GameObject objectModel, float targetScale, float timeSpan)
        {
            _isBusy = true;

            var moveDelta = (targetScale == 0.0f ? _size : -_size) / 2f;
            LeanTween.moveLocal(objectModel,
                    new Vector3(objectModel.transform.localPosition.x,
                        objectModel.transform.localPosition.y + moveDelta,
                        objectModel.transform.localPosition.z), timeSpan);

            LeanTween.scale(objectModel,
                       new Vector3(objectModel.transform.localScale.x,
                           objectModel.transform.localScale.y,
                           targetScale), timeSpan).setOnComplete(() => _isBusy = false);
        }

    }
}
