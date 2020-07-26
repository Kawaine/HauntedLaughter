using UnityEngine;

namespace SuperShaders
{
    [ExecuteInEditMode]
    public class RGBEffect : MonoBehaviour
    {
        [Range(0f, 1f)]
        public float red = 1f;

        [Range(0f, 1f)]
        public float green = 1f;

        [Range(0f, 1f)]
        public float blue = 1f;

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

            mpb.SetFloat("_Red", enable ? red : 1f);
            mpb.SetFloat("_Green", enable ? green : 1f);
            mpb.SetFloat("_Blue", enable ? blue : 1f);

            spriteRenderer.SetPropertyBlock(mpb);
        }
    }
}
