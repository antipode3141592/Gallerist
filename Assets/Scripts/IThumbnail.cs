using UnityEngine;

namespace Gallerist
{
    public interface IThumbnail
    {
        public string Name { get; }
        public Sprite Image { get; }
    }
}