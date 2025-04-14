using System.Windows.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SuperWinUtils.Views.Widgets;

public sealed partial class SettingEditor : UserControl
{
    public SettingEditor()
    {
        InitializeComponent();
    }

    public string Header
    {
        get => (string)GetValue(HeaderProperty);
        set => SetValue(HeaderProperty, value);
    }
    public static readonly DependencyProperty HeaderProperty =
        DependencyProperty.Register(nameof(Header), typeof(string), typeof(SettingEditor), new PropertyMetadata(string.Empty));

    public string Value
    {
        get => (string)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }
    public static readonly DependencyProperty ValueProperty =
        DependencyProperty.Register(nameof(Value), typeof(string), typeof(SettingEditor), new PropertyMetadata(string.Empty));

    public object CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }
    public static readonly DependencyProperty CommandParameterProperty =
        DependencyProperty.Register(nameof(CommandParameter), typeof(object), typeof(SettingEditor), new PropertyMetadata(null));

    public ICommand SaveCommand
    {
        get => (ICommand)GetValue(SaveCommandProperty);
        set => SetValue(SaveCommandProperty, value);
    }
    public static readonly DependencyProperty SaveCommandProperty =
        DependencyProperty.Register(nameof(SaveCommand), typeof(ICommand), typeof(SettingEditor), new PropertyMetadata(null));

    public ICommand RestoreCommand
    {
        get => (ICommand)GetValue(RestoreCommandProperty);
        set => SetValue(RestoreCommandProperty, value);
    }
    public static readonly DependencyProperty RestoreCommandProperty =
        DependencyProperty.Register(nameof(RestoreCommand), typeof(ICommand), typeof(SettingEditor), new PropertyMetadata(null));
    public string SaveToolTip
    {
        get => (string)GetValue(SaveToolTipProperty);
        set => SetValue(SaveToolTipProperty, value);
    }
    public static readonly DependencyProperty SaveToolTipProperty =
        DependencyProperty.Register(nameof(SaveToolTip), typeof(string), typeof(SettingEditor), new PropertyMetadata(string.Empty));

    public string RestoreToolTip
    {
        get => (string)GetValue(RestoreToolTipProperty);
        set => SetValue(RestoreToolTipProperty, value);
    }
    public static readonly DependencyProperty RestoreToolTipProperty =
        DependencyProperty.Register(nameof(RestoreToolTip), typeof(string), typeof(SettingEditor), new PropertyMetadata(string.Empty));

}