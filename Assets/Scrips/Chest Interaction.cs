using UnityEditor.Scripting.LifecycleManagement;
using UnityEngine;

public class ChestInteraction : MonoBehaviour, IInteractable
{
    Animator anim;
    static bool opened;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }
    public void Interact()
    {
        if (!opened) anim.SetTrigger("open");
        else anim.SetTrigger("close");
        opened = !opened;
    }
}
