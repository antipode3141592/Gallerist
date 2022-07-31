using UnityEngine;

namespace Gallerist.UI
{
    public abstract class Display : MonoBehaviour, IDisplay
    {
        protected Vector3 originalPosition;
        protected float yOffset = 10000f;

        protected virtual void Awake()
        {
            originalPosition = transform.position;
            MoveOffscreen();
        }

        public virtual void Hide()
        {
            MoveOffscreen();
        }

        public virtual void Show()
        {
            MoveOnScreen();
        }

        protected void MoveOffscreen()
        {
            transform.position = originalPosition + new Vector3(0f, yOffset);
        }

        protected void MoveOnScreen()
        {
            transform.position = originalPosition;
        }
    }
}