using Godot;
using System;

public class Player : KinematicBody2D
{
    [Export]
    public int Speed = 200; // pixels per sec
    [Export]
    public int MaxHealth = 10;
    public int Health;

    Vector2 velocity = new Vector2();
    public Position2D StartPosition;
    private ColorRect laser;
    private float deadzone = 0.5f;

    public String moveLeft;
    public String moveRight;
    public String moveUp;
    public String moveDown;
    public String shoot;
    public int controller;
    private RayCast2D aim;
    private Timer laserTimer;

    [Signal]
    public delegate void Dead();

    [Signal]
    public delegate void HealthChanged();

    public override void _Ready()
    {
        aim = GetNode<RayCast2D>("RayCast2D");
        laser = GetNode<ColorRect>("Laser");
        laser.Hide();
        laserTimer = GetNode<Timer>("LaserTimer");
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
            laser.Show();
            laserTimer.Start();

            GetNode<AudioStreamPlayer2D>("Shoot").Play();

            if (aim.IsColliding()) {
                if (col.GetClass().Equals("KinematicBody2D")) {
                    GetNode<AudioStreamPlayer2D>("Hit").Play();
                    col.HasMethod("Hit");
                    col.Call("Hit");
                }
            }
        }
    }

    public void HideLaser() {
        laser.Hide();
    }

    public void Hit() {
        if (--Health == 0) {
            EmitSignal("Dead");
        }
        else {
            EmitSignal("HealthChanged");
        }
    }
 
    public override void _PhysicsProcess(float delta)
    {
        handleInput(delta);
    }
}
