using DG.Tweening;
using UnityEngine;

namespace UI.Animals
{
	public class TastyLabel : MonoBehaviour
	{
		[SerializeField] private Transform target;
		[SerializeField] private Vector3 offset = new(0f, 1f, 0f);
		
		private float _duration;
		
		public void Initialize(float duration)
		{
			_duration = duration;
		}

		public void Show()
		{
			gameObject.SetActive(true);
			transform.position = target.position + offset;
			transform.rotation = Quaternion.Euler(90f, 0f, 0f);
			transform.DOPunchScale(new Vector3(1.5f, 1.5f, 1.5f), 0.3f);
			DOVirtual.DelayedCall(_duration, () => gameObject.SetActive(false));
		}
	}
}