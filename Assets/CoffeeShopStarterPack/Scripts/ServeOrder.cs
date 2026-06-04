using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

namespace PW
{
    public class ServeOrder : MonoBehaviour
    {
        [HideInInspector]
        public int orderID = -1;
        private Image myImage;

        // ⭐ [진짜 최종] 두 번째 인자를 float 타입으로 안전하게 받도록 수정했습니다!
        public void SetOrder(int id, float duration = 0f)
        {
            orderID = id;
        }

        public void SetSprite(Sprite sprite)
        {
            if (myImage == null) myImage = GetComponent<Image>();
            if (myImage != null) myImage.sprite = sprite;
        }

        // 외곽선 에러를 완벽 방지한 강조 효과 코루틴
        public IEnumerator DoEmphasize()
        {
            float totalTime = .6f;
            float curTime = totalTime;
            while (curTime > 0)
            {
                curTime -= Time.deltaTime;
                transform.localScale += Vector3.one * 0.1f * -1f * Mathf.Sin(totalTime - 2 * curTime);
                yield return null;
            }
            transform.localScale = Vector3.one;
        }
    }
}