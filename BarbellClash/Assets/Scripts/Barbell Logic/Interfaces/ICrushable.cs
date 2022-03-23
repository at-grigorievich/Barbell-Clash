namespace Barbell
{
    public interface ICrushable
    {
        public uint MaxPlateId { get; }

        void SetCrushCollider(bool enabled);
    }
}