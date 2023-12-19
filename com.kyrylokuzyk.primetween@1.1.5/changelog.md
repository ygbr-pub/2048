## [1.1.5] - 2023-12-14
### Added:
- Add Tween.TextMaxVisibleCharacters(TMP_Text target, ...) method to support simple typewriter animation. Please see the 'Demo.TypewriterAnimatorExample.cs' for a more complex example of text animation that makes pauses after punctuations. 
### Fixed
- Fixed: don't allow to set invalid elapsedTimeTotal and progressTotal values to infinite tweens.

## [1.1.4] - 2023-12-10
### Added:
- Tween/Sequence.timeScale can now be negative to support backward movement (rewind).
- Add 'useFixedUpdate' parameter to update a tween or sequence in the FixedUpdate().
- Release Parametric Easing to production (previously this feature was experimental): https://github.com/KyryloKuzyk/PrimeTween#parametric-easing
### Changed
- The default 'easeBetweenShakes' is now Ease.OutQuad instead of Ease.OutSine.
- The 'useUnscaledTime' parameter now doesn't prevent the abrupt delta time changes when unpausing the Editor to be consistent with the official Unity's behavior.
### Fixed
- Fixed: shakes log the 'warnEndValueEqualsCurrent' warning if object's local position/rotation is Vector3.zero/Quaternion.identity.

## [1.1.3] - 2023-12-06
### Added:
- Add Sequence.OnComplete() API.
### Fixed
- Fixed: nesting multiple empty Sequences may lead to a StackOverflow exception in some cases.
- Fixed: enabling/disabling UI.Slider in Demo scene constantly allocates GC.  

## [1.1.2] - 2023-12-03
### Added:
- Add 'PrimeTweenConfig.warnEndValueEqualsCurrent' setting to warn if the 'endValue' equals to the current animated value.
### Fixed
- Fixed: PrimeTween may log warnings in Editor when exiting Play Mode. 

## [1.1.1] - 2023-11-30
### Added:
- Sequences now support CycleMode.Yoyo and CycleMode.Rewind with the help of Sequence.Create(numCycles, CycleMode **cycleMode**).
- Sequences now support easing that can be applied to the whole Sequence with the help of Sequence.Create(..., Ease **sequenceEase**).
- 'elapsedTime', 'elapsedTimeTotal', 'progress', and 'progressTotal' properties now have setters, so it's possible to manually set the elapsed time of tweens and sequences. Please see the Demo scene for usage example.
- Parent Sequence now controls the isPaused, timeScale, and useUnscaledTime of all its children tweens and sequences.
- Add a warning when tween.SetRemainingCycles() is called on Tween.Delay(). More info: https://forum.unity.com/threads/1479609/page-3#post-9415922.
### Changed
- It's no longer allowed to Stop()/Complete() a tween inside a Sequence. Please Stop()/Complete() the parent Sequence instead.
- It's no longer allowed to await or use the '.ToYieldInstruction()' on tween inside a Sequence. Please use the parent Sequence instead.
- It's no longer allowed to add tweens to a started Sequence.
- It's now allowed to call Tween.StopAll(), Tween.CompleteAll() and Tween.SetPausedAll() from onValueChange, OnUpdate() and OnComplete().
- SetCycles() was renamed to SetRemainingCycles(). To set Sequence cycles, use Sequence.Create(cycles: numCycles).
- Remove 'minExpected' and 'numMaxExpected' parameters from Tween.StopAll/CompleteAll/SetPausedAll() methods.
### Fixed
- Fixed: Tween.GetTweensCount() may return the incorrect result if called from the OnComplete() callback.
- Fixed: MeasureMemoryAllocations.cs example script doesn't ignore its own allocations.

## [1.0.17] - 2023-11-12
### Fixed
- Fixed: the Demo scene doesn't compile if PrimeTween is not installed.

## [1.0.16] - 2023-11-04
### Fixed
- Fixed: Quaternion tweens don't work correctly with CycleMode.Incremental. Bug report: https://github.com/KyryloKuzyk/PrimeTween/issues/19

## [1.0.15] - 2023-10-14
### Fixed
- Fixed: the first time Sequence is created, it may play incorrectly in some cases.

## [1.0.14] - 2023-10-02
### Added
- Add methods to animate RectTransform.offsetMin/offsetMax.
- Add sequence.timeScale to set the timeScale for all tweens in a Sequence.
- Add Tween.TweenTimeScale(Sequence) method to animate sequence.timeScale over time.
### Fixed
- Fixed: Unity's Time.unscaledDeltaTime reports the wrong value after unpausing a scene in the Editor.

## [1.0.13] - 2023-09-24
### Fixed
- Fixed: passing a null UnityEngine.Object to 'Tween.' methods causes null ref exception in PrimeTweenManager.Update(). Bug report: https://github.com/KyryloKuzyk/PrimeTween/issues/13

## [1.0.12] - 2023-09-22
### Added
- Experimental: add parametric Easing.Bounce(float strength) and Easing.BounceExact(float magnitude). BounceExact allows to specify the exact bounce amplitude in meters/degrees regardless of the total tween distance. This feature requires the PRIME_TWEEN_EXPERIMENTAL define.
- Add From(fromValue) method to adapter.
- Support parametric OutBack and OutElastic eases in the adapter.
### Changed
- Easing.Elastic: normalize the oscillation period by duration; this ensures the tween has the same period regardless of duration.

