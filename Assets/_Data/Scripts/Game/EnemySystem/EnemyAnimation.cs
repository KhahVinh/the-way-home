using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void SetMoving(bool isMoving)
    {
        anim.SetBool("IsMoving", isMoving);
    }

    public void SetDirection(Vector2 dir)
    {
        anim.SetFloat("MoveX", dir.x);
        anim.SetFloat("MoveY", dir.y);
    }

    public void TriggerAttack()
    {
        anim.SetTrigger("Attack");
    }

    public void PlayDeath()
    {
        anim.SetBool("IsDead", true);
    }
    public void PlayHurt()
    {
        anim.SetTrigger("Hurt");
    }
}
