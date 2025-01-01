/*using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine;

public class ScreenSpaceOutlines : ScriptableRendererFeature
{ 
    [System.Serializable]
    private class ViewSpaceNormalsTextureSettings
    {

    }
    private class ViewSpaceNormalsTexturePass : ScriptableRenderPass 
    {
        private ViewSpaceNormalsTextureSettings normalsTextureSettings;
        private readonly List<ShaderTagId> shaderTagIdList;
        private readonly RenderTargetHandle normals;
        public ViewSpaceNormalsTexturePass(RenderPassEvent renderPassEvent, ViewSpaceNormalsTextureSettings settings)
        {
            normalsMaterial = new Material(Shader.Find("Hidden/ViewSpaceNormalsShader"));
            shaderTagIdList = new List<ShaderTagId>
            {
                new shaderTagId("UniversalForward"), new shaderTagId("UniversalForwardOnly"), new shaderTagId("LightWeightForward"), new shaderTagId("SRDefaultUnlit")
            };
            this.renderPassEvent = renderPassEvent;
            normals.Init("_ScreenViewSpaceNormals");
        }
        public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
        {
            RenderTextureDescriptor normalsTextureDescriptor = cameraTextureDescriptor;
            normalsTextureDescriptor.colorFormat = normalsTextureSettings.colorFormat;
            normalsTextureDescriptor.depthBufferBits = normalsTextureDescriptor.depthBufferBits;
            cmd.GetTemporaryRT(normals.id, cameraTextureDescriptor, FilterMode.Point);
            ConfigureTarget(normals.Identifier());
            ConfigureClear(ClearFlag.All, normalsTextureSettings.backgroundColor);
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            if (!normalsMaterial)
                return;
            CommandBuffer cmd = CommandBufferPool.Get();
            using (new ProfilingScope(cmd, new ProfilingSampler("SceneViewSpaceNormalsTextureCreation")))
            {
                context.ExecuteCommandBuffer(cmd);
                cmd.Clear();
                DrawingSettings drawSettings = CreateDrawingSettings(shaderTagList, ref renderingData, renderingData.cameraData.defaultOpaqueSortFlags);
                draqSettings.overrideMaterial = normalsMaterial;
                FilteringSettings filteringSettings = FilteringSettings.defaultValue;
                context.DrawRenderers(renderingData.cullResults, ref drawSettings, ref filteringSettings);        
            }
            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }

        public override void OnCameraCleanup(CommandBuffer cmd)
        {
            cmd.ReleaseTemporaryRT(normals.id);
        }
    }

    private class ScreenSpaceOutlinePass : ScriptableRenderPass
    {

    }

    [SerializeField] private RenderPassEvent renderPassEvent;

    private ViewSpaceNormalsTexturePass viewSpaceNormalsTexturePass;
    private ScreenSpaceOutlinesPass screenSpaceOutlinesPass;

    public override void Create()
    {
        viewSpaceNormalsTexturePass = new ViewSpaceNormalsTexturePass(renderPassEvent);
        screenSpaceOutlinesPass = new ScreenSpaceOutlinesPass(renderPassEvent);
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        renderer.EnqueuePass(viewSpaceNormalsTexturePass);
        renderer.EnqueuePass(screenSpaceOutlinesPass);
    }

    [SerializeField] private ViewSpaceNormalsTextureSettings viewSpaceNormalsTextureSettings;

}
*/
