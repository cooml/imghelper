using LZ4;
using System;
using System.Collections.Generic;
using System.Text;

namespace imgtobase64
{
  /// <summary>
  /// LZ4 压缩工具类
  /// </summary>
  public class LZ4Lib
  {
    /// <summary>
    /// 压缩文本
    /// </summary>
    /// <param name="text">文本内容</param>
    /// <returns></returns>
    public static string CompressBuffer(string text)
    {
      var compressed = Convert.ToBase64String(
          LZ4Codec.Wrap(Encoding.UTF8.GetBytes(text)));

      return compressed;
    }

    /// <summary>
    /// 解压文本
    /// </summary>
    /// <param name="compressed">压缩的文本</param>
    /// <returns></returns>
    public static string DecompressBuffer(string compressed)
    {
      var lorems =
          Encoding.UTF8.GetString(
              LZ4Codec.Unwrap(Convert.FromBase64String(compressed)));


      return lorems;
    }
  }
}
