using UnityEngine;

public class Entity_SFX : MonoBehaviour
{
    private AudioSource audioSource;

    [Header("SFX Names")]
    [SerializeField] private string attackHit;
    [SerializeField] private string attackMiss;
    [SerializeField] private string stunned;
    [SerializeField] private string dash;
    [SerializeField] private string footstep;
    [SerializeField] private string death;
    [SerializeField] private string jump;
    [SerializeField] private string enemyDeath;
    [Space]
    [SerializeField] private float soundDistance = 15f;
    [SerializeField] private bool showGizmo;


    private void Awake()
    {
        audioSource = GetComponentInChildren<AudioSource>();
    }

    public void PlayAttackHit()  => AudioManager.instance.PlaySFX(attackHit,  audioSource, soundDistance);
    public void PlayAttackMiss() => AudioManager.instance.PlaySFX(attackMiss, audioSource, soundDistance);
    public void PlayStunned()    => AudioManager.instance.PlaySFX(stunned,    audioSource, soundDistance);
    public void PlayDash()       => AudioManager.instance.PlaySFX(dash,       audioSource, soundDistance);
    public void PlayFootstep()   => AudioManager.instance.PlaySFX(footstep,   audioSource, soundDistance);
    public void PlayDeath()      => AudioManager.instance.PlayGlobalSFX(death);
    public void PlayJump()       => AudioManager.instance.PlayGlobalSFX(jump);
    public void PlayEnemyDeath() => AudioManager.instance.PlaySFX(enemyDeath, audioSource, soundDistance);

    private void OnDrawGizmos()
    {
        if (showGizmo)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, soundDistance);
        }
    }
}