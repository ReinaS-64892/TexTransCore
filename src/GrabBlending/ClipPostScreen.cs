#nullable enable
using System;

namespace net.rs64.TexTransCore.MultiLayerImageCanvas
{
    public class ClipPostScreen : ITTGrabBlending
    {
        public ClipPostScreen() { }
        public void GrabBlending<TTCE>(TTCE engine, ITTRenderTexture grabTexture)
        where TTCE : ITexTransCreateTexture
        , ITexTransComputeKeyQuery
        , ITexTransGetComputeHandler
        , ITexTransDriveStorageBufferHolder
        {
            using var computeHandler = engine.GetComputeHandler(engine.GetExKeyQuery<IBlendingComputeKey>().GrabBlend[nameof(ClipPostScreen)]);

            var texID = computeHandler.NameToID("Tex");
            computeHandler.SetTexture(texID, grabTexture);

            computeHandler.DispatchWithTextureSize(grabTexture);

        }
    }
}

/*
Clip Studio Paint が V1 よりも上のバージョンで、白くスクリーンするような挙動を色のチャンネルだけに施し出力する挙動を再現するためのワークアラウンドのための存在。
*/
