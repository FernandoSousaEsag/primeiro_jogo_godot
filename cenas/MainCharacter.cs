using Godot;
using System;

public partial class MainCharacter : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -900.0f;
	
	private AnimatedSprite2D sprite2d;
 
public override void _Ready()
	{
		sprite2d = GetNode<AnimatedSprite2D>("Sprite2D");
		GD.Print(sprite2d);
	}
 
	

	
	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		
		// Animations
		if (Math.Abs(velocity.X) > 1)
			sprite2d.Animation = "running";
		else
			sprite2d.Animation = "default";
			
		// Senão está no chão adicona gravidade - Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
			sprite2d.Animation ="jumping";
		}

		// Verifica se está no chão e foi pressionada a tecla Saltar -Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}
				
		// Verifica  se o utilizador/jogador está a pressionar as teclas  esquerda direita, acima, baixo  e atualiza a direção
		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
		
		 // muda o sprite baseado na direção
		bool isLeft = velocity.X < 0;
		sprite2d.FlipH = isLeft;
		
	}
}
