using DG.Tweening;
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

        private Sequence _sequence;

        public void SetData(Reward reward, int days, bool isSelect)
        {
            _icon.sprite = reward.Icon;
            _txtDays.text = $"Day {days}";
            _txtValue.text = reward.Value.ToString();

            _selectBackground.DOFade(isSelect ? 1 : 0, 0.3f);
        }

        public void StartIconAnimation()
        {
            if (_sequence != null)
            {
                return;
            }

            _sequence = DOTween.Sequence();
            _sequence.Append(_icon.transform.DOScale(Vector3.one * 1.15f, 0.5f));
            _sequence.Append(_icon.transform.DOScale(Vector3.one, 0.5f));
            _sequence.OnComplete(() => _sequence.Restart());
        }

        public void StopIconAnimation()
        {
            _sequence.OnComplete(() =>
            {
                _sequence?.Kill();
                _sequence = null;
            });
        }

        private void Start()
        {
            gameObject.transform.localScale = Vector3.zero;
        }

        public void Show(float delay)
        {
            gameObject.transform
                .DOScale(Vector3.one, 0.3f)
                .SetDelay(delay)
                .SetEase(Ease.OutBack);
        }
    }
}