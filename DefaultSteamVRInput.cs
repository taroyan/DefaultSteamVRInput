using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

/// <summary>
/// 初期状態のVIVE Controllerの信号を取得するスクリプト
/// </summary>
public class DefaultSteamVRInput : MonoBehaviour
{
    [Tooltip("Activates an action set on attach and deactivates on detach")]
    public SteamVR_ActionSet activateActionSetOnAttach_Platformer; // インスペクター上でplatformerを選択

    [Tooltip("Activates an action set on attach and deactivates on detach")]
    public SteamVR_ActionSet activateActionSetOnAttach_Buggy; // インスペクター上でbuggyを選択

    [Tooltip("Activates an action set on attach and deactivates on detach")]
    public SteamVR_ActionSet activateActionSetOnAttach_Mixedreality; // インスペクター上でmixedrealityを選択

    // -------------- defaultのアクション --------------
    public SteamVR_Action_Boolean interactUIAction = SteamVR_Actions.default_InteractUI; // defaultのInteractUI
    public SteamVR_Action_Boolean teleportAction = SteamVR_Actions.default_Teleport; // defaultのTeleport
    public SteamVR_Action_Boolean grabPinchAction = SteamVR_Actions.default_GrabPinch; // defaultのGrabPinch
    public SteamVR_Action_Boolean grabGripAction = SteamVR_Actions.default_GrabGrip; // defaultのGrabGrip
    public SteamVR_Action_Pose poseAction = SteamVR_Actions.default_Pose; // defaultのPase
    public SteamVR_Action_Skeleton skeletonLeftHandAction = SteamVR_Actions.default_SkeletonLeftHand; // defaultのSkeletonLeftHand
    public SteamVR_Action_Skeleton skeletonRightHandAction = SteamVR_Actions.default_SkeletonRightHand; // defaultのSkeletonLeftHand
    public SteamVR_Action_Single squeezeAction = SteamVR_Actions.default_Squeeze; // defaultのSqueeze(Vector1がなかったのでSingleでいいのか不明)
    public SteamVR_Action_Boolean headsetOnHeadAction = SteamVR_Actions.default_HeadsetOnHead; // defaultのHeadsetOnHead

    private SteamVR_Action_Vibration haptic = SteamVR_Actions._default.Haptic; // defaultのHaptic  Outのアクション 
    // ----------------------------------------------

    // -------------- flatformerのアクション --------------
    public SteamVR_Action_Vector2 moveAction = SteamVR_Actions.platformer_Move; // platformerのMove
    public SteamVR_Action_Boolean jumpAction = SteamVR_Actions.platformer_Jump; // platformerのJump
    // -------------------------------------------------

    // -------------- buggyのアクション --------------
    public SteamVR_Action_Vector2 steeringAction = SteamVR_Actions.buggy_Steering; // buggyのSteering
    public SteamVR_Action_Single throttleAction = SteamVR_Actions.buggy_Throttle; // buggyのThrottle(Vector1がなかったのでSingleでいいのか不明)
    public SteamVR_Action_Boolean brakeAction = SteamVR_Actions.buggy_Brake; // buggyのBrake
    public SteamVR_Action_Boolean resetAction = SteamVR_Actions.buggy_Reset; // buggyのReset
    // --------------------------------------------

    private SteamVR_Input_Sources hand;

    private void Start()
    {
        activateActionSetOnAttach_Platformer.Activate(SteamVR_Input_Sources.LeftHand); // 左のコントローラーのplatformerを有効へ
        activateActionSetOnAttach_Platformer.Activate(SteamVR_Input_Sources.RightHand); // 右のコントローラーのplatformerを有効へ
        activateActionSetOnAttach_Buggy.Activate(SteamVR_Input_Sources.LeftHand); // 左のコントローラーのbuggyを有効へ
        activateActionSetOnAttach_Buggy.Activate(SteamVR_Input_Sources.RightHand); // 右のコントローラーのbuggyを有効へ
        activateActionSetOnAttach_Mixedreality.Activate(SteamVR_Input_Sources.LeftHand); // 左のコントローラーのmixedrealityを有効へ
        activateActionSetOnAttach_Mixedreality.Activate(SteamVR_Input_Sources.RightHand); // 右のコントローラーのmixedrealityを有効へ
    }

