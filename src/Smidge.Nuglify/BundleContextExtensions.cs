﻿using System.Text;
using NUglify.JavaScript;
using Smidge.CompositeFiles;

namespace Smidge.Nuglify
{
    internal static class BundleContextExtensions
    {
        /// <summary>
        /// Gets or Adds a V3InlineSourceMap into the current bundle context
        /// </summary>
        /// <param name="bundleContext"></param>
        /// <param name="sourceMapType"></param>
        /// <returns></returns>
        public static V3DeferredSourceMap GetSourceMapFromContext(this BundleContext bundleContext, SourceMapType sourceMapType)
        {
            var key = typeof(V3DeferredSourceMap).Name;
            object ctx;
            if (bundleContext.Items.TryGetValue(key, out ctx))
            {
                return (V3DeferredSourceMap)ctx;
            }

            //not in the context so add it
            var sb = new StringBuilder();
            var sourceMapWriter = new Utf8StringWriter(sb);
            var inlineSourceMap = new V3DeferredSourceMap((V3SourceMap)SourceMapFactory.Create(sourceMapWriter, V3SourceMap.ImplementationName), sb, sourceMapType);
            bundleContext.Items[key] = inlineSourceMap;
            return inlineSourceMap;
        }

    }
}