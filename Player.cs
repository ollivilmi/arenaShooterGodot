using Godot;
using System;

public class Player : KinematicBody2D
{
    [Export]
    public int Speed = 200; // pixels per sec
    Vector2 velocity = new Vector2();
    public Position2D StartPosition;

    private float deadzone = 0.5f;

    [Signal]
    public delegate void Hit();

    public override void _Ready()
    {
        Hide();
    }

    public void Start()
    {
        Position = StartPosition.Position;
        Show();
    }

    private void handleInput(float delta)
        {
        velocity = new Vector2(); // The player's movement vector.
        velocity.x = -Input.GetActionStrength("p1_left") + Input.GetActionStrength("p1_right");
        velocity.y = +Input.GetActionStrength("p1_down") - Input.GetActionStrength("p1_up");
 
        var direction = new Vector2(Input.GetJoyAxis(0, 2), Input.GetJoyAxis(0, 3));

        if (Math.Abs(direction.x) > deadzone || Math.Abs(direction.y) > deadzone) {
            Rotation = direction.Angle();
        }

        velocity = MoveAndSlide(velocity.Normalized() * Speed);
        velocity = velocity * delta;
    }
 
    public override void _PhysicsProcess(float delta)
    {
        handleInput(delta);
    }
}
