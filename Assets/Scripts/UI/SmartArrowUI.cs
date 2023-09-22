using System;
using MacounDemo.Logic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

namespace MacounDemo.UI
{
    public class SmartArrowUI : MonoBehaviour
    {
        [Required]
        [SerializeField]
        private TMP_Text textField;

        [Required]
        [SerializeField]
        private SmartArrow smartArrow;

        [SerializeField]
        private string audienceHitText;

        [SerializeField]
        private string playerHitText;

        private void Start()
        {
            textField.text = string.Empty;
        }

        private void OnEnable()
        {
            smartArrow.audienceHit.AddListener(OnAudienceHit);
            smartArrow.playerHit.AddListener(OnPlayerHit);
        }

        private void OnAudienceHit()
        {
            textField.text = audienceHitText;
        }

        private void OnPlayerHit()
        {
            textField.text = playerHitText;
        }

        private void OnDisable()
        {
            smartArrow.audienceHit.RemoveListener(OnAudienceHit);
            smartArrow.playerHit.RemoveListener(OnPlayerHit);
        }
    }
}