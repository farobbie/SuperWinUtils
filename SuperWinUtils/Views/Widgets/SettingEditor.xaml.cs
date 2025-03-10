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

    public ICommand ResetCommand
    {
        get => (ICommand)GetValue(ResetCommandProperty);
        set => SetValue(ResetCommandProperty, value);
    }
    public static readonly DependencyProperty ResetCommandProperty =
        DependencyProperty.Register(nameof(ResetCommand), typeof(ICommand), typeof(SettingEditor), new PropertyMetadata(null));
    public ImageSource SaveImage
    {
        get => (ImageSource)GetValue(SaveImageProperty);
        set => SetValue(SaveImageProperty, value);
    }
    public static readonly DependencyProperty SaveImageProperty =
        DependencyProperty.Register(nameof(SaveImage), typeof(ImageSource), typeof(SettingEditor), new PropertyMetadata(null));

    public ImageSource ResetImage
    {
        get => (ImageSource)GetValue(ResetImageProperty);
        set => SetValue(ResetImageProperty, value);
    }
    public static readonly DependencyProperty ResetImageProperty =
        DependencyProperty.Register(nameof(ResetImage), typeof(ImageSource), typeof(SettingEditor), new PropertyMetadata(null));

}