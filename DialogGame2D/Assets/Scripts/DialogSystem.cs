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

    bool textFinished;

    List<string> textList = new List<string>();

    // Start is called before the first frame update
    void Awake()
    {
        GetTextFormFile(textFile);
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
        if (Input.GetKeyDown(KeyCode.R) && textFinished)
        {
            //textLabel.text = textList[index];
            //index++;
            StartCoroutine(SetTextUI());
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
        if(textFinished)
            StartCoroutine(SetTextUI());
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
        textFinished = false;
        textLabel.text = "";
        print(textList[index]);
        if (textList[index].Contains("protagonist")){
            faceImage.sprite = face01;
            index++;
        }
        else if (textList[index].Contains("protagonist")){
            faceImage.sprite = face01;
            index++;
        }
        switch (textList[index])
        {
            case "protagonist":
                print("一樣");
                faceImage.sprite = face01;
                index++;
                break;
            case "talkrole":
                faceImage.sprite = face01;
                index++;
                break;
            case "Narrator":
                index++;
                break;
        }

        for(int i = 0; i < textList[index].Length; i++)
        {
            textLabel.text += textList[index][i];
            yield return new WaitForSeconds(textSpeed);
        }
        textFinished = true;
        index++;
    }
}
