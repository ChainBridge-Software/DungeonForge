using UnityEngine;

public class AnimatorOverrider : MonoBehaviour
{
    protected Animator animator;
    protected AnimatorOverrideController overrideController;
    public AnimationClip weapon1, weapon2, weaponAir;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GameObject.Find("Player").GetComponent<Animator>();

        //new anim overrider

        overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);

        //
        animator.runtimeAnimatorController = overrideController;
    }

    public void UnEquipWeaponAnim(ItemType itemType)
    {
        if (itemType == ItemType.weapon)
        {
            overrideController["MC_BareHand1"] = weapon1;
            overrideController["MC_BareHand2"] = weapon2;
            overrideController["MC_BareHand_Air"] = weaponAir;
        }
    }
    public void EquipWeaponAnim(ItemType itemType, AnimationClip w1, AnimationClip w2, AnimationClip wAir)
    {
        if (itemType == ItemType.weapon)
        {
            overrideController["MC_BareHand1"] = w1;
            overrideController["MC_BareHand2"] = w2;
            overrideController["MC_BareHand_Air"] = wAir;
        }
    }
    public void EquipAbility1Anim(ItemType itemType, AnimationClip ab)
    {
        overrideController["Ab1"] = ab;
    }
    public void EquipAbility2Anim(ItemType itemType, AnimationClip ab)
    {
        overrideController["Ab2"] = ab;
    }
    public void EquipAbility3Anim(ItemType itemType, AnimationClip ab)
    {
        overrideController["Ab3"] = ab;
    }
}
