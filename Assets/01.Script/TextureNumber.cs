using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextureNumber : MonoBehaviour
{
    [SerializeField]
    bool LerpMode = true;

    [SerializeField]
    GameObject TexturePrefab;

    [SerializeField]
    Sprite[] NumberTexture;

    [SerializeField]
    int score;

    //[SerializeField]
    List<GameObject> images = new List<GameObject>();

    [SerializeField]
    float LerpSpeed = 1f;

    public void SettingValue(int data) //값 받기
    {
        ResetTexturePrefab();

        for (int i = 0; i < data.ToString().Length; i++)
        {
            images.Add(Instantiate(TexturePrefab, transform));
        }
        images.Reverse();
        StartCoroutine(PlayNumberAnimation(data));
    }

    void ResetTexturePrefab() // Images 의 리스트 제거, 초기화
    {
        if (images != null)
        {
            for (int i = 0; i < images.Count; i++)
            {
                Destroy(images[i]);
            }
            images.Clear();
        }
    }

    IEnumerator PlayNumberAnimation(int value) // 목표치까지 Lerp 하여 전달
    {
        float timer = 0f;
        if (LerpMode == true)
        {
            while (timer < 1f)
            {
                timer += Time.deltaTime * (LerpSpeed);
                int lerpNum = (int)Mathf.Lerp(0, value, timer);
                if (timer >= 1f) // 오차가 나는 오류를 위한 코드
                {
                    lerpNum = value;
                }
                GetNum(lerpNum);
                yield return null;
            }
        }
        else
        {
            GetNum(value);
        }
    }

    void GetNum(int num)
    {
        for (int i =0; i < num.ToString().Length; i ++)
        {
            images[num.ToString().Length - (i + 1)].GetComponent<Image>().sprite = NumberTexture[int.Parse(num.ToString().Substring(i , 1))];
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SettingValue(score);
        }
    }
}
