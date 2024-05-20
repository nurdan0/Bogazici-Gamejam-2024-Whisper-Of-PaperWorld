using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class ShortVideoController : MonoBehaviour
{
    public GameObject video;
    public PostProcessVolume postProcessVolume;
    private Vignette vignette;

    IEnumerator Start()
    {
        postProcessVolume.profile.TryGetSettings(out vignette);
        yield return new WaitForSeconds(21);
        video.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            postProcessVolume.enabled = true;
            StartCoroutine(IncreaseVignetteIntensity());
        }
    }

    private IEnumerator IncreaseVignetteIntensity()
    {
        int CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int NextSceneIndex = CurrentSceneIndex + 1;
        float targetIntensity = 1f;
        float duration = 1f;
        float startIntensity = vignette.intensity.value;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            vignette.intensity.value = Mathf.Lerp(startIntensity, targetIntensity, elapsed / duration);
            yield return null;
        }

        vignette.intensity.value = targetIntensity;
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(NextSceneIndex);
    }
}
