#if ANDROID
using Maui.CollectionView.ScrollBar.Platforms.Android;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui.CollectionView.ScrollBar
{
    public static class MauiAppBuilderExtensions
    {
#if ANDROID
        public static MauiAppBuilder UseScrollBarInCollectionView(this MauiAppBuilder builder, Action<ScrollBarOptions>? config)
        {
            var opts = new ScrollBarOptions();
            config?.Invoke(opts);

            ScrollbarMapper.Configure(opts);
            return builder;
        }
#endif
    }
}
