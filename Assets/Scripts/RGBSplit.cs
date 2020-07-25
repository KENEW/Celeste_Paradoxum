//	Copyright (c) KimPuppy.
//	http://bakak112.tistory.com/

using UnityEngine;

[ExecuteInEditMode]
public class RGBSplit : MonoBehaviour
{
    public Shader curShader;
    public Vector2 RCoord, GCoord, BCoord;
    private Material curMaterial;

    private Material material
    {
        get
        {
            if (curMaterial == null)
            {
                curMaterial = new Material(curShader);
                curMaterial.hideFlags = HideFlags.HideAndDontSave;
            }
            return curMaterial;
        }
    }

    private void Start()
    {
        if (!SystemInfo.supportsImageEffects)
        {
            enabled = false;
            return;
        }
    }

    private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
    {
        if (curShader != null)
        {
            material.SetVector("_RCoord", RCoord);
            material.SetVector("_GCoord", GCoord);
            material.SetVector("_BCoord", BCoord);
            Graphics.Blit(sourceTexture, destTexture, material);
        }
        else
        {
            Graphics.Blit(sourceTexture, destTexture);
        }
    }

    private void OnDisable()
    {
        if (curMaterial)
        {
            DestroyImmediate(curMaterial);
        }
    }
}