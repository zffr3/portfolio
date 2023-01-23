using UnityEngine;

public class WalkinZombieAnimation : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;

    private void Start()
    {
        this.GetComponent<Animator>();
    }

    public void SetSpeed(float speed)
    {
        if (speed != 0)
        {
            this._animator.SetBool("Walk", true);
            this._animator.SetFloat("Blend", speed);
            return;
        }
        this._animator.SetBool("Walk", false);

    }

    public void Attack()
    {
        this._animator.SetTrigger("Attack");
    }

    public void SetZombieAnimationState(ZombieState nState, bool state)
    {
        this._animator.SetBool(nState.ToString(), state);
    }
}
