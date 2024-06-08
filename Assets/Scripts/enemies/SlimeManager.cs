using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeManager : MonoBehaviour
{
    public static SlimeManager Instance;

    private HashSet<SlimeController> chasingSlimes = new HashSet<SlimeController>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterChasingSlime(SlimeController slime)
    {
        chasingSlimes.Add(slime);
        CheckAndPlayMusic();
    }

    public void UnregisterChasingSlime(SlimeController slime)
    {
        chasingSlimes.Remove(slime);
        CheckAndStopMusic();
    }

    private void CheckAndPlayMusic()
    {
        if (chasingSlimes.Count > 0 && !AudioManager.Instance.IsMusicPlaying("battle"))
        {
            AudioManager.Instance.PlayMusic("battle");
        }
    }

    private void CheckAndStopMusic()
    {
        if (chasingSlimes.Count == 0 && AudioManager.Instance.IsMusicPlaying("battle"))
        {
            StartCoroutine(WaitAndFadeOutMusic(5f)); // Menunggu 5 detik sebelum mengecek dan fade out
        }
    }

    private IEnumerator WaitAndFadeOutMusic(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        if (chasingSlimes.Count == 0 && AudioManager.Instance.IsMusicPlaying("battle"))
        {
            AudioManager.Instance.FadeOutMusic();
        }
    }
}
