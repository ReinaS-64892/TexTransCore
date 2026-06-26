#nullable enable
namespace net.rs64.TexTransCore.MultiLayerImageCanvas
{
    // 何らか Workaround 無ようなものの場合に使われることを想定している。余り使うべきではない存在
    public class GrabDirectLayer<TTCE> : GrabLayer<TTCE>
    where TTCE : ITexTransCreateTexture
    , ITexTransLoadTexture
    , ITexTransCopyRenderTexture
    , ITexTransComputeKeyQuery
    , ITexTransGetComputeHandler
    , ITexTransDriveStorageBufferHolder
    {
        ITTGrabBlending _grabBlendingObject;
        public GrabDirectLayer(bool visible, bool preBlendToLayerBelow, ITTGrabBlending grabBlending) : base(visible, new NoMask<TTCE>(), preBlendToLayerBelow)
        {
            _grabBlendingObject = grabBlending;
        }

        public override void GrabImage(TTCE engine, EvaluateContext<TTCE> evaluateContext, ITTRenderTexture grabTexture)
        {
            _grabBlendingObject.GrabBlending(engine, grabTexture);
        }
    }
}
