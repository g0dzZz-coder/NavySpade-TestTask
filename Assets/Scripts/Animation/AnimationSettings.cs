using DG.Tweening;
using System;
using UnityEngine;

namespace NavySpade.Animation
{
    [Serializable]
    public class AnimationSettings
    {
        [Range(0f, 10f)]
        public float duration = 0.5f;
    }

    [Serializable]
    public class LoopAnimationSettings : AnimationSettings
    {
        public LoopType loopType = LoopType.Incremental;
        public Ease ease = Ease.Flash;
    }
}