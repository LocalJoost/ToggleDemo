
namespace HoloToolkitExtensions
{
    public class ActiveTogglable : Togglable
    {
        public bool IsActive = true;

        public virtual void Start()
        {
            gameObject.SetActive(IsActive);
        }

        public override void Toggle()
        {
            IsActive = !IsActive;
            gameObject.SetActive(IsActive);
        }

        public virtual void Update()
        {
            // This code to make sure the logic still works in someone
            // set the IsActive field directly
            if (IsActive != gameObject.activeSelf)
            {
                gameObject.SetActive(IsActive);
            }
        }
    }
}
