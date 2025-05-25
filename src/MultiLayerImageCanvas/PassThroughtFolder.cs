#nullable enable
using System;
using System.Collections.Generic;

namespace net.rs64.TexTransCore.MultiLayerImageCanvas
{
    public class PassThoughtFolder<TTCE> : GrabLayer<TTCE>
    where TTCE : ITexTransCreateTexture
    , ITexTransLoadTexture
    , ITexTransCopyRenderTexture
    , ITexTransComputeKeyQuery
    , ITexTransGetComputeHandler
    , ITexTransDriveStorageBufferHolder
    {
        public List<LayerObject<TTCE>> Layers;
        public AlphaOperation AlphaOperator;

        public PassThoughtFolder(bool visible, AlphaMask<TTCE> alphaModifier, AlphaOperation alphaOperator, bool preBlendToLayerBelow, List<LayerObject<TTCE>> layers) : base(visible, alphaModifier, preBlendToLayerBelow)
        {
            Layers = layers;
            AlphaOperator = alphaOperator;
        }

        public override void GrabImage(TTCE engine, EvaluateContext<TTCE> evaluateContext, ITTRenderTexture grabTexture)
        {
            using (var nEvalCtx = EvaluateContext<TTCE>.NestContext(engine, grabTexture.Width, grabTexture.Hight, evaluateContext, AlphaMask, null, AlphaOperator))
            {
                new CanvasContext<TTCE>(engine).EvaluateForFlattened(grabTexture, nEvalCtx, CanvasContext<TTCE>.ToBelowFlattened(Layers));
            }
        }

        public override void Dispose()
        {
            base.Dispose();
            foreach (var l in Layers) l.Dispose();
        }
    }
}
