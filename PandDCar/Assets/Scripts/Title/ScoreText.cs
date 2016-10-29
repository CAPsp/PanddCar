using UnityEngine;using System.Collections;using UnityEngine.UI;using Original;public class ScoreText : MonoBehaviour {    public const int LIMIT_RANK = 3;   // 何位まで保存するか(表示がずれるので安易に変えないこと)    void Awake() {

        // スコアを取り出して、フレームに表示するテキストに整形
        ArrayList array = ScoreManagement.Read();        string text     = "";        for(int i = 1; i<= ScoreManagement.LIMIT_RANK; i++) {            text += "\n";            text += "\t\t" + i.ToString() + "\t:\t" + ((int)array[i-1]).ToString() + "\n";        }        GetComponent<Text>().text = text;    }}