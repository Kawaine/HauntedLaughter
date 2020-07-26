using Platformer.Mechanics;
using UnityEngine;

namespace SuperShaders.Screen
{
    [ExecuteInEditMode]
    public class ScreenScanlineEffect : MonoBehaviour
    {

        [Header("Keep color to greyscale for a transparent effect.")]
        public Color currentColor = Color.black;

        [Range(1, 10)]
        public int size = 4;

        [Range(0f, 1f)]
        public float delta = 0f;

        public Material material;

        private void Awake()
        {
            material = new Material(Shader.Find("Hidden/ScanlineScreenShader"));
        }

        private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            if (!material)
            {
                return;
            }

            material.SetColor("_Color", currentColor);
            material.SetFloat("_Size", size);
            material.SetFloat("_Delta", delta);

            Graphics.Blit(source, destination, material);
        }
    }
}