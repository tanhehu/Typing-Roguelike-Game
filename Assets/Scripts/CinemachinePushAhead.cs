using UnityEngine;
using Cinemachine;

/// <summary>
/// Add this component to the SAME GameObject as your CinemachineVirtualCamera.
/// It nudges the camera in a given direction (movement input, facing direction,
/// or aim direction) and smoothly follows it, instead of the raw velocity-based
/// "Look Ahead" built into Framing Transposer.
///
/// This is what gives Hollow Knight-style cameras their feel: the push is
/// deliberate and holds steady, rather than snapping back every time the
/// player stops or reverses direction.
/// </summary>
//[AddComponentMenu("Cinemachine/Extensions/Push Ahead Camera")]
//[SaveDuringPlay]
//public class CinemachinePushAhead : CinemachineExtension
//{
//    [Header("Push Settings")]
//    [Tooltip("How far the camera pushes ahead in the aim direction, in world units.")]
//    public float pushDistance = 2.5f;

//    [Tooltip("Time in seconds for the push offset to smoothly reach its target. " +
//             "Higher = lazier, more 'weighted' camera (Hollow Knight uses a fairly slow value).")]
//    public float smoothTime = 0.35f;

//    [Header("Direction Handling")]
//    [Tooltip("Input magnitude below this is treated as 'no input'.")]
//    public float inputDeadzone = 0.1f;

//    [Tooltip("If true, camera keeps pushing toward the last direction you moved/faced " +
//             "even after you stop (Hollow Knight behavior). If false, it recenters when input stops.")]
//    public bool holdLastDirection = true;

//    [Tooltip("If true, only allows push along the dominant axis (classic 2D platformer feel: " +
//             "push left/right, ignore small vertical noise). Turn off for free 8-directional top-down push.")]
//    public bool restrictToDominantAxis = false;

//    Vector2 _aimDirection;
//    Vector2 _currentOffset;
//    Vector2 _offsetVelocity;
//    Vector2 _lastNonZeroDir = Vector2.right;

//    /// <summary>
//    /// Call this every frame from your player/input script with the direction
//    /// the camera should lean toward — e.g. movement input, facing direction,
//    /// or a mouse/stick aim direction. Does not need to be normalized.
//    /// </summary>
//    public void SetAimDirection(Vector2 dir)
//    {
//        _aimDirection = dir;

//        if (dir.sqrMagnitude > inputDeadzone * inputDeadzone)
//        {
//            Vector2 normalized = dir.normalized;

//            if (restrictToDominantAxis)
//            {
//                normalized = Mathf.Abs(normalized.x) >= Mathf.Abs(normalized.y)
//                    ? new Vector2(Mathf.Sign(normalized.x), 0f)
//                    : new Vector2(0f, Mathf.Sign(normalized.y));
//            }

//            _lastNonZeroDir = normalized;
//        }
//    }

//    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
//    {
//        // Apply after Body (Framing Transposer) has positioned the camera on the target,
//        // so our push is an additive offset on top of normal follow behavior.
//        if (stage != CinemachineCore.Stage.Body)
//            return;

//        bool hasInput = _aimDirection.sqrMagnitude > inputDeadzone * inputDeadzone;

//        Vector2 targetDir = hasInput
//            ? _lastNonZeroDir
//            : (holdLastDirection ? _lastNonZeroDir : Vector2.zero);

//        Vector2 targetOffset = targetDir * pushDistance;

//        float dt = deltaTime >= 0f ? deltaTime : Time.deltaTime;
//        _currentOffset = Vector2.SmoothDamp(
//            _currentOffset, targetOffset, ref _offsetVelocity, smoothTime, Mathf.Infinity, dt);

//        state.RawPosition += (Vector3)_currentOffset;
//    }
//}

