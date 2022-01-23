using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rewards
{
    public class DailyRewardView : MonoBehaviour
    {
        private const string CurrentSlotInActiveKey = nameof(CurrentSlotInActiveKey);
        private const string TimeGetRewardKey = nameof(TimeGetRewardKey);

        [Header("Settings Time Get Reward")]
        [SerializeField] private float _timeCooldown = 86400;
        [SerializeField] private float _timeDeadline = 172800;
        
        [Header("Settings Rewards")]
        [SerializeField] private List<Reward> _rewards;
        
        [Header("UI Elements")]
        [SerializeField] private TMP_Text _txtTimeNextReward;
        
        [SerializeField] private Transform _slotRewardsContainer;
        [SerializeField] private SlotRewardContainerView _slotRewardContainerView;
        [SerializeField] private Button _btnGetReward;
        [SerializeField] private Button _btnReset;
        [SerializeField] private Slider _progressBar;
        
        public float TimeCooldown => _timeCooldown;
  
        public float TimeDeadline => _timeDeadline;
  
        public List<Reward> Rewards => _rewards;

        public TMP_Text TimerNewReward => _txtTimeNextReward;
  
        public Transform SlotRewardsContainer => _slotRewardsContainer;
  
        public SlotRewardContainerView SlotRewardViewContainer => _slotRewardContainerView;
  
        public Button GetRewardButton => _btnGetReward;
  
        public Button ResetButton => _btnReset;
        
        public Slider ProgressBar => _progressBar;
        
        public int CurrentSlotInActive
        {
            get => PlayerPrefs.GetInt(CurrentSlotInActiveKey, 0);
            set => PlayerPrefs.SetInt(CurrentSlotInActiveKey, value);
        }

        public DateTime? TimeGetReward
        {
            get
            {
                var data = PlayerPrefs.GetString(TimeGetRewardKey, null);
          
                if (!string.IsNullOrEmpty(data))
                    return DateTime.Parse(data);

                return null;
            }
            set
            {
                if (value != null)
                    PlayerPrefs.SetString(TimeGetRewardKey, value.ToString());
                else
                    PlayerPrefs.DeleteKey(TimeGetRewardKey);
            }
        }

        private void OnDestroy()
        {
            _btnGetReward.onClick.RemoveAllListeners();
            _btnReset.onClick.RemoveAllListeners();
        }
    }
}