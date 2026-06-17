using TMPro;
using UnityEngine;

public class UI_StatToolTip : UI_ToolTip
{
    private Player_Stats playerStats;
    private TextMeshProUGUI statToolTipText;

    protected override void Awake()
    {
        base.Awake();
        playerStats = FindFirstObjectByType<Player_Stats>();
        statToolTipText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void ShowToolTip(bool show, RectTransform targetRect,StatType statType)
    {
        base.ShowToolTip(show, targetRect);
        statToolTipText.text = GetStatTextByType(statType);
    }

    public string GetStatTextByType(StatType type)
    {
        switch (type)
        {
            // Atributos Principais
            case StatType.Strength:
                return "Aumenta o dano físico em 1 por ponto." +
                    "\n Aumenta o poder crítico em 0.5% por ponto.";
            case StatType.Agility:
                return "Aumenta a chance de crítico em 0.3% por ponto." +
                    "\n Aumenta a evasão em 0.5% por ponto.";
            case StatType.Intelligence:
                return "Aumenta as resistências elementais em 0.5% por ponto.";
            case StatType.Vitality:
                return "Aumenta a vida máxima em 5 por ponto." +
                    "\n Aumenta a armadura em 1 por ponto.";

            // Dano Físico
            case StatType.Damage:
                return "Determina o dano físico dos seus ataques.";
            case StatType.CritChance:
                return "Chance dos seus ataques causarem dano crítico.";
            case StatType.CritPower:
                return "Aumenta o dano causado por ataques críticos.";
            case StatType.ArmorReduction:
                return "Porcentagem da armadura que será ignorada pelos seus ataques.";
            case StatType.AttackSpeed:
                return "Determina quão rápido você pode atacar.";

            // Defesa
            case StatType.MaxHealth:
                return "Determina quanta vida total você possui.";
            case StatType.HealthRegen:
                return "Quantidade de vida restaurada por segundo.";
            case StatType.Armor:
                return "Reduz o dano físico recebido."
                    + "\n A mitigação de armadura é limitada a 85%.";
            case StatType.Evasion:
                return "Chance de evitar completamente ataques."
                    + "\n Limitado a 85%.";

            default:
                return "Nenhuma descrição disponível para este atributo.";
        }
    }
}
