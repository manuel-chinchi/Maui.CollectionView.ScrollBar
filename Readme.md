# Maui.CollectionView.ScrollBar

Enables a ScrollBar for the CollectionView control (Android API 29+)

## Overview

By default, MAUI has issues displaying the scrollbar for the `CollectionView` control, especially 
on Android. This currently applies to .NET 8, 9, and 10.

This package fixes that error. It is recommended to remove it if your version of MAUI displays 
the scrollbar correctly.

## Platforms

- Android: API 29+ (Android 10 or higher)
- iOS: No
- Windows: No

**NOTE: For platforms listed as "No," the package has no effect**


## Configuration

### Steps by step

1. Download the latest version of the package from the [releases](https://github.com/manuel-chinchi/Maui.CollectionView.ScrollBar/releases) (a .zip file).
2. Extract the .zip file and place the .nupkg package in a folder on your system, for example, `c:\offline-packages\mcsv1.0.0\`.
3. Open your solution and run the following command in a terminal (you must be located in the directory containing your MAUI .proj file).
    ```c#
    dotnet add package Maui.CollectionView.ScrollBar -s "c:\offline-packages\mcsv1.0.0\"
    ```
4. In the `MauiProgram.cs` add the next code in `CreateMauiApp` method

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

5. Configure your CollectionView control as usual. You should see the scrollbar appear 
   or disappear based on the control's properties.
   
   - Default: It reveals itself for an instant and then hides.
   - Always: always show
   - Never: always hidden

## Notes

This package is not available in Microsoft's official NuGet.org repository because the following 
error occurred when an attempt was made to upload it (even under very different names).

```
The package ID is reserved. You can upload your package with a different package ID. Reach out to support@nuget.org if you have questions
```


I hope this proves useful to anyone who needs it ;)