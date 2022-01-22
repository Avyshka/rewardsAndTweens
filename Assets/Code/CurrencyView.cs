using TMPro;
using UnityEngine;

namespace Rewards
{
    public class CurrencyView : MonoBehaviour
    {
        public static CurrencyView Instance;

        [SerializeField] private TMP_Text _txtFood;
        [SerializeField] private TMP_Text _txtWood;
        [SerializeField] private TMP_Text _txtGold;
        [SerializeField] private TMP_Text _txtDiamonds;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            RefreshText();
        }

        private void RefreshText()
        {
            _txtFood.text = GetCurrency(RewardType.Food).ToString();
            _txtWood.text = GetCurrency(RewardType.Wood).ToString();
            _txtGold.text = GetCurrency(RewardType.Gold).ToString();
            _txtDiamonds.text = GetCurrency(RewardType.Diamonds).ToString();
        }
        
        private int GetCurrency(RewardType rewardType)
        {
            return PlayerPrefs.GetInt(rewardType.ToString(), 0);
        }

        public void SetCurrency(RewardType rewardType, int value)
        {
            PlayerPrefs.SetInt(rewardType.ToString(), GetCurrency(rewardType) + value);
            RefreshText();
        }
    }
}