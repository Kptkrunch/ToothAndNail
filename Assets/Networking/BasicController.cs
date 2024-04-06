// using UnityEngine;
// using Photon.Pun;
//
// [RequireComponent(typeof(CharacterController))]
// public class BasicController : MonoBehaviourPun, IPunObservable {
//
//     BasicController basicController;
//
//     private void Awake()
//     {
//         basicController = GetComponent<BasicController>();
//     }
//
//     void Update()
//     {
//         //Processes Input only if this client is controlling this character
//         if (photonView.IsMine)
//         {
//             basicController.ProcessInputs();
//         }
//     }
//
//     public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
//     {
//         if (stream.IsWriting)// this client owns this player. Send these values to the other clients/network
//         {
//             stream.SendNext(basicController.Position);
//             stream.SendNext(basicController.Rotation);
//             stream.SendNext(basicController.CurrentWeapon);
//             stream.SendNext(basicController.PickedUpTools);
//             stream.SendNext(basicController.ConsumedConsumables);
//
//             // Add others as per your game requirements
//         }
//         else // network player, receive data
//         {
//             basicController.Position = (Vector3)stream.ReceiveNext();
//             basicController.Rotation = (Quaternion)stream.ReceiveNext();
//             basicController.CurrentWeapon = (string)stream.ReceiveNext();
//             basicController.PickedUpTools = (string)stream.ReceiveNext();
//             basicController.ConsumedConsumables = (string)stream.ReceiveNext();
//
//             // Add others as per your game requirements, ensure correct casting 
//         }
//     }
// }