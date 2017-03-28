using UnityEngine;
namespace HoloToolkitExtensions
{
public class DynamicTextureDownloader : MonoBehaviour
{
    public string ImageUrl;
    public bool ResizePlane;

    private WWW _imageLoader = null;
    private string _previousImageUrl = null;
    private bool _appliedToTexture = false;

    private Vector3 _originalScale;

    void Start()
    {
        _originalScale = transform.localScale;
    }

    void Update()
    {
        CheckLoadImage();
    }

    private void CheckLoadImage()
    {
        // No image requested
        if (string.IsNullOrEmpty(ImageUrl))
        {
            return;
        }

        // New image set - reset status vars and start loading new image
        if (_previousImageUrl != ImageUrl)
        {
            _previousImageUrl = ImageUrl;
            _appliedToTexture = false;
            _imageLoader = new WWW(ImageUrl);
        }

        if (_imageLoader.isDone && !_appliedToTexture)
        {
            // Apparently an image was loading and is now done. Get the texture and apply
            _appliedToTexture = true;
            Destroy(GetComponent<Renderer>().material.mainTexture);
            GetComponent<Renderer>().material.mainTexture = _imageLoader.texture;
            Destroy(_imageLoader.texture);

            if (ResizePlane)
            {
                DoResizePlane();
            };
        }
    }

    private void DoResizePlane()
    {
        // Keep the longest edge at the same length
        if (_imageLoader.texture.width < _imageLoader.texture.height)
        {
            transform.localScale = new Vector3(
                _originalScale.z * _imageLoader.texture.width / _imageLoader.texture.height,
                _originalScale.y, _originalScale.z);
        }
        else
        {
            transform.localScale = new Vector3(
                _originalScale.x, _originalScale.y,
                _originalScale.x * _imageLoader.texture.height / _imageLoader.texture.width);
        }
    }
}
}