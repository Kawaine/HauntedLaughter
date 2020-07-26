using UnityEngine;

namespace SuperShaders.Screen
{
    [ExecuteInEditMode]
    public class ScreenRGBEffect : MonoBehaviour
    {
        [Range(0f, 1f)]
        public float red = 1f;

        [Range(0f, 1f)]
        public float green = 1f;

        [Range(0f, 1f)]
        public float blue = 1f;

        private Material material;

        private void Awake()
        {
            material = new Material(Shader.Find("Hidden/RGBScreenShader"));
        }

        private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            if (!material)
            {
                return;
            }

            material.SetFloat("_Red", red);
            material.SetFloat("_Green", green);
            material.SetFloat("_Blue", blue);

            Graphics.Blit(source, destination, material);
        }
    }
}