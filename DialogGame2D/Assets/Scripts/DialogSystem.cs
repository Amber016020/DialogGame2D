using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    [Header("UI組件")]
    public Text textLabel;
    public Image faceImage;

    [Header("文本組件")]
    public TextAsset textFile;
    public int index;
    public float textSpeed;

    [Header("頭像")]
    public Sprite face01, face02;

    [Header("話題選項")]
    public Text TextChoice;
    public Text T1;
    public Text T2;
    public Text T3;
    public Text T4;

    [Header("對話框")]
    public GameObject dialog;

    [Header("話題")]
    public GameObject dialogChoice;

    bool textFinished;
    bool cancelTyping;
    string chh = "";

    List<string> textList = new List<string>();

    void Awake()
    {
        GetTextFormFile(textFile);
    }

    public void Choice(string ch)
    {
        chh = ch;
    }

    private void OnEnable()
    {
        //textLabel.text = textList[index];
        //index++;
        textFinished = true;
        StartCoroutine(SetTextUI());
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)&& index == textList.Count)
        {
            gameObject.SetActive(false);
            index = 0;
            return;
        }
        //if (Input.GetKeyDown(KeyCode.R) && textFinished)
        //{
        //    //textLabel.text = textList[index];
        //    //index++;
        //    StartCoroutine(SetTextUI());
        //}
        if (Input.GetKeyDown(KeyCode.R))
        {
            if(textFinished && !cancelTyping)
            {
                StartCoroutine(SetTextUI());
            }
            else if (!textFinished)
            {
                cancelTyping = !cancelTyping;
            }
        }
    }

    public void NetLine()
    {
        //if (index == textList.Count)
        //{
        //    gameObject.SetActive(false);
        //    index = 0;
        //    return;
        //}
        //textLabel.text = textList[index];
        //index++;

        if (textFinished && !cancelTyping)
        {
            StartCoroutine(SetTextUI());
        }
        else if (!textFinished)
        {
            cancelTyping = !cancelTyping;
        }
        //if (textFinished)
        //    StartCoroutine(SetTextUI());
    }

    void GetTextFormFile(TextAsset file)
    {
        textList.Clear();
        index = 0;

        var lineDate = file.text.Split('\n');

        foreach(var line in lineDate)
        {
            textList.Add(line);
        }
    }

    IEnumerator SetTextUI()
    {
        //選項的邏輯
        textFinished = false;
        textLabel.text = "";
        print(textList[index]);
        if(chh != null)
        {
            if (textList[index] != chh)
            {
                index++;
            }
        }
        if (textList[index].Contains("protagonist"))
        {
            faceImage.color = new Color(255, 255, 255, 255);
            faceImage.sprite = face01;
            index++;
        }
        else if (textList[index].Contains("talkrole")){
            faceImage.color = new Color(255, 255, 255, 255);
            faceImage.sprite = face02;
            index++;
        }
        else if (textList[index].Contains("Narrator"))
        {
            faceImage.color = new Color(255, 255, 255, 0);
            index++;
        }
        else if (textList[index].Contains("Choice"))
        {
            dialogChoice.SetActive(true);
            dialog.SetActive(false);
            index++;
            TextChoice.text = textList[index];
        }
        else if (textList[index].Contains("T1"))
        {
            index++;
            TextChoice.text = textList[index];
        }
        else if (textList[index].Contains("T2"))
        {
            index++;
            TextChoice.text = textList[index];
        }


        int letter = 0;
        while (!cancelTyping && letter < textList[index].Length -1 )
        {
            textLabel.text += textList[index][letter];
            letter++;
            yield return new WaitForSeconds(textSpeed);
        }

        //for(int i = 0; i < textList[index].Length; i++)
        //{
        //    textLabel.text += textList[index][i];
        //    yield return new WaitForSeconds(textSpeed);
        //}
        textLabel.text = textList[index];
        cancelTyping = false;
        textFinished = true;
        index++;
    }
}
