using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera _vcam;
    [SerializeField]
    private Rigidbody2D _playerRb;

    [SerializeField]
    private float _idleSize = 4f;
    [SerializeField]
    private float _moveSize = 4.4f;
    [SerializeField]
    private float _tweenTime = .75f;

    Tween zoomTween;
    bool isMoving;

    private void Update()
    {
        bool movingNow = _playerRb.velocity.magnitude > 0.05f;

        // Chỉ tween khi trạng thái thay đổi
        if (movingNow != isMoving)
        {
            isMoving = movingNow;
            StartZoom(isMoving ? _moveSize : _idleSize);
        }
    }

    private void StartZoom(float targetSize)
    {
        zoomTween?.Kill();

        zoomTween = DOTween.To(
            () => _vcam.m_Lens.OrthographicSize,
            x => _vcam.m_Lens.OrthographicSize = x,
            targetSize,
            _tweenTime
        ).SetEase(Ease.OutQuad);
    }
}
