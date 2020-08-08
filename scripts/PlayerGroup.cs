using Godot;
using System;

public class PlayerGroup : Node2D
{
    private Position2D startPos;
    private Player player;
    private TextureProgress healthbar;
    private static Vector2 healthbarOffset = new Vector2(-48, -50);

    [Export]
    public int playerNumber = 0;

    public override void _Ready() {
        startPos = GetNode<Position2D>("Start");
        player = GetNode<Player>("Body");
        healthbar = GetNode<TextureProgress>("HP");

        String prefix = "p" + playerNumber + "_";

        player.moveLeft = prefix + "left";
        player.moveRight = prefix + "right";
        player.moveUp = prefix + "up";
        player.moveDown = prefix + "down";
        player.shoot = prefix + "shoot";
        player.controller = playerNumber;
    }

    public void Start()
    {
        player.Position = startPos.Position;
        player.Health = player.MaxHealth;
        healthbar.Value = player.Health;
        Show();
    }

    public override void _Process(float delta) {
        healthbar.SetGlobalPosition(player.Position + healthbarOffset);
    }
}
