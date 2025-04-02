using UnityEngine;
using UnityEngine.Rendering;

public class AnimatorOverrider : MonoBehaviour
{
    protected Animator animator;
    protected AnimatorOverrideController overrideController;
    public AnimationClip weapon1, weapon2, weaponAir, ures;

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
        animator.SetBool("canAb1", true);
    }
    public void EquipAbility2Anim(ItemType itemType, AnimationClip ab)
    {
        overrideController["Ab2"] = ab;
        animator.SetBool("canAb2", true);
    }
    public void EquipAbility3Anim(ItemType itemType, AnimationClip ab)
    {
        overrideController["Ab3"] = ab;
        animator.SetBool("canAb3", true);
    }

    public void UnEquipAbility1Anim()
    {
        overrideController["Ab1"] = ures;
        animator.SetBool("canAb1", false);
    }
    public void UnEquipAbility2Anim()
    {
        overrideController["Ab2"] = ures;
        animator.SetBool("canAb2", false);
    }
    public void UnEquipAbility3Anim()
    {
        overrideController["Ab3"] = ures;
        animator.SetBool("canAb3", false);
    }

}
