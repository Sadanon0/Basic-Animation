using UnityEngine;
using UnityEngine.Playables;

public class CutsceneController : MonoBehaviour
{
    [SerializeField] private PlayableDirector diretor;
    [SerializeField] GameObject canvas;

    private void Awake()
    {
        if (canvas != null) canvas.SetActive(false);

    }


    private void OnEnable()
    {
        if (diretor != null)
        {
            diretor.stopped += OncutsceneFinished;
        }
    }
    private void OnDisable()
    {
        if (diretor != null)
        {
            diretor.stopped -= OncutsceneFinished;
        }
    }

    private void OncutsceneFinished(PlayableDirector pd)
    {
        if (canvas != null)
        {
            canvas.SetActive(true);
        }
    }

}
