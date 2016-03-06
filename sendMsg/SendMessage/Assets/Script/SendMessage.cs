using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class SendMessage : MonoBehaviour
{
    public InputField number;
    public InputField content;
    public Button btn_send;
    public Text statue;

    private AndroidJavaClass ajc;

    // Use this for initialization
    void Start()
    {
        ajc = new AndroidJavaClass("com.qyxls.sms.SMSActivity");
        btn_send.onClick.AddListener(delegate()
        {
            this.SendMsg(btn_send.gameObject);
        });
    }

    void SendMsg(GameObject go)
    {
        if (string.IsNullOrEmpty(number.text) || string.IsNullOrEmpty(content.text))
        {
            statue.text = "信息发送失败!";
            return;
        }
        ajc.CallStatic("SMSSend", new string[] { number.text, content.text });
        statue.text = "信息已发送!";
        StartCoroutine(ChangeStatue());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Home))
            Application.Quit();
    }

    private IEnumerator ChangeStatue()
    {
        yield return new WaitForSeconds(1f);
        statue.text = "";
        number.text = "";
        content.text = "";
    }

}
