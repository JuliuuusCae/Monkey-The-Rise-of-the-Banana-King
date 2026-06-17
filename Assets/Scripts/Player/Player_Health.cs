using UnityEngine;

public class Player_Health : Entity_Health
{
    [SerializeField] private float deathScreenDelay = 1.5f;

    private Player player;

    protected override void Awake()
    {
        base.Awake();
        player = GetComponent<Player>();
    }

    protected override void Die()
    {
        base.Die();

        Invoke(nameof(OpenDeathScreen), deathScreenDelay);
        //GameManager.instance.SetLastPlayerPosition(transform.position);
        //GameManager.instance.RestartScene();
    }

    private void OpenDeathScreen()
    {
        player.ui.OpenDeathScreenUI();
    }
}