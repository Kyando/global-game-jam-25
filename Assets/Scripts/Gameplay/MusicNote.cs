using System;
using UnityEngine;

public class MusicNote : MonoBehaviour
{
    public float beatTime = 0f;
    public int noteId = 0;
    public bool isNotePlayed = false;
    private Animator animator;

    private void Awake()
    {
        this.animator = this.GetComponent<Animator>();
    }

    public void OnNotePlayed()
    {
        isNotePlayed = true;
        if (animator is not null)
        {
            animator.CrossFade("FadeOut",0,0);
        }
    }
}