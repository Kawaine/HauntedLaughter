using UnityEngine;

namespace SuperShaders
{
    [ExecuteInEditMode]
    public class CutoutEffect : MonoBehaviour
    {
        public Texture2D cutoutTexture = null;
        
        public Vector2 offset = Vector2.zero;

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

            if (cutoutTexture != null)
            {
                mpb.SetTexture("_CutoutTexture", cutoutTexture);
            }

            mpb.SetVector("_CutoutOffset", enable ? offset : Vector2.zero);

            spriteRenderer.SetPropertyBlock(mpb);
        }
    }
}
