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
using UniRx;


[System.SerializableAttribute()]
public sealed partial class LevelSceneManagerSettings {
    
    public string[] _Scenes;
}

// <summary>
// The responsibility of this class is to manage the scenes Initialization, Loading, Transitioning, and Unloading.
// </summary>
public class LevelSceneManagerBase : SceneManager {
    
    private CharacterController _CharacterController;
    
    public LevelSceneManagerSettings _LevelSceneManagerSettings = new LevelSceneManagerSettings();
    
    [Inject()]
    public virtual CharacterController CharacterController {
        get {
            if ((this._CharacterController == null)) {
                this._CharacterController = new CharacterController() { Container = Container };
            }
            return this._CharacterController;
        }
        set {
            _CharacterController = value;
        }
    }
    
    // <summary>
    // This method is the first method to be invoked when the scene first loads. Anything registered here with 'Container' will effectively 
    // be injected on controllers, and instances defined on a subsystem.And example of this would be Container.RegisterInstance<IDataRepository>(new CodeRepository()). Then any property with 
    // the 'Inject' attribute on any controller or view-model will automatically be set by uFrame. 
    // </summary>
    public override void Setup() {
        base.Setup();
        Container.RegisterController<CharacterController>(CharacterController);
        this.Container.InjectAll();
    }
    
    public override void Initialize() {
        base.Initialize();
    }
}
