using UnityEngine;

namespace SuperShaders.Screen
{
    [ExecuteInEditMode]
    public class ScreenVignetteEffect : MonoBehaviour
    {
        [Range(0f, 1f)]
        public float radius = 0.9f;

        [Range(0f, 1f)]
        public float softness = 0.4f;

        [Range(0f, 1f)]
        public float horizontalOffset = 0.5f;

        [Range(0f, 1f)]
        public float verticalOffset = 0.5f;

        private Material material;

        private void Awake()
        {
            material = new Material(Shader.Find("Hidden/VignetteScreenShader"));
        }

        private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            if (!material)
            {
                return;
            }

            material.SetFloat("_Radius", radius);
            material.SetFloat("_Softness", softness);
            material.SetVector("_Offset", new Vector2(horizontalOffset, verticalOffset));

            Graphics.Blit(source, destination, material);
        }
    }
}