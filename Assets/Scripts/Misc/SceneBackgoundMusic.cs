using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(AudioSource))]
    public class SceneBackgoundMusic : SingletonBase<SceneBackgoundMusic>
    {
        protected override void Awake()
        {
            base.Awake();

            if (TryGetComponent(out AudioSource BGMSource))
            {
                BGMSource.ignoreListenerPause = true;
            }
        }
    }
}
