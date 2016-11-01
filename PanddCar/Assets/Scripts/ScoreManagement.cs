using UnityEngine;
using System.Collections;
using Original.Debug;

namespace Original {

    // PlayerPrefsに保存したデータは容易に書き換え可能だが、
    // 書き換えたところで優越感に浸れるぐらいなので使用することに
    // ( ローカルファイルへのセーブも考えたがめんど(ry )
    public class ScoreManagement{

        public const int LIMIT_RANK = 3;   // 何位まで保存するか(表示がずれるので安易に変えないこと)

        // ランク順でスコアが入ったArrayListを返す
        public static ArrayList Read() {

            // キーがないなら新しく保存
            int i = 1;
            if (PlayerPrefs.HasKey(i.ToString())) {

                for (i = 1; i <= LIMIT_RANK; i++) {
                    PlayerPrefs.SetInt(i.ToString(), 0);
                }

                PlayerPrefs.Save();     // この処理は重いが、初回起動限定なので
            }

            ArrayList array = new ArrayList();
            for (i = 1; i <= LIMIT_RANK; i++) {

                array.Add( PlayerPrefs.GetInt(i.ToString()) );
            }

            return array;
        }

        // 引数に応じてランクにスコアを書き込み
        public static void Write(int rank, int score) {

            DebugCommon.Assert( 1 <= rank && rank <+ LIMIT_RANK );

            // 新しいランクのために既存のランクをずらす処理
            for(int i = LIMIT_RANK - 1; i >= rank; i--) {
                PlayerPrefs.SetInt( (i + 1).ToString(), PlayerPrefs.GetInt(i.ToString()) );
            }

            PlayerPrefs.SetInt( rank.ToString(), score );

        }

    }

}