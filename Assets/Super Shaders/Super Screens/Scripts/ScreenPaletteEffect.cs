using UnityEngine;

namespace SuperShaders.Screen
{
    [ExecuteInEditMode]
    public class ScreenPaletteEffect : MonoBehaviour
    {
        [Header("Compatibility: Keep this effect above all others.")]
        public Color colorA = new Color(0.2f, 0.17f, 0.31f, 1);
        public Color colorB = new Color(0.27f, 0.53f, 0.56f, 1);
        public Color colorC = new Color(0.58f, 0.89f, 0.27f, 1);
        public Color colorD = new Color(0.89f, 0.95f, 0.89f, 1);

        [Range(0f, 1f)]
        public float intensity = 1f;

        [Range(0f, 1f)]
        public float threshold = 0.15f;

        private Material material;

        private void Awake()
        {
            material = new Material(Shader.Find("Hidden/PaletteScreenShader"));
        }

        private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            if (!material)
            {
                return;
            }

            material.SetColor("_ColorA", colorA);
            material.SetColor("_ColorB", colorB);
            material.SetColor("_ColorC", colorC);
            material.SetColor("_ColorD", colorD);

            material.SetFloat("_Intensity", intensity);
            material.SetFloat("_Threshold", threshold);

            Graphics.Blit(source, destination, material);
        }
    }
}