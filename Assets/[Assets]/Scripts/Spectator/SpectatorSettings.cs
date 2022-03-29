// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Animations;

// public enum ViewMode
// {
//     FirstPerson,
//     ThirdPerson
// }
// public class SpectatorSettings : MonoBehaviour
// {
//     [SerializeField] GameObject firstpersonviewobject;
//     [SerializeField] GameObject thirdpersonviewobject;
//     [SerializeField] GameObject thirdpersoncameracontroller;

//     [SerializeField] PositionConstraint positionconstraint;
//     [SerializeField] RotationConstraint rotationconstraint;

//     // PlayerModel _playermodel;
//     // public PlayerModel playermodel
//     // {
//     //     get { return _playermodel; }
//     //     set
//     //     {
//     //         if (!value) return;
//     //         if (_playermodel) _playermodel.visible = true;
//     //         _playermodel = value;
//     //         if(viewmode==ViewMode.FirstPerson)
//     //         {
//     //             _playermodel.visible = false;
//     //         }
//     //         if (viewmode == ViewMode.ThirdPerson)
//     //         {
//     //             _playermodel.visible = true;
//     //         }
//     //     }
//     // }

//     ViewMode _viewmode;
//     public ViewMode viewmode
//     {
//         get
//         {
//             return _viewmode;
//         }
//         set
//         {
//             if (value == ViewMode.FirstPerson)
//             {
//                 if (playermodel) playermodel.visible = false;
//                 rotationconstraint.enabled = true;

//                 thirdpersonviewobject.SetActive(false);
//                 firstpersonviewobject.SetActive(true);
//             }
//             if (value == ViewMode.ThirdPerson)
//             {
//                 if (playermodel) playermodel.visible = true;
//                 rotationconstraint.enabled = lockThirdPersonView;
//                 thirdpersoncameracontroller.SetActive(!lockThirdPersonView);

//                 firstpersonviewobject.SetActive(false);
//                 thirdpersonviewobject.SetActive(true);
//             }
//             _viewmode = value;
//         }
//     }

//     bool _lockThirdPersonView;
//     public bool lockThirdPersonView
//     {
//         get
//         {
//             return _lockThirdPersonView;
//         }
//         set
//         {
//             if (viewmode == ViewMode.ThirdPerson)
//             {
//                 rotationconstraint.enabled = value;
//                 thirdpersoncameracontroller.SetActive(!value);
//             }
//             _lockThirdPersonView = value;
//         }
//     }

//     public void SpectateOn(Transform pivot)
//     {
//         ConstraintSource source = new ConstraintSource();
//         source.sourceTransform = pivot;
//         source.weight = 1;
//         positionconstraint.SetSource(0, source);
//         rotationconstraint.SetSource(0, source);
//     }
// }
