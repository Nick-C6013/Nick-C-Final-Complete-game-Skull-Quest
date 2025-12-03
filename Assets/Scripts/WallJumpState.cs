using UnityEngine;

public class WallJumpState:PlayerState
{
    public override void EnterState(PlayerController player)
    {
        TryPlayAnimation(player, "Jump");

        Vector2 velocity = player.rb.linearVelocity;
        velocity.y = player.jumpForce;
        velocity.x = -player.transform.localScale.x * player.jumpForce;
        player.rb.linearVelocity = velocity;

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayJumpSound();
        }
    }

    public override void UpdateState(PlayerController player)
    {
        float horizontal = Input.GetAxis("Horizontal");
        Vector2 velocity = player.rb.linearVelocity;
        velocity.x = horizontal * player.moveSpeed;
        player.rb.linearVelocity = velocity;
        player.ChangeState(new JumpingState());

        if (horizontal < 0)
            player.spriteRenderer.flipX = true;
        else if (horizontal > 0)
            player.spriteRenderer.flipX = false;

        if (player.IsGrounded() && player.rb.linearVelocity.y <= 0)
        {
            if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f)
            {
                player.ChangeState(new MovingState());
            }
            else
            {
                player.ChangeState(new IdleState());
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            player.Fire();
        }
    }

    public override void ExitState(PlayerController player) { }

    public override string GetStateName() => "Wall Jumping";

    private void TryPlayAnimation(PlayerController player, string animName)
    {
        if (player.animator != null &&
            player.animator.runtimeAnimatorController != null &&
            player.animator.isActiveAndEnabled)
        {
            try
            {
                player.animator.Play(animName);
            }
            catch
            {
                // Animation doesn't exist - continue without it
            }
        }
    }
}
