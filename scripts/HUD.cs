using Godot;
using System;

public class HUD : CanvasLayer
{
    [Signal]
    public delegate void StartGame();

    private Label message;
    private ColorRect messageBox;

    private Timer messageTimer;
    private Button startButton;

    public override void _Ready()
    {
        message = GetNode<Label>("Message");
        messageTimer = GetNode<Timer>("MessageTimer");
        messageBox = GetNode<ColorRect>("MessageBox");
        startButton = GetNode<Button>("StartButton");
    }

    public void ShowMessage(string text)
    {
        message.Text = text;
        message.Show();
        messageBox.Show();
        messageTimer.Start();
    }

    async public void ShowGameOver()
    {
        ShowMessage("Game Over");

        await ToSignal(messageTimer, "timeout");

        message.Text = "PvP arena";
        message.Show();
        messageBox.Show();

        await ToSignal(GetTree().CreateTimer(1), "timeout");
        startButton.Show();
    }

    public void OnStartButtonPressed()
    {
        startButton.Hide();
        EmitSignal("StartGame");
    }

    public void OnMessageTimerTimeout()
    {
        message.Hide();
        messageBox.Hide();
    }
}
