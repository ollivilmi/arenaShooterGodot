using Godot;

public class Main : Node2D
{
    private Player player1;
  //  private Player player2;


    public override void _Ready()
    {
        player1 = GetNode<Player>("Player1");
        player1.StartPosition = GetNode<Position2D>("Player1Start");
       // player2 = GetNode<Player>("Player2");
    //    player2.StartPosition = GetNode<Position2D>("Player2Start");
    }

    public override void _Process(float delta) {
        if (Input.IsActionPressed("ui_cancel")) {
            GetTree().Quit();
        }
    }
    
    public void GameOver()
    {
        player1.Hide();
       // player2.Hide();

        GetNode<HUD>("HUD").ShowGameOver();
        GetNode<AudioStreamPlayer2D>("DeathSound").Play();
        GetNode<AudioStreamPlayer2D>("Music").Stop();
    }

    public void NewGame()
    {
        player1.Start();
       // player2.Start();

        GetNode<Timer>("StartTimer").Start();
        var hud = GetNode<HUD>("HUD");
        hud.ShowMessage("Get Ready!");
        GetNode<AudioStreamPlayer2D>("Music").Play();
    }
}
