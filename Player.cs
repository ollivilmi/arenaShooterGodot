using Godot;
using System;

public class Player : KinematicBody2D
{
    [Export]
    public int Speed = 200; // pixels per sec
    Vector2 velocity = new Vector2();
    public Position2D StartPosition;
    private ColorRect crosshair;
    private float deadzone = 0.5f;

    public String moveLeft;
    public String moveRight;
    public String moveUp;
    public String moveDown;
    public String shoot;
    public int controller;

    private RayCast2D aim;

    [Signal]
    public delegate void Hit();

    public override void _Ready()
    {
        Hide();
        aim = GetNode<RayCast2D>("RayCast2D");
        crosshair = GetNode<ColorRect>("Crosshair");
    }

    public void Start()
    {
        Position = StartPosition.Position;
        Show();
    }

    private void handleInput(float delta)
        {
        velocity = new Vector2(); // The player's movement vector.
        velocity.x = -Input.GetActionStrength(moveLeft) + Input.GetActionStrength(moveRight);
        velocity.y = +Input.GetActionStrength(moveDown) - Input.GetActionStrength(moveUp);
 
        var direction = new Vector2(Input.GetJoyAxis(controller, 2), Input.GetJoyAxis(controller, 3));

        if (Math.Abs(direction.x) > deadzone || Math.Abs(direction.y) > deadzone) {
            Rotation = direction.Angle();
        }

        velocity = MoveAndSlide(velocity.Normalized() * Speed);
        velocity = velocity * delta;

        if (Input.IsActionJustPressed(shoot)) {
            var col = aim.GetCollider();
            GetNode<AudioStreamPlayer2D>("Shoot").Play();

            if (aim.IsColliding()) {
                if (col.GetClass().Equals("KinematicBody2D")) {
                    GetNode<AudioStreamPlayer2D>("Hit").Play();
                    col.EmitSignal("Hit");
                }
            }
        }
    }
 
    public override void _PhysicsProcess(float delta)
    {
        handleInput(delta);
    }
}
