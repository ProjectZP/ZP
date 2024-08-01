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
        private const float DEFAULT_PLAY_TIME = 1f;
        private const float CHROMATIC_ABERRATION_INTENSITY_SCALE = 0.04f;
        private const float ATTACKED_SATURATION_SCALE = 9f / 255f;
        private const float RUNNING_LENS_DISTORTION_SCALE = 0.03f;

        [SerializeField] private Volume _youDiedVolume;
        [SerializeField] private Volume _runningVolume;
        [SerializeField] private Volume _attackedVolume;

        private Color _colorBuffer = new Color(1f,1f,1f);

        private void Awake()
        {
            if (_youDiedVolume == null)
                _youDiedVolume = transform.Find("YouDiedEffect").GetComponent<Volume>();
            if (_runningVolume == null)
                _runningVolume = transform.Find("RunningEffect").GetComponent<Volume>();
            if (_attackedVolume == null)
                _attackedVolume = transform.Find("AttackedEffect").GetComponent<Volume>();
        }

        public void PlayGrayScaleEffect()
        {
            if (_youDiedVolume.profile.TryGet(out ColorAdjustments colorAdjustments))
                StartCoroutine(C_PlayGrayScaleEffect(colorAdjustments));
        }

        private IEnumerator C_PlayGrayScaleEffect(ColorAdjustments colorAdjustments)
        {
            while (colorAdjustments.saturation.value > -100f)
            {
                colorAdjustments.saturation.Override(colorAdjustments.saturation.value - SATURATION_SCALE);
                yield return null;
            }
        }

        public void PlayGrainEffect()
        {
            if (_youDiedVolume.profile.TryGet(out FilmGrain filmGrain))
                StartCoroutine(C_PlayGrainEffect(filmGrain));
        }

        private IEnumerator C_PlayGrainEffect(FilmGrain filmGrain)
        {
            while (filmGrain.intensity.value < 1f)
            {
                filmGrain.intensity.Override(filmGrain.intensity.value + GRAIN_INTENSITY_SCALE);
                yield return null;
            }
        }

        public void PlayAttackedEffect()
        {
            if (_attackedVolume.profile.TryGet(out ColorAdjustments colorAdjustments)
                && _attackedVolume.profile.TryGet(out ChromaticAberration chromaticAberration))
                StartCoroutine(C_PlayAttackedEffect(colorAdjustments, chromaticAberration));
        }

        private IEnumerator C_PlayAttackedEffect(ColorAdjustments colorAdjustments, ChromaticAberration chromaticAberration, float time = DEFAULT_PLAY_TIME)
        {
            while(chromaticAberration.intensity.value < 1f)
            {
                _colorBuffer.g -= ATTACKED_SATURATION_SCALE;
                _colorBuffer.b -= ATTACKED_SATURATION_SCALE;

                colorAdjustments.colorFilter.Override(_colorBuffer);
             
                chromaticAberration.intensity.Override(
                    chromaticAberration.intensity.value + CHROMATIC_ABERRATION_INTENSITY_SCALE);
                yield return null;
            }

            _colorBuffer.g = _colorBuffer.b = 1f;
            colorAdjustments.colorFilter.Override(_colorBuffer);
            chromaticAberration.intensity.Override(0f);
        }

        public void PlayRunningEffect()
        {
            if (_runningVolume.profile.TryGet(out LensDistortion lensDistortion))
                StartCoroutine(C_PlayRunningEffect(lensDistortion));
        }

        private IEnumerator C_PlayRunningEffect(LensDistortion lensDistortion)
        {
            while (lensDistortion.intensity.value > -1f)
            {
                lensDistortion.intensity.Override(
                    lensDistortion.intensity.value - RUNNING_LENS_DISTORTION_SCALE);
                yield return null;
            }
        }

        public void StopRunningEffect()
        {
            if (_runningVolume.profile.TryGet(out LensDistortion lensDistortion))
                lensDistortion.intensity.Override(0f);
        }
    }
}
