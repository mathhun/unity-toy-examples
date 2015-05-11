// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 2.0.50727.1433
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;


[DiagramInfoAttribute("RunningGuyGame")]
public abstract class CharacterViewBase : ViewBase {
    
    [UFGroup("View Model Properties")]
    [UnityEngine.HideInInspector()]
    public Boolean _JumpLocked;
    
    [UFGroup("View Model Properties")]
    [UnityEngine.HideInInspector()]
    public Int32 _JumpsPerformed;
    
    [UFGroup("View Model Properties")]
    [UnityEngine.HideInInspector()]
    public Int32 _CoinsCollected;
    
    public override System.Type ViewModelType {
        get {
            return typeof(CharacterViewModel);
        }
    }
    
    public CharacterViewModel Character {
        get {
            return ((CharacterViewModel)(this.ViewModelObject));
        }
        set {
            this.ViewModelObject = value;
        }
    }
    
    public override ViewModel CreateModel() {
        return this.RequestViewModel(GameManager.Container.Resolve<CharacterController>());
    }
    
    protected override void InitializeViewModel(ViewModel viewModel) {
        CharacterViewModel character = ((CharacterViewModel)(viewModel));
        character.JumpLocked = this._JumpLocked;
        character.JumpsPerformed = this._JumpsPerformed;
        character.CoinsCollected = this._CoinsCollected;
    }
    
    public virtual void ExecutePickUpCoin() {
        this.ExecuteCommand(Character.PickUpCoin);
    }
}

[DiagramInfoAttribute("RunningGuyGame")]
public abstract class CoinViewBase : ViewBase {
    
    public override System.Type ViewModelType {
        get {
            return typeof(CoinViewModel);
        }
    }
    
    public CoinViewModel Coin {
        get {
            return ((CoinViewModel)(this.ViewModelObject));
        }
        set {
            this.ViewModelObject = value;
        }
    }
    
    public override ViewModel CreateModel() {
        return this.RequestViewModel(GameManager.Container.Resolve<CoinController>());
    }
    
    protected override void InitializeViewModel(ViewModel viewModel) {
    }
    
    public virtual void ExecutePickUp() {
        this.ExecuteCommand(Coin.PickUp);
    }
}

[DiagramInfoAttribute("RunningGuyGame")]
public abstract class LevelRootViewBase : ViewBase {
    
    [UFGroup("View Model Properties")]
    [UnityEngine.HideInInspector()]
    public ViewBase _Player;
    
    public override System.Type ViewModelType {
        get {
            return typeof(LevelRootViewModel);
        }
    }
    
    public LevelRootViewModel LevelRoot {
        get {
            return ((LevelRootViewModel)(this.ViewModelObject));
        }
        set {
            this.ViewModelObject = value;
        }
    }
    
    public override ViewModel CreateModel() {
        return this.RequestViewModel(GameManager.Container.Resolve<LevelRootController>());
    }
    
    protected override void InitializeViewModel(ViewModel viewModel) {
        LevelRootViewModel levelRoot = ((LevelRootViewModel)(viewModel));
        levelRoot.Player = this._Player == null ? null : this._Player.ViewModelObject as CharacterViewModel;
    }
}

public class CharacterAvatarViewViewBase : CharacterViewBase {
    
    private IDisposable _MovementIntentionDisposable;
    
    private IDisposable _JumpIntentionDisposable;
    
    private IDisposable _IsOnTheGroundDisposable;
    
    [UFToggleGroup("MovementState")]
    [UnityEngine.HideInInspector()]
    [UFRequireInstanceMethod("MovementStateChanged")]
    public bool _BindMovementState = true;
    
    [UFToggleGroup("JumpState")]
    [UnityEngine.HideInInspector()]
    [UFRequireInstanceMethod("JumpStateChanged")]
    public bool _BindJumpState = true;
    
    [UFToggleGroup("IsNotOnTheGround")]
    [UnityEngine.HideInInspector()]
    public bool _BindIsNotOnTheGround = true;
    
    public override ViewModel CreateModel() {
        return this.RequestViewModel(GameManager.Container.Resolve<CharacterController>());
    }
    
    /// Subscribes to the state machine property and executes a method for each state.
    public virtual void MovementStateChanged(Invert.StateMachine.State value) {
        if (value is Idle) {
            this.OnIdle();
        }
        if (value is MoveLeft) {
            this.OnMoveLeft();
        }
        if (value is MoveRight) {
            this.OnMoveRight();
        }
    }
    
    public virtual void OnIdle() {
    }
    
    public virtual void OnMoveLeft() {
    }
    
    public virtual void OnMoveRight() {
    }
    
    /// Subscribes to the state machine property and executes a method for each state.
    public virtual void JumpStateChanged(Invert.StateMachine.State value) {
        if (value is NoJump) {
            this.OnNoJump();
        }
        if (value is DoJump) {
            this.OnDoJump();
        }
        if (value is InTheAir) {
            this.OnInTheAir();
        }
    }
    
