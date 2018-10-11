using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.FirstPerson
{
    [Serializable]
    public class MouseLook
    {
        public float XSensitivity = 2f;
        public float YSensitivity = 2f;
        public bool clampVerticalRotation = true;
        public float MinimumX = -90F;
        public float MaximumX = 90F;
        public bool smooth;
        public float smoothTime = 5f;


        private Quaternion m_CharacterTargetRot;
        private Quaternion m_CameraTargetRot;
        private Quaternion m_InvertedCharacterTargetRot;
        private bool inverted = false;
        private float zRot = 0;
        private float zInvertRot = 180;
        public void Init(Transform character, Transform camera)
        {
            m_CharacterTargetRot = character.localRotation;
            m_CameraTargetRot = camera.localRotation;
            m_InvertedCharacterTargetRot = character.localRotation;
            m_InvertedCharacterTargetRot.eulerAngles = new Vector3(character.localEulerAngles.x, character.localEulerAngles.y, 180);
        }

        public void InvertRotation(Transform character)
        {
            if (inverted)
            {
                m_InvertedCharacterTargetRot = character.localRotation;
                m_InvertedCharacterTargetRot.eulerAngles = new Vector3(character.localEulerAngles.x, character.localEulerAngles.y, 180);
                //trying to rotate the same way
                //m_CharacterTargetRot.eulerAngles = new Vector3(m_CharacterTargetRot.eulerAngles.x, m_CharacterTargetRot.eulerAngles.y, 360);
                smoothTime = 0;
            }
            else
            {
                m_CharacterTargetRot = character.localRotation;
                m_CharacterTargetRot.eulerAngles = new Vector3(character.localEulerAngles.x, character.localEulerAngles.y, 0);
                smoothTime = 0;
            }
            inverted = !inverted;
        }

        public void LookRotation(Transform character, Transform camera)
        {
            float yRot = CrossPlatformInputManager.GetAxis("Mouse X") * XSensitivity;
            float xRot = CrossPlatformInputManager.GetAxis("Mouse Y") * YSensitivity;
            if (inverted)
            {
                m_InvertedCharacterTargetRot *= Quaternion.Euler(0f, yRot, 0f);
            }
            else
            {
                m_CharacterTargetRot *= Quaternion.Euler(0f, yRot, 0f);
            }

            m_CameraTargetRot *= Quaternion.Euler(-xRot, 0f, 0f);

            if (clampVerticalRotation)
                m_CameraTargetRot = ClampRotationAroundXAxis(m_CameraTargetRot);

            if (smooth)
            {
                Quaternion rot;
                if (inverted)
                {
                    //not smooth rotation for y but smooth for z.
                    //this is so turning upside down is smooth
                    rot = character.localRotation;
                    rot.y = m_InvertedCharacterTargetRot.y;
                    character.localRotation = rot;

                    character.localRotation = Quaternion.Slerp(character.localRotation, m_InvertedCharacterTargetRot,
                   smoothTime * Time.deltaTime);
                    if (smoothTime < 2)
                    {
                        smoothTime += 0.25f;
                        if (smoothTime > 2)
                        {
                            smoothTime = 2;
                        }
                    }
                }
                else
                {

                    rot = character.localRotation;
                    rot.y = m_CharacterTargetRot.y;
                    character.localRotation = rot;

                    character.localRotation = Quaternion.Slerp(character.localRotation, m_CharacterTargetRot,
                   smoothTime * Time.deltaTime);
                    if (smoothTime <= 2)
                    {
                        smoothTime += 0.25f;
                        if (smoothTime > 2)
                        {
                            smoothTime = 2;
                        }
                    }
                }

                camera.localRotation = Quaternion.Slerp(camera.localRotation, m_CameraTargetRot,
                    smoothTime * Time.deltaTime);

            }
            //instant rotation. not smooth
            else
            {
                if (inverted)
                {
                    character.localRotation = m_InvertedCharacterTargetRot;
                }
                else
                {
                    character.localRotation = m_CharacterTargetRot;
                }

                camera.localRotation = m_CameraTargetRot;
            }
        }


        Quaternion ClampRotationAroundXAxis(Quaternion q)
        {
            q.x /= q.w;
            q.y /= q.w;
            q.z /= q.w;
            q.w = 1.0f;

            float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

            angleX = Mathf.Clamp(angleX, MinimumX, MaximumX);

            q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

            return q;
        }

    }
}
