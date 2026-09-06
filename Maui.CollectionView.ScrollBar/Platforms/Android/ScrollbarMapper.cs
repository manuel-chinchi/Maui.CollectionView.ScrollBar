using Microsoft.Maui.Controls.Handlers.Items;
using Android.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Graphics.Drawables;
using AndroidX.AppCompat.Content.Res;
using Android.OS;
using Maui.CollectionView.ScrollBar.Platforms.Android;

namespace Maui.CollectionView.ScrollBar
{
    public static class ScrollbarMapper
    {
        static ScrollBarOptions? s_options = new();

        const string DEFAULT_SB_COLOR = "#919191";
        const float DEFAULT_SB_CORNER_RADIUS = 0f;

        public static void Configure(ScrollBarOptions? options = null)
        {
            s_options = options;

            CollectionViewHandler.Mapper.AppendToMapping("AlwaysVisibleScrollbar", (handler, view) =>
            {
                Apply(handler.PlatformView);
            });
        }

        private static void Apply(Android.Views.View? platformView)
        {
            if (platformView is null)
                return;

            platformView.VerticalScrollBarEnabled = true;
            platformView.ScrollBarStyle = ScrollbarStyles.OutsideOverlay;
            platformView.ScrollbarFadingEnabled = true;
            platformView.ScrollBarSize = 12;

            var context = Microsoft.Maui.ApplicationModel.Platform.CurrentActivity ?? Android.App.Application.Context;

            Drawable? thumb = null;

            if (s_options != null && s_options.ScrollBarResourceId.HasValue)
            {
                try
                {
                    thumb = AppCompatResources.GetDrawable(context, s_options.ScrollBarResourceId.Value);
                }
                catch
                {
                    // nothing
                }
            }

            if (thumb is null && s_options != null && s_options.UseDefaultScrollBar)
            {
                var gd = new GradientDrawable();

                gd.SetShape(Android.Graphics.Drawables.ShapeType.Rectangle);
                gd.SetColor(Android.Graphics.Color.ParseColor(DEFAULT_SB_COLOR));
                gd.SetCornerRadius(DEFAULT_SB_CORNER_RADIUS);

                thumb = gd;
            }

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Q) // API 29+
            {
                platformView.VerticalScrollbarThumbDrawable = thumb;
            }
            else
            {
                try
                {
                    var mScrollCacheField = Java.Lang.Class.FromType(typeof(Android.Views.View)).GetDeclaredField("mScrollCache");
                    mScrollCacheField.Accessible = true;

                    var mScrollCache = mScrollCacheField.Get(platformView);
                    if (mScrollCache is null)
                        return;

                    var scrollBarField = mScrollCache.Class.GetDeclaredField("scrollBar");
                    scrollBarField.Accessible = true;

                    var scrollBar = scrollBarField.Get(mScrollCache);
                    if (scrollBar is null)
                        return;

                    var method = scrollBar.Class.GetDeclaredMethod("setVerticalThumbDrawable",Java.Lang.Class.FromType(typeof(Drawable)));

                    method.Accessible = true;
                    method.Invoke(scrollBar, thumb);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"[Maui.CollectionView.ScrollBar] error: {ex.Message}");
                }
            }
        }
    }
}
