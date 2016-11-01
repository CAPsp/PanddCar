
namespace Original.Debug {

    // 例外処理を助けるクラス
    public static class DebugCommon {
        
        // 条件がfalseなら例外を投げる
        public static void Assert(bool condition, string message = "") {
            if (!condition) {

                if (message == "") {
                    throw new System.Exception("The condition is false.");
                }
                else {
                    throw new System.Exception(message);
                }
            }
        }

        // 渡された配列の要素にnullがあったら例外を投げる
        public static void CheckArraysNull(object[] arrays) {
            for(int i = 0; i < arrays.Length; i++) {
                if(arrays[i] == null) {
                    throw new System.Exception("Arrays contain null.");
                }
            }
        }

    }

}