    public virtual void OnNoJump() {
    }
    
    public virtual void OnDoJump() {
    }
    
    public virtual void OnInTheAir() {
    }
    
    /// Subscribes to the property and is notified anytime the value changes.
    public virtual void IsNotOnTheGroundChanged(Boolean value) {
    }
    
    public virtual void ResetMovementIntention() {
        if (_MovementIntentionDisposable != null) _MovementIntentionDisposable.Dispose();
        _MovementIntentionDisposable = GetMovementIntentionObservable().Subscribe(Character._MovementIntentionProperty).DisposeWith(this);
    }
    
    protected virtual MovementIntention CalculateMovementIntention() {
        return default(MovementIntention);
    }
    
    protected virtual UniRx.IObservable<MovementIntention> GetMovementIntentionObservable() {
        return this.UpdateAsObservable().Select(p => CalculateMovementIntention());
    }
    
    public virtual void ResetJumpIntention() {
        if (_JumpIntentionDisposable != null) _JumpIntentionDisposable.Dispose();
        _JumpIntentionDisposable = GetJumpIntentionObservable().Subscribe(Character._JumpIntentionProperty).DisposeWith(this);
    }
    
    protected virtual JumpIntention CalculateJumpIntention() {
        return default(JumpIntention);
    }
    
    protected virtual UniRx.IObservable<JumpIntention> GetJumpIntentionObservable() {
        return this.UpdateAsObservable().Select(p => CalculateJumpIntention());
    }
    
    public virtual void ResetIsOnTheGround() {
        if (_IsOnTheGroundDisposable != null) _IsOnTheGroundDisposable.Dispose();
        _IsOnTheGroundDisposable = GetIsOnTheGroundObservable().Subscribe(Character._IsOnTheGroundProperty).DisposeWith(this);
    }
    
    protected virtual Boolean CalculateIsOnTheGround() {
        return default(Boolean);
    }
    
    protected virtual UniRx.IObservable<Boolean> GetIsOnTheGroundObservable() {
        return this.UpdateAsObservable().Select(p => CalculateIsOnTheGround());
    }
    
    public override void Bind() {
        base.Bind();
        ResetMovementIntention();
        ResetJumpIntention();
        ResetIsOnTheGround();
        if (this._BindMovementState) {
            this.BindProperty(Character._MovementStateProperty, this.MovementStateChanged);
        }
        if (this._BindJumpState) {
            this.BindProperty(Character._JumpStateProperty, this.JumpStateChanged);
        }
        if (this._BindIsNotOnTheGround) {
            this.BindProperty(Character._IsNotOnTheGroundProperty, this.IsNotOnTheGroundChanged);
        }
    }
}

public partial class CharacterAvatarView : CharacterAvatarViewViewBase {
}

public class LevelRootViewViewBase : LevelRootViewBase {
    
    [UFToggleGroup("Coins")]
    [UnityEngine.HideInInspector()]
    public bool _BindCoins = true;
    
    [UFGroup("Coins")]
    [UnityEngine.HideInInspector()]
    public bool _CoinsSceneFirst;
    
    [UFGroup("Coins")]
    [UnityEngine.HideInInspector()]
    public UnityEngine.Transform _CoinsContainer;
    
    [UFToggleGroup("Score")]
    [UnityEngine.HideInInspector()]
    public bool _BindScore = true;
    
    public override ViewModel CreateModel() {
        return this.RequestViewModel(GameManager.Container.Resolve<LevelRootController>());
    }
    
    /// This binding will add or remove views based on an element/viewmodel collection.
    public virtual ViewBase CreateCoinsView(CoinViewModel item) {
        return this.InstantiateView(item);
    }
    
    /// This binding will add or remove views based on an element/viewmodel collection.
    public virtual void CoinsAdded(ViewBase item) {
    }
    
    /// This binding will add or remove views based on an element/viewmodel collection.
    public virtual void CoinsRemoved(ViewBase item) {
    }
    
    /// Subscribes to the property and is notified anytime the value changes.
    public virtual void ScoreChanged(Int32 value) {
    }
    
    public override void Bind() {
        base.Bind();
        if (this._BindCoins) {
            this.BindToViewCollection( LevelRoot._CoinsProperty, viewModel=>{ return CreateCoinsView(viewModel as CoinViewModel); }, CoinsAdded, CoinsRemoved, _CoinsContainer, _CoinsSceneFirst);
        }
        if (this._BindScore) {
            this.BindProperty(LevelRoot._ScoreProperty, this.ScoreChanged);
        }
    }
}

public partial class LevelRootView : LevelRootViewViewBase {
}

public class CoinViewViewBase : CoinViewBase {
    
    public override ViewModel CreateModel() {
        return this.RequestViewModel(GameManager.Container.Resolve<CoinController>());
    }
    
    public override void Bind() {
        base.Bind();
    }
}

public partial class CoinView : CoinViewViewBase {
}
