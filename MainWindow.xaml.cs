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