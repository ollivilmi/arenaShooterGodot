using Godot;

public class Main : Node2D
{
    private Node2D player1;
    private Node2D player2;

    public override void _Ready()
    {
        player1 = GetNode<Node2D>("Player1");
        player2 = GetNode<Node2D>("Player2");
    }

    public override void _Process(float delta) {
        if (Input.IsActionPressed("ui_cancel")) {
            GetTree().Quit();
        }
    }
    
    public void GameOver()
    {
        player1.Hide();
        player2.Hide();

        GetNode<HUD>("HUD").ShowGameOver();
        GetNode<AudioStreamPlayer2D>("Sounds/DeathSound").Play();
        GetNode<AudioStreamPlayer2D>("Sounds/Music").Stop();
    }

    public void NewGame()
    {
        player1.Call("Start");
        player2.Call("Start");

        GetNode<Timer>("StartTimer").Start();
        var hud = GetNode<HUD>("HUD");
        hud.ShowMessage("Get Ready!");
        GetNode<AudioStreamPlayer2D>("Sounds/Music").Play();
    }
}
