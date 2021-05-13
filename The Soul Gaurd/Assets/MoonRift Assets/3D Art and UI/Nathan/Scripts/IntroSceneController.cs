using UnityEngine.UI;
using UnityEngine;

public class IntroSceneController : MonoBehaviour
{   
    
    [SerializeField]
    GameObject page_1, page_2, page_3;

    [SerializeField]
    AudioClip backgroundAudio, clip_1, clip_2, clip_3;

    [SerializeField]
    AudioSource backgroundSrc, narrationSrc; //backgroundSrc is the first AudioSource on Intro Canvas, while narrationSrc is the second

    [SerializeField]
    float fadeSpeed, startTime;

    [SerializeField]
    GameObject parchmentObj, coverObj;

    RawImage parchment, coverImg;

    Material textMat1, imgMat1, textMat2, imgMat2, textMat3, imgMat3;

    bool cover, page1, page2, page3;

    float fade = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        backgroundSrc.clip = backgroundAudio;
        backgroundSrc.Play();

        narrationSrc.clip = clip_1;

        textMat1 = page_1.transform.GetChild(0).GetComponent<Text>().material;
        imgMat1 = page_1.transform.GetChild(1).GetComponent<Image>().material;

        textMat2 = page_2.transform.GetChild(0).GetComponent<Text>().material;
        imgMat2 = page_2.transform.GetChild(1).GetComponent<Image>().material;

        textMat3 = page_3.transform.GetChild(0).GetComponent<Text>().material;
        imgMat3 = page_3.transform.GetChild(1).GetComponent<Image>().material;

        parchment = parchmentObj.GetComponent<RawImage>();
        coverImg = coverObj.GetComponent<RawImage>();

        cover = true;
    }

    // Update is called once per frame
    void Update()
    {   
        if(cover && Time.time >= startTime){

            Color tmp = coverImg.color;

            if(tmp.a <= 0){
                cover = false;
                page1 = true;
            }

            tmp.a -= fadeSpeed * Time.deltaTime;
            coverImg.color = tmp;

            tmp.a = 1 - tmp.a;
            parchment.color = tmp;
        }

        else if(page1){
            fade += fadeSpeed * Time.deltaTime;

            textMat1.SetFloat("_Fade", fade);
            imgMat1.SetFloat("_Fade", fade);

            if(fade >= 1f){
                fade = 0f;
                page1 = false;
                page2 = true;

                narrationSrc.Play();
            }
        }

        else if(page2){
            if(!narrationSrc.isPlaying){
                fade += fadeSpeed * Time.deltaTime;

                textMat1.SetFloat("_Fade", 1 - fade);
                imgMat1.SetFloat("_Fade", 1 - fade);

                textMat2.SetFloat("_Fade", fade);
                imgMat2.SetFloat("_Fade", fade);

                if(fade >= 1f){
                    fade = 0f;
                    page2 = false;
                    page3 = true;

                    narrationSrc.clip = clip_2;
                    narrationSrc.Play();
                }
            }
        }

        else if(page3){
            if(!narrationSrc.isPlaying){
                fade += fadeSpeed * Time.deltaTime;

                textMat2.SetFloat("_Fade", 1 - fade);
                imgMat2.SetFloat("_Fade", 1 - fade);

                textMat3.SetFloat("_Fade", fade);
                imgMat3.SetFloat("_Fade", fade);

                if(fade >= 1f){
                    fade = 1f;
                    page3 = false;

                    narrationSrc.clip = clip_3;
                    narrationSrc.Play();
                }
            }
        }

        else{
            if(!narrationSrc.isPlaying){
                fade -= fadeSpeed * Time.deltaTime;

                textMat3.SetFloat("_Fade", fade);
                imgMat3.SetFloat("_Fade", fade);

                if(fade <= 0f){
                    fade = 0f;
                    page3 = false;

                    Color tmp = parchment.color;

                    if(tmp.a <= 0){
                        return;
                    }

                    tmp.a -= fadeSpeed * Time.deltaTime;
                    parchment.color = tmp;

                    backgroundSrc.volume -= fadeSpeed * Time.deltaTime;
                }
            }
        }
    }
}
