namespace System.Molecular.Core
{
    using Collections.Generic;
    using Numerics;
    public interface IAtomStorage : IEnumerable<Atom>
    {
        int Add(float x, float y, float z);
        int Add(float x, float y);
        int Add(Vector3 vector);
        int Add(Vector2 vector);
        int Add(string label, Vector3 position = default);
        int Add(string label, Vector2 position);
        int Add(int atomicIndex, Vector3 pos = default);


        int Count();

        Atom this[int index] { get; }
    }
}