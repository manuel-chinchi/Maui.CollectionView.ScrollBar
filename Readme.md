# Maui.CollectionView.ScrollBar

Enables a ScrollBar for the CollectionView control (Android API 29+)

# Configuration

In the `MauiProgram.cs` add the next code in `CreateMauiApp` method

```c#
#if ANDROID
using Maui.CollectionView.ScrollBar;
#endif

    public static MauiApp.CreateMauiApp()
    {
        // defaul config builder
        var builder = MauiApp.CreateBuilder();

        builder
            .UserMauiApp<App>()
            .ConfigureFonts(fonts => 
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // config Maui.CollectionView.ScrollBar 
#if ANDROID
    builder.UseScrollBarInCollectionView();
#endif

        return builder.Build();
    }
```

