using UnityEngine;

namespace Original{

    // 階層構造に関する処理を助けるユーティリティクラス
    public static class HierarchyUtil {

        // そのGameObjectの大元の親を返す
        public static GameObject SearchMasterParent(GameObject obj) {

            GameObject parent = GetParent(obj);

            if(parent == null) {
                return obj;
            }
            else {
                return SearchMasterParent(parent);
            }

        }

        // 親を返す
        public static GameObject GetParent(GameObject obj) {

            Transform parent = obj.transform.parent;

            return (parent != null) ? parent.gameObject : null;

        }

    }

}