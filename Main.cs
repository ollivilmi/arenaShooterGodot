using Godot;

public class Main : Node2D
{
    private Player player1;
    private Player player2;

    public override void _Ready()
    {
        player1 = GetNode<Player>("Player1");
        player1.StartPosition = GetNode<Position2D>("Player1Start");
        player1.moveLeft = "p1_left";
        player1.moveRight = "p1_right";
        player1.moveUp = "p1_up";
        player1.moveDown = "p1_down";
        player1.controller = 0;
        player1.shoot = "p1_shoot";

        player2 = GetNode<Player>("Player2");
        player2.StartPosition = GetNode<Position2D>("Player2Start");
        player2.moveLeft = "p2_left";
        player2.moveRight = "p2_right";
        player2.moveUp = "p2_up";
        player2.moveDown = "p2_down";
        player2.controller = 1;
        player2.shoot = "p2_shoot";
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
        GetNode<AudioStreamPlayer2D>("DeathSound").Play();
        GetNode<AudioStreamPlayer2D>("Music").Stop();
    }

    public void NewGame()
    {
        player1.Start();
        player2.Start();

        GetNode<Timer>("StartTimer").Start();
        var hud = GetNode<HUD>("HUD");
        hud.ShowMessage("Get Ready!");
        GetNode<AudioStreamPlayer2D>("Music").Play();
    }
}
