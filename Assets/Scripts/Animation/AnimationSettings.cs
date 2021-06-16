using UnityEngine;

namespace NavySpade.Animation
{
    public enum AnimationType
    {
        Fade,
        Scale
    }

    [CreateAssetMenu(fileName = "Animation", menuName = "Settings/Animation", order = 51)]
    public class AnimationSettings : ScriptableObject
    {
        public AnimationType type = default;
        public float time = 0.2f;
    }
}