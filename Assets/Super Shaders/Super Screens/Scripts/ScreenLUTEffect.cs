using UnityEngine;

namespace SuperShaders.Screen
{
    [ExecuteInEditMode]
    public class ScreenLUTEffect : MonoBehaviour
    {
        public Texture2D lut = null;

        [Range(0f, 1f)]
        public float offset = 0f;

        [Range(0f, 1f)]
        public float strength = 1f;

        private Material material;

        private void Awake()
        {
            material = new Material(Shader.Find("Hidden/LUTScreenShader"));
        }

        private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            if (!material)
            {
                return;
            }

            material.SetTexture("_LUT", lut);

            material.SetFloat("_Offset", offset);
            material.SetFloat("_Strength", strength);

            Graphics.Blit(source, destination, material);
        }
    }
}