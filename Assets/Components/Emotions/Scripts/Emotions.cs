using UnityEngine;

public class Emotions : Singleton<Emotions>
{
    public enum State
    {
        Default,
        Disappointed,
        Happy,
        Naughty,
        Unimpressed
    }

    [SerializeField] private Sprite defaultEmoji;
    [SerializeField] private Sprite disappointedEmoji;
    [SerializeField] private Sprite happyEmoji;
    [SerializeField] private Sprite naughtyEmoji;
    [SerializeField] private Sprite unimpressedEmoji;

    public Sprite GetEmoji(State value)
    {
        switch (value)
        {
            case State.Default:
                return defaultEmoji;
            case State.Disappointed:
                return disappointedEmoji;
            case State.Happy:
                return happyEmoji;
            case State.Naughty:
                return naughtyEmoji;
            case State.Unimpressed:
                return unimpressedEmoji;
        }
        return defaultEmoji;
    }
}
