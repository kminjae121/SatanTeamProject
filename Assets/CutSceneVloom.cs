using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using DG.Tweening;


public class CutSceneVloom : MonoBehaviour
{
    private Volume volume;

    [SerializeField]
    private Image blackPanel;

    Sequence MySequence;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject itemText;

    [SerializeField]
    private GameObject human;

    [SerializeField]
    private AudioSource audioSource;

    private void Awake()
    {
        volume = GetComponentInChildren<Volume>();
    }

    private void Start()
    {
        if(SaveManager.Instance.isAlreadyStart || SaveManager.Instance.isFirstSpawn || SaveManager.Instance.isSecondSpawn || SaveManager.Instance.isThirdSpawn)
        {
            human.SetActive(false);
            StartGame();
        }
        else
        {
            player.SetActive(false);
            MySequence = DOTween.Sequence()
                .OnStart(() => {

                })
            .Append(blackPanel.DOFade(0.5f, 1))
            .Append(blackPanel.DOFade(0f, 1))
            .OnComplete(() => {
                //종료시 실행
            });
        }
    }

    public void BlackEye()
    {
        MySequence = DOTween.Sequence()
           .OnStart(() => {

           })
       .Append(blackPanel.DOFade(0.5f, 1))
       .Append(blackPanel.DOFade(0f, 1))
       .OnComplete(() => {
            //종료시 실행
        });
    }

    public void NextChat()
    {
        print("아니 자막 나왔는데");
        ChatManager.Instance.Chat(2);
    }

    public void PlusVignette()
    {
        Vignette vignette;
        float startVignette = 0.239f;
        float endVignette = 0.463f;

        if (volume.profile.TryGet(out vignette))
        {
            DOTween.KillAll();
            DOTween.To(() => startVignette, vloom => vignette.intensity.value = vloom, endVignette, 2);
        }
    }

    public void SoundStop()
    {
        audioSource.Stop();
    }

    public void StartGame()
    {
        if(!SaveManager.Instance.isAlreadyStart)
            ChatManager.Instance.Chat(6);
        player.SetActive(true);
        itemText.SetActive(true);
        blackPanel.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    public void FadeBlackImage()
    {
        DOTween.KillAll();
        print("실행됨");
        blackPanel.DOFade(1, 1);
        human.SetActive(false);
    }

    public void FadeOutBlackImage()
    {
        DOTween.KillAll();
        print("실행됨");
        blackPanel.DOFade(0, 1);
    }

    public void MinusVignette()
    {
        Vignette vignette;
        float startVignette = 0.463f;
        float endVignette = 0.239f;

        if (volume.profile.TryGet(out vignette))
        {
            DOTween.KillAll();
            DOTween.To(() => startVignette, vloom => vignette.intensity.value = vloom, endVignette, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
