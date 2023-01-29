using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TextUpdater : MonoBehaviour
{
    public TextMeshProUGUI TextField;
    public float UpdateTime = 60;
    
    private string mTextToDisplay ="";
    private float mCurrentTime = 0;
    private bool mCoroutineRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ObtaingSheetData());

        mCurrentTime = UpdateTime;
    }

    // Update is called once per frame
    void Update()
    {
        mCurrentTime -= Time.deltaTime;

        if ((mCurrentTime <= 0) && (mCoroutineRunning == false))
        {
            StartCoroutine(ObtaingSheetData());

            mCurrentTime = UpdateTime;
        }
    }

    IEnumerator ObtaingSheetData()
    {
        mCoroutineRunning = true;
        UnityWebRequest www = UnityWebRequest.Get("https://opensheet.elk.sh/1tFPaiivN1AN06no99r762WNpqEYPu2sP1EB9xzYD23I/Sheet1");
        yield return www.SendWebRequest();
        
        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.Log("Error with json: " + www.error);
        }
        else
        {
            mTextToDisplay = string.Empty;

            string json = www.downloadHandler.text;
            var jsonArray = JSON.Parse(json);
            Debug.Log(jsonArray); 

            foreach (var entry in jsonArray)
            {
                var entryo = JSON.Parse(entry.ToString());

                Debug.Log(entryo);

                string entryName = entryo[0]["Name"];
                string entryDamage = entryo[0]["Damage"]; 

                mTextToDisplay += "Name: " + entryName + ", Damage: " + entryDamage + "\n"; 
            }
        }

        TextField.text = mTextToDisplay;

        mCoroutineRunning = false;
    }
}