    private void Update()
    {
        // -------------- トラックパッドの位置取得 ------------------
        Vector2 ml= moveAction[SteamVR_Input_Sources.LeftHand].axis; // 左のトラックパッドの位置を取得
        if (ml != Vector2.zero)
        {
            Debug.Log("左(Move) " + ml.x.ToString() + "  " + ml.y.ToString());
        }
        Vector2 mr = moveAction[SteamVR_Input_Sources.RightHand].axis; // 右のトラックパッドの位置を取得
        if (mr != Vector2.zero)
        {
            Debug.Log("右(Move) " + mr.x.ToString() + "  " + mr.y.ToString());
        }

        ml = steeringAction[SteamVR_Input_Sources.LeftHand].axis;    // 左のトラックパッドの位置を取得
        if (ml != Vector2.zero)
        {
            Debug.Log("左(Steering) " + ml.x.ToString() + "  " + ml.y.ToString());
        }
        mr = steeringAction[SteamVR_Input_Sources.RightHand].axis; // 右のトラックパッドの位置を取得
        if (mr != Vector2.zero)
        {
            Debug.Log("右(Steering) " + mr.x.ToString() + "  " + mr.y.ToString());
        }
        // -----------------------------------------------------

        // --------------- トラックパッドクリック -------------------
        if (teleportAction.GetStateDown(SteamVR_Input_Sources.LeftHand)) // 左手のトラックパッドをクリックした?(Yes)
        {
            Debug.Log("左手のトラックパッドをクリックしました(Teleport)");
        }
        
        if (teleportAction.GetStateDown(SteamVR_Input_Sources.RightHand)) // 右手のトラックパッドをクリックした?(Yes)
        {
            Debug.Log("右手のトラックパッドをクリックしました(Teleport)");
        }
        
        if (jumpAction.GetStateDown(SteamVR_Input_Sources.LeftHand)) // 左手のトラックパッドをクリックした?(Yes)
        {
            Debug.Log("左手のトラックパッドをクリックしました(Jump)");
        }
        
        if (jumpAction.GetStateDown(SteamVR_Input_Sources.RightHand)) // 右手のトラックパッドをクリックした?(Yes)
        {
            Debug.Log("右手のトラックパッドをクリックしました(Jump)");
        }
        
        if (brakeAction.GetStateDown(SteamVR_Input_Sources.LeftHand)) // 左手のトラックパッドをクリックした?(Yes)
        {
            Debug.Log("左手のトラックパッドをクリックしました(Brake)");
        }
        
        if (brakeAction.GetStateDown(SteamVR_Input_Sources.RightHand)) // 右手のトラックパッドをクリックした?(Yes)
        {
            Debug.Log("右手のトラックパッドをクリックしました(Brake)");
        }
        // -----------------------------------------------------
        
        // -------------- メニュークリック --------------
        if (resetAction.GetStateDown(SteamVR_Input_Sources.LeftHand))    // 左手のメニューをクリックした?(Yes)
        {
            Debug.Log("左手のメニューをクリックしました");
        }
        
        if (resetAction.GetStateDown(SteamVR_Input_Sources.RightHand))    // 右手のメニューをクリックした?(Yes)
        {
            Debug.Log("右手のメニューをクリックしました");
        }
        // ------------------------------------------
        
        // -------------- システムクリック --------------
        // デフォルト設定では未使用
        // ------------------------------------------
        
        // -------------- グリップクリック --------------
        if (grabGripAction.GetStateDown(SteamVR_Input_Sources.LeftHand))    // 左手のグリップを握った?(Yes)
        {
            Debug.Log("左手のグリップを握りました");
        }
        if (grabGripAction.GetStateDown(SteamVR_Input_Sources.RightHand))    // 右手のグリップを握った?(Yes)
        {
            Debug.Log("右手のグリップを握りました");
        }
        // ------------------------------------------
        
        // -------------- トリガークリック --------------
        if (interactUIAction.GetStateDown(SteamVR_Input_Sources.LeftHand))    // 左手のトリガーを引いた?(Yes)
        {
            Debug.Log("左手のトリガーを引きました(InteractUIAction)");
        }
        if (grabPinchAction.GetStateDown(SteamVR_Input_Sources.LeftHand))    // 左手のトリガーを引いた?(Yes)
        {
            Debug.Log("左手のトリガーを引きました(GrabPinch)");
        }
        if (interactUIAction.GetStateDown(SteamVR_Input_Sources.RightHand))    // 右手のトリガーを引いた?(Yes)
        {
            Debug.Log("右手のトリガーを引きました(InteractUIAction)");
        }
        if (grabPinchAction.GetStateDown(SteamVR_Input_Sources.RightHand))    // 右手のトリガーを引いた?(Yes)
        {
            Debug.Log("右手のトリガーを引きました(GrabPinch)");
        }
        // ------------------------------------------
        
        // -------------- トリガープル --------------
        if (squeezeAction[SteamVR_Input_Sources.LeftHand].changed)    // 左手のトリガーの引いている量が変化した?(Yes)
        {
            Debug.Log("左手のトリガーを引いている最中です(Squeeze) " + squeezeAction.GetAxis(SteamVR_Input_Sources.LeftHand));
        }
        
        if (squeezeAction[SteamVR_Input_Sources.RightHand].changed)    // 右手のトリガーの引いている量が変化した?(Yes)
        {
            Debug.Log("右手のトリガーを引いている最中です(Squeeze) " + squeezeAction.GetAxis(SteamVR_Input_Sources.RightHand));
        }
        
        if (throttleAction[SteamVR_Input_Sources.LeftHand].changed)    // 左手のトリガーの引いている量が変化した?(Yes)
        {
            Debug.Log("左手のトリガーを引いている最中です(Throttle) " + squeezeAction.GetAxis(SteamVR_Input_Sources.LeftHand));
        }
        
        if (throttleAction[SteamVR_Input_Sources.RightHand].changed)    // 右手のトリガーの引いている量が変化した?(Yes)
        {
            Debug.Log("右手のトリガーを引いている最中です(Throttle) " + squeezeAction.GetAxis(SteamVR_Input_Sources.RightHand));
        }    
        // ----------------------------------------
        
        // -------------- コントローラーの振動 --------------
        if (interactUIAction.GetState(SteamVR_Input_Sources.LeftHand))    //　左手のコントローラを引き続けている?(Yes)
        {
            haptic.Execute(0, 0.005f, 0.005f, 1, SteamVR_Input_Sources.LeftHand);    // ずっと振動
        }
        
        if (interactUIAction.GetState(SteamVR_Input_Sources.RightHand))    //　右手のコントローラを引き続けている?(Yes)
        {
            haptic.Execute(0, 0.005f, 0.005f, 1, SteamVR_Input_Sources.RightHand);    // ずっと振動
        }
        // ----------------------------------------------
    }
}