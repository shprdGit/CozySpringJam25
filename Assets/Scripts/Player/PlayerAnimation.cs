using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip[] wingSwooshClips;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void IdleControl(bool value)
    {
        animator.SetBool("isIdle", value);
    }

    public void GroundedControl(bool value)
    {
        animator.SetBool("isGrounded", value);
    }

    public void AccelerationControl(bool value)
    {
        animator.SetBool("isAccelerating", value);
    }

    public void ChargeControl(bool value)
    {
        animator.SetBool("isCharging", value);
    }

    // === Animation Event Method ===
    public void PlayWingSwoosh()
    {
        if (audioSource != null && wingSwooshClips != null && wingSwooshClips.Length > 0)
        {
            int index = Random.Range(0, wingSwooshClips.Length);
            audioSource.PlayOneShot(wingSwooshClips[index]);
        }
    }
}