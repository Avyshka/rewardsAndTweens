using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rewards
{
    public class SlotRewardContainerView : MonoBehaviour
    {
        [SerializeField] private Image _selectBackground;
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _txtDays;
        [SerializeField] private TMP_Text _txtValue;

        public void SetData(Reward reward, int days, bool isSelect)
        {
            _icon.sprite = reward.Icon;
            _txtDays.text = $"Day {days}";
            _txtValue.text = reward.Value.ToString();
            _selectBackground.gameObject.SetActive(isSelect);
        }
    }
}