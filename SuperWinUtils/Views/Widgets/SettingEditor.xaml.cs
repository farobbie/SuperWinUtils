using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System.Windows.Input;

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
}