using HurricaneVR.Framework.Core.Grabbers;
using HurricaneVR.Framework.Shared;
using UnityEngine;

namespace HurricaneVR.Framework.Core.Player
{


    public class HVRHexaBodyHands : HVRPhysicsHands
    {
        [Header("----HVRHexaBodyHands-----")]
        public HVRJointSettings ClimbStrength;
        public HVRHandGrabber Grabber;
        public Transform Camera;

        public Transform Shoulder;
        public float ArmLength = .85f;

        [Header("Torque Control")]
        [Tooltip("If true the joint will use controller angular velocity as it's target")]
        public bool EnableTargetAngularVelocity;
        [Tooltip("Scaling factor to apply to target angular velocity")]
        [Range(0, 1)]
        public float AngularVelocityScale = 1f;

        private Vector3 _previousCamera;
        private Vector3 _previousControllerPosition;
        private Quaternion _previousRotation;

        protected override void Start()
        {
            base.Start();

            if (Grabber)
            {
                Grabber.Grabbed.AddListener(OnGrabbed);
                Grabber.Released.AddListener(OnReleased);
            }
        }

        private void OnReleased(HVRGrabberBase arg0, HVRGrabbable arg1)
        {
            ResetStrength();
        }

        private void OnGrabbed(HVRGrabberBase arg0, HVRGrabbable arg1)
        {
            if (arg1.IsClimbable && ClimbStrength)
            {
                UpdateStrength(ClimbStrength);
            }
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            UpdateJointAnchors();
            UpdateRotation();
            UpdateTargetVelocity();
        }

        public override void UpdateStrength(HVRJointSettings settings)
        {
            if (Grabber.IsClimbing)
            {
                base.UpdateStrength(ClimbStrength);
                return;
            }
            
            base.UpdateStrength(settings);
        }

        private void UpdateRotation()
        {
            Joint.targetRotation = Quaternion.Inverse(ParentRigidBody.rotation) * Target.rotation;
        }

        protected void UpdateJointAnchors()
        {
            var localTargetPosition = ParentRigidBody.transform.InverseTransformPoint(Target.position);
            if (Shoulder)
            {
                var localAnchor = ParentRigidBody.transform.InverseTransformPoint(Shoulder.position);
                var dir = localTargetPosition - localAnchor;
                dir = Vector3.ClampMagnitude(dir, ArmLength);
                var point = localAnchor + dir;
                Joint.targetPosition = point;
            }
            else
            {
                Joint.targetPosition = localTargetPosition;
            }
        }

        public override void SetupJoint()
        {
            Joint = ParentRigidBody.transform.gameObject.AddComponent<ConfigurableJoint>();
            Joint.connectedBody = RigidBody;
            Joint.autoConfigureConnectedAnchor = false;
            Joint.anchor = Vector3.zero;
            Joint.connectedAnchor = Vector3.zero;

            UpdateStrength(JointSettings);
        }



        public void UpdateTargetVelocity()
        {
            var camVelocity = (Camera.localPosition - _previousCamera) / Time.fixedDeltaTime;
            camVelocity.y = 0f;
            _previousCamera = Camera.localPosition;

            var local = ParentRigidBody.transform.InverseTransformPoint(Target.position);
            var velocity = (local - _previousControllerPosition) / Time.fixedDeltaTime;
            _previousControllerPosition = local;
            Joint.targetVelocity = velocity + camVelocity;

            var angularVelocity = Target.rotation.AngularVelocity(_previousRotation);
            if (EnableTargetAngularVelocity)
            {
                angularVelocity.Scale(new Vector3(AngularVelocityScale, AngularVelocityScale, AngularVelocityScale));
                Joint.targetAngularVelocity = Quaternion.Inverse(ParentRigidBody.transform.rotation) * angularVelocity;

                if (Joint.rotationDriveMode == RotationDriveMode.XYAndZ)
                {
                    Joint.targetAngularVelocity *= -1;
                }
            }
            else
            {
                Joint.targetAngularVelocity = Vector3.zero;
            }

            _previousRotation = Target.rotation;
        }

        

        //void CalculateForce()
        //{
        //    Vector3 positionDelta = Target.position - transform.position;
        //    Vector3 spring = Joint.xDrive.positionSpring * positionDelta;

        //    Vector3 velocityDelta = targetVelocity - RigidBody.velocity;
        //    Vector3 damper = Joint.xDrive.positionDamper * velocityDelta;

        //    force = spring + damper;
        //    force = Vector3.ClampMagnitude(force, Joint.xDrive.maximumForce);

        //}

        //private void OnDrawGizmos()
        //{
        //    if (RigidBody)
        //    {
        //        Gizmos.color = Color.green;
        //        Gizmos.DrawWireSphere(RigidBody.worldCenterOfMass, .017f);
        //    }
        //}
    }
}