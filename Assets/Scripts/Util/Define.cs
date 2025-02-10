using UnityEngine;

/// <summary>
/// Collection of names used through out the project.
/// </summary>
public class Define : MonoBehaviour
{
    public enum UnitType
    {
        Player,
        Enemy
    }

    public enum Item
    {
        Lumi
    }
    public enum Scene
    {
        Unknown,
        TitleScene,
        LobbyScene,
        GameScene,
        TestScene,
    }

    public enum Sound
    {
        Bgm,
        Effect,
        Speech,
        Max,
    }

    public enum UIEvent
    {
        Click,
        Preseed,
        PointerDown,
        PointerUp,
        BeginDrag,
        Drag,
        EndDrag,
    }

    public enum SkillType
    {
        Firefly,
        ShadowClones,
        LuminescentAbsorption,
    }

    public enum GadgetType
    {
        Scouting,
        Absorption,
        Flashbang,
        MainGadget,
    }


}
