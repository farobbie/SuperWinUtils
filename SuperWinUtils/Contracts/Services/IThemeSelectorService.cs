using Microsoft.UI.Xaml;

namespace SuperWinUtils.Contracts.Services;

public interface IThemeSelectorService
{
    ElementTheme Theme { get; }

    Action<ElementTheme>? ThemeChanged { get; set; }

    Task InitializeAsync();

    Task SetThemeAsync(ElementTheme theme);

    Task SetRequestedThemeAsync();
}
