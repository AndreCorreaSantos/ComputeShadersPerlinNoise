using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class computeShaderClass : MonoBehaviour
{
    public ComputeShader computeShader;
    
    public RenderTexture renderTexture;

    public Slider freqSlider;
    public Slider ampSlider;

    public Slider timeSlider;

    private float tCount = 0.0f;
    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        tCount = tCount+Time.deltaTime*timeSlider.value;
        float frequency = freqSlider.value;
        float amplitude = ampSlider.value;
        if (renderTexture == null){
            renderTexture = new RenderTexture(256,256,24);
            renderTexture.enableRandomWrite=true;
            renderTexture.Create();
        }
        computeShader.SetTexture(0,"Result",renderTexture);
        computeShader.SetFloat("resolution", renderTexture.width);

        computeShader.SetFloat("time",tCount);
        computeShader.SetFloat("frequency",frequency);
        computeShader.SetFloat("amplitude",amplitude);
        computeShader.Dispatch(0,renderTexture.width/8,renderTexture.height/8,1);
        Graphics.Blit(renderTexture,dest);
    }
}