## [1.0.11] - 2023-09-21
### Added
- Tween.GlobalTimeScale(), Tween.TweenTimeScale(), and tween.timeScale are no longer experimental.
- Add tween.OnUpdate() to execute a custom callback when the animated value is updated.
- Experimental: add Easing.Overshoot(float strength) and Easing.Elastic(float strength, float normalizedPeriod) methods to customize Ease.OutBounce and Ease.OutElastic.
### Changed
- Tween methods now accept Easing struct instead of AnimationCurve. You can continue using AnimationCurve as before because it is implicitly converted to the Easing struct.
- Ease.OutElastic now works the same as the most popular js and C# tween libraries.
### Fixed
- Add PrimeTween.Demo namespace to Demo scripts.

## [1.0.10] - 2023-09-13
### Added
- Add Tween.XxxAtSpeed() methods to create animations based on speed instead of duration.
- Add Tween.GetTweensCount() method.
### Changed
- Improve performance.

## [1.0.9] - 2023-09-08
### Changed
- Tween.StopAll(null) and Tween.CompleteAll(null) now immediately clean the internal tweens list, so PrimeTweenConfig.SetTweensCapacity() can be called immediately after that.
### Fixed
- Fixed: Tween.ShakeLocalRotation() doesn't work correctly.

## [1.0.8] - 2023-09-07
### Added
- Tween.Delay(), tween.OnComplete(), and sequence.ChainDelay() methods now accept the 'warnIfTargetDestroyed' parameter to control whether PrimeTween should log the error about tween's target destruction. More info: https://github.com/KyryloKuzyk/PrimeTween/discussions/4
### Fixed
- Fix compilation error with PRIME_TWEEN_DOTWEEN_ADAPTER.

## [1.0.7] - 2023-09-01
### Added
- Add 'UI Toolkit' support (com.unity.modules.uielements). All animations from the [ITransitionAnimations](https://docs.unity3d.com/2023.2/Documentation/ScriptReference/UIElements.Experimental.ITransitionAnimations.html) interface are supported. 
- Experimental: add Tween.PositionOutBounce/LocalPositionOutBounce() methods. These methods add the ability to fine-tune the Ease.OutBounce by specifying the exact bounce amplitude, number of bounces, and bounce stiffness.
- Log error when properties of dead tweens and sequences are used.
### Changed
- Rename 'Tween.LocalScale()' methods to 'Tween.Scale()'.
- Rename 'IsAlive' to 'isAlive'.
- Rename 'IsPaused' to 'isPaused'.

## [1.0.5] - 2023-08-29
### Added
- Experimental: add tween.timeScale; Tween.TweenTimeScale(); Tween.GlobalTimeScale().
### Fixed
- Fixed: onComplete callback should not be called if Tween.OnComplete<T>(_target_, ...) has been destroyed.
- Fixed: 'additive' tweens don't work correctly.

## [1.0.4] - 2023-08-22
### Added
- Warn if PrimeTween's API is used in Edit mode (when the scene is not running).
- Experimental: additive tweens.
### Changed
- Move Tween.Custom() 'AnimationCurve' parameter after 'onValueChange' parameter to be consistent with other overloads.
- Move Tween.ShakeCustom/PunchCustom() 'startValue' parameter before 'ShakeSettings' parameter to be consistent with Tween.Custom() methods.
### Fixed
- Normalize Quaternion passed to animation methods.

## [1.0.3] - 2023-08-15
### Added
- Sequence nesting: sequences can be grouped/chained to other sequences.
- Add startValue/endValue overload to all animation methods.
- Add Tween.ShakeCamera().
- Measure memory allocations in the Demo scene.

## [1.0.2] - 2023-05-29
### Added
- All tweens can now be viewed in PrimeTweenManager's inspector.
### Changed
- Tweens are now frame perfect. That is, on a stable framerate, tweens take a deterministic number of frames.

## [1.0.1] - 2023-04-28
### Added
- Add a version of Tween.Custom() that doesn't require passing a tween's target. Please note that this version will most probably generate garbage because of closure allocation.
- Add Tween.EulerAngles/LocalEulerAngles methods to animate rotation beyond 180 degrees.
- Add Demo scene.
### Changed
- Remove Tween.RotationXYZ/LocalRotationXYZ methods because manipulating Euler angles may lead to unexpected results. More info: https://docs.unity3d.com/ScriptReference/Transform-eulerAngles.html
- Tween.Rotation/LocalRotation methods now convert the Vector3 parameter to Quaternion instead of treating it as Euler angles.
### Fixed
- Property drawers don't work correctly with copy-pasting and prefabs.
- Fixed: samples asmdef has dependency duplication that leads to compilation error.
- Fixed: custom curve should not be clamped at the start or the end of the animation.

## [1.0.0] - 2023-04-20
### Added
- Animate anything (UI, Transform, Material, etc.) with zero allocations.
- Group animations in Sequences with zero allocations.
- Shake anything.
- Everything is tweakable from the Inspector.
- Async/await support.
- Coroutines support.