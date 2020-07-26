using UnityEngine;

namespace SuperShaders.Screen
{
    [ExecuteInEditMode]
    public class ScreenBarrelEffect : MonoBehaviour
    {
        [Range(-5f, 5f)]
        public float distortion = -0.7f;

        [Range(-5f, 5f)]
        public float view = 1f;

        public Vector2 shift = Vector2.one;

        private Material material;

        private void Awake()
        {
            material = new Material(Shader.Find("Hidden/BarrelScreenShader"));
        }

        private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            if (!material)
            {
                return;
            }

            material.SetFloat("_Distortion", distortion);
            material.SetFloat("_View", view);

            material.SetFloat("_HorizontalShift", shift.x);
            material.SetFloat("_VerticalShift", shift.y);

            Graphics.Blit(source, destination, material);
        }
    }
}