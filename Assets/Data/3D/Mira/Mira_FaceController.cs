using UnityEngine;

public class Mira_FaceController : MonoBehaviour
{
    public enum EyeType {
        Flat, Unsee, Sad, Angry, Shy, Smile 
    }

    public enum MouthType {
        Flat, Ow, Smile, Shy
    }

    [SerializeField] Mira_TextureController leftEye, rightEye, mouth;

    void LEye(int index)
    {
        leftEye.SetTexture(index);
    }

    void REye(int index)
    {
        rightEye.SetTexture(index);
    }

    void Mouth(int index)
    {
        mouth.SetTexture(index);
    }

    public void SetLeftEye(EyeType eyeType)
    {
        switch (eyeType)
        {
            case EyeType.Flat:
            LEye(0);
            break;

            case EyeType.Unsee:
            LEye(1);
            break;

            case EyeType.Sad:
            LEye(2);
            break;

            case EyeType.Angry:
            LEye(3);
            break;

            case EyeType.Shy:
            LEye(4);
            break;

            case EyeType.Smile:
            LEye(5);
            break;
        }
    }

    public void SetRightEye(EyeType eyeType)
    {
        switch (eyeType)
        {
            case EyeType.Flat:
            REye(0);
            break;

            case EyeType.Unsee:
            REye(1);
            break;

            case EyeType.Sad:
            REye(2);
            break;

            case EyeType.Angry:
            REye(3);
            break;

            case EyeType.Shy:
            REye(4);
            break;

            case EyeType.Smile:
            REye(5);
            break;
        }
    }

    public void SetMouth(MouthType mouthType)
    {
        switch (mouthType)
        {
            case MouthType.Flat:
            Mouth(0);
            break;

            case MouthType.Ow:
            Mouth(1);
            break;

            case MouthType.Smile:
            Mouth(2);
            break;

            case MouthType.Shy:
            Mouth(3);
            break;
        }
    }

    public void SetBothEye(EyeType eyeType)
    {
        SetLeftEye(eyeType);
        SetRightEye(eyeType);
    }

    public void Smile() 
    {
        SetBothEye(EyeType.Smile);
        SetMouth(MouthType.Flat);
    }

    public void HugeSmile() 
    {
        SetBothEye(EyeType.Smile);
        SetMouth(MouthType.Smile);
    }

    public void CalmSmile()
    {
        SetBothEye(EyeType.Unsee);
        SetMouth(MouthType.Smile);
    }

    public void Flat() 
    {
        SetBothEye(EyeType.Flat);
        SetMouth(MouthType.Flat);
    }

    public void Shy() 
    {
        SetBothEye(EyeType.Shy);
        SetMouth(MouthType.Shy);
    }

    public void EyeFlat() { SetBothEye(EyeType.Flat); }
    public void EyeUnsee() { SetBothEye(EyeType.Unsee); }
    public void EyeSad() { SetBothEye(EyeType.Sad); }
    public void EyeAngry() { SetBothEye(EyeType.Angry); }
    public void EyeShy() { SetBothEye(EyeType.Shy); }
    public void EyeSmile() { SetBothEye(EyeType.Smile); }

    public void MouthFlat() { SetMouth(MouthType.Flat); }
    public void MouthOw() { SetMouth(MouthType.Ow); }
    public void MouthSmile() { SetMouth(MouthType.Smile); }
    public void MouthShy() { SetMouth(MouthType.Shy); }
}
