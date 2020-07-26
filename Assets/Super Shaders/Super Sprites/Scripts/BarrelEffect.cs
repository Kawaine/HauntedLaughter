using UnityEngine;

namespace SuperShaders
{
    [ExecuteInEditMode]
    public class BarrelEffect : MonoBehaviour
    {
        [Range(-5f, 5f)]
        public float distortion = -0.7f;

        [Range(-5f, 5f)]
        public float view = 1f;

        public Vector2 shift = Vector2.one;

        private SpriteRenderer spriteRenderer;

        private void OnEnable()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();

            UpdateEffect(true);
        }

        private void OnDisable()
        {
            UpdateEffect(false);
        }

        private void Update()
        {
            UpdateEffect(true);
        }

        private void UpdateEffect(bool enable)
        {
            MaterialPropertyBlock mpb = new MaterialPropertyBlock();

            spriteRenderer.GetPropertyBlock(mpb);

            mpb.SetFloat("_Distortion", enable ? distortion : -0.7f);
            mpb.SetFloat("_View", enable ? view : 1f);

            mpb.SetFloat("_HorizontalShift", enable ? shift.x : 0f);
            mpb.SetFloat("_VerticalShift", enable ? shift.y : 0f);

            spriteRenderer.SetPropertyBlock(mpb);
        }
    }
}
