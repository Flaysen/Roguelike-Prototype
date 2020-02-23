using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : Item
{
    public int HealthBonus;
    public int EnergyBonus;
    public int ArmourBonus;
    public int PowerBonus;
    public int SpeedBonus;
    public int AttackRatioBonus;
    public int AttackSpeedBonus;
    public int AttackRangeBonus;
    public int LuckBonus;
    [Space]
    public float HealthPercentBonus;
    public float EnergyPercentBonus;
    public float ArmourPercentBonus;
    public float PowerPercentBonus;
    public float SpeedPercentBonus;
    public float AttackRatioPercentBonus;
    public float AttackSpeedPercentBonus;
    public float AttackRangePercentBonus;
    public float LuckPercentBonus;

    public override void OnPickup()
    {
        base.OnPickup();
        Equip();
        
    }

    public override void OnDrop()
    {
        base.OnDrop();
        Unequip();
    }

    public void Equip()
    {
        void AddBonusStats(int bonusFlat, float bonusPercent, CharacterStat statType)
        {
            if (bonusFlat != 0)
                statType.AddModifier(new StatModifier(bonusFlat, StatModType.Flat, this));
            if (bonusPercent != 0)
                statType.AddModifier(new StatModifier(bonusPercent, StatModType.PercentAdd, this));
        }

        AddBonusStats(HealthBonus, HealthPercentBonus, stats.MaxHealth);

        AddBonusStats(EnergyBonus, EnergyPercentBonus, stats.MaxEnergy);

        AddBonusStats(ArmourBonus, ArmourPercentBonus, stats.Armour);

        AddBonusStats(PowerBonus, PowerPercentBonus, stats.Power);

        AddBonusStats(SpeedBonus, SpeedPercentBonus, stats.Speed);

        AddBonusStats(AttackRatioBonus, AttackRatioPercentBonus, stats.AttackRatio);

        AddBonusStats(AttackSpeedBonus, AttackSpeedPercentBonus, stats.AttackSpeed);

        AddBonusStats(AttackRangeBonus, AttackRangePercentBonus, stats.AttackRange);

        AddBonusStats(LuckBonus,LuckPercentBonus, stats.Luck);



    }

    public void Unequip()
    {
        stats.MaxHealth.RemoveAllModifiersFromSource(this);
        stats.MaxEnergy.RemoveAllModifiersFromSource(this);
        stats.Armour.RemoveAllModifiersFromSource(this);
        stats.Power.RemoveAllModifiersFromSource(this);
        stats.Speed.RemoveAllModifiersFromSource(this);
        stats.AttackRatio.RemoveAllModifiersFromSource(this);
        stats.AttackSpeed.RemoveAllModifiersFromSource(this);
        stats.AttackRange.RemoveAllModifiersFromSource(this);
        stats.Luck.RemoveAllModifiersFromSource(this);
    }
}
