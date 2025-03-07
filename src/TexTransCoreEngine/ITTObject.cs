#nullable enable
using System;

namespace net.rs64.TexTransCore
{
    /// <summary>
    /// TexTransTool の Core 部分にて 扱われる メモリ管理を要求する Mesh や Texture などのオブジェクトを意味する存在
    /// </summary>
    public interface ITTObject : IDisposable
    {
        string Name { get; set; }
    }
}
