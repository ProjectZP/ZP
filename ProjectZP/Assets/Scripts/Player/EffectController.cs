using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace ZP.SJH.Player
{
    public class EffectController : MonoBehaviour
    {
        private const float SATURATION_SCALE = 1f;
        private const float GRAIN_INTENSITY_SCALE = 0.1f;

        [SerializeField] private Volume _volume;

        private void Awake()
        {
            if (_volume == null)
                _volume = transform.Find("PostProcessVolume").GetComponent<Volume>();
        }

        public void SetGrayScaleEffect()
        {
            if (_volume.profile.TryGet(out ColorAdjustments colorAdjustments))
                StartCoroutine(C_SetGrayScaleEffect(colorAdjustments));
        }

        private IEnumerator C_SetGrayScaleEffect(ColorAdjustments colorAdjustments)
        {
            while (colorAdjustments.saturation.value > -100f)
            {
                colorAdjustments.saturation.Override(colorAdjustments.saturation.value - SATURATION_SCALE);
                yield return null;
            }
        }

        public void SetGrainEffect()
        {
            if (_volume.profile.TryGet(out FilmGrain filmGrain))
                StartCoroutine(C_SetGrainEffect(filmGrain));
        }

        private IEnumerator C_SetGrainEffect(FilmGrain filmGrain)
        {
            while (filmGrain.intensity.value < 1f)
            {
                filmGrain.intensity.Override(filmGrain.intensity.value + GRAIN_INTENSITY_SCALE);
                yield return null;
            }
        }
    }
}
