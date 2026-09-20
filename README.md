# CS077 Week 4 - Repeated Signals

This raw starter deliberately opens a plain window. Build the UI and code using the illustrated tutorial in Resources. It contains no Scan, STOP or reset implementation.

## My runner and Attention First connection
The runner needs to see if a planet has lifeforms. The interface used by the runner is able to tell if hostile lifeforms are present or not on the planet. One test-driven revision is that how the runner detects lifeforms on the planet.

## Expected and observed tests

## AI question, change, test result and next step
Assuming your existing controls are named PacketInput and SignalOutput, you only need these two small additions.

1. MainWindow.xaml
File: MainWindow.xaml
Insertion location: Inside the existing <Grid> (or other root layout container), alongside your existing controls.

<Button Content="Reset"
        x:Name="ResetReceiverButton"
        Click="ResetReceiverButton_Click" />

You can position it using whatever layout your existing UI already uses (for example, add Grid.Row/Grid.Column if your grid requires them). No other XAML needs to change.

2. MainWindow.xaml.cs
File: MainWindow.xaml.cs
Insertion location: Inside the existing MainWindow class, but outside ProcessPackets. Do not modify ProcessPackets.

private void ResetReceiverButton_Click(object sender, RoutedEventArgs e)
{
    PacketInput.Text = "PING;PING;DISTRESS;STOP;PING";
    SignalOutput.Text = "Ready.";
}

What the pieces mean
Click="ResetReceiverButton_Click" connects the button's Click event to your C# method. When the user clicks the button, WPF calls that method.

x:Name="ResetReceiverButton" gives the button a name in XAML/C#. You don't actually need the name for this handler, but it identifies the button if you later need to reference it.

private void ResetReceiverButton_Click(object sender, RoutedEventArgs e) is the standard WPF Click-event handler signature:

private — the method is used by this window.

void — it doesn't return a value.

sender — the object that raised the event (the button).

RoutedEventArgs e — WPF's event information for the Click event.

Test
Change PacketInput.Text to something else, such as STOP;DISTRESS.

Change SignalOutput.Text to something else.

Click Reset.

Expected result:

PacketInput becomes PING;PING;DISTRESS;STOP;PING

SignalOutput becomes Ready.

ProcessPackets remains unchanged.



<Window x:Class="CS077.RepeatedSignals.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="CS077 Week 4 - raw starter" Height="450" Width="700">
    <Grid>
        <TextBox x:Name="PacketInput" MaxLength="200" Text="PING;PING;DISTRESS;STOP;PING"/>
        <TextBlock x:Name="SignalOutput" Text="Ready" TextWrapping="Wrap" FontFamily="Consolas" Margin="10,59,-10,-59"/>
        <Button x:Name="ScanButton" Click="ScanButton_Click" Content="Scan batch" HorizontalAlignment="Left" Margin="10,27,0,0" VerticalAlignment="Top" Height="27" Width="89"/>
        <Button x:Name="ResetReceiverButton" Click="ResetReceiverButton_Click" Content="Reset receiver" HorizontalAlignment="Left" Margin="109,27,0,0" VerticalAlignment="Top" Height="27" Width="89"/>
        <TextBlock x:Name="CallSign" Text="Viper" HorizontalAlignment="Left" Margin="10,0,0,30" VerticalAlignment="Bottom"/>
        <TextBlock x:Name="Pilot" Text="F-16" HorizontalAlignment="Left" Margin="10,0,0,10" VerticalAlignment="Bottom"/>
        <!-- Tutorial 2: replace this window markup with your receiver controls. -->
    </Grid>
</Window>
using System;
using System.Windows;


namespace CS077.RepeatedSignals;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void ScanButton_Click(object sender, RoutedEventArgs e)
    {
        string[] packets = PacketInput.Text.Split(';',
            StringSplitOptions.RemoveEmptyEntries |
            StringSplitOptions.TrimEntries);
        SignalOutput.Text = ProcessPackets(packets);
    }
    public static string ProcessPackets(string[] packets)
    {
        int processed = 0;
        string log = "";
        for (int index = 0; index < packets.Length && processed < 6; index++)
        {
            if (packets[index] == "STOP")
            {
                break;
            }
            log += $"{processed + 1}. {packets[index]}\n";
            processed++;
        }
        return log + $"Processed: {processed}";
    }

    private void ResetReceiverButton_Click(object sender, RoutedEventArgs e)
    {
        PacketInput.Text = "PING;PING;DISTRESS;STOP;PING";
        SignalOutput.Text = "Ready";
    }
}

## Whiteboard and course collaboration evidence

## How to run and sources

Open CS077.RepeatedSignals.csproj in Visual Studio on Windows with .NET desktop development. Press F5.